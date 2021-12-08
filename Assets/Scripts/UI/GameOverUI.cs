using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameOverUI : MonoBehaviour
{
    [SerializeField] float activeDuration;
    [Header("백그라운드")]
    [SerializeField] Image backGround;
    [SerializeField] float backGroundDuration;
    [Header("게임 오버")]
    [SerializeField] float OverAndButtonActiveDuration;
    [SerializeField] Image gameOver;
    [SerializeField] float gameOverDuration;
    [SerializeField] Vector3 gameOverStartOffset;
    [Header("버튼")]
    [SerializeField] Button restartButton;
    [SerializeField] Button exitButton;
    [SerializeField] float buttonDuration;
    [SerializeField] Vector3 buttonStartOffset;

    [SerializeField]bool test;
    private void Awake() {
        restartButton.onClick.AddListener(RestartClick);
        exitButton.onClick.AddListener(ExitClick);
        EventManager.Instance.PlayerDie += PlayerDie;
        backGround.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        if (test)
        StartCoroutine(C_PlayerDie());
    }
    private void OnDestroy() {
        EventManager.Instance.PlayerDie -= PlayerDie;
        backGround.DOKill();
        gameOver.DOKill();
        restartButton.DOKill();
        exitButton.DOKill();
    }
    void PlayerDie(PlayerController player) {
        StartCoroutine(C_PlayerDie());
    }
    IEnumerator C_PlayerDie() {
        yield return new WaitForSeconds(activeDuration);
        backGround.gameObject.SetActive(true);
        Color backColor = backGround.color;
        backGround.color = new Color(backColor.r, backColor.g, backColor.b, 0);
        backGround.DOColor(backColor, backGroundDuration);
        yield return new WaitForSeconds(OverAndButtonActiveDuration);
        gameOver.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);

        DoMove(gameOver, gameOverStartOffset, gameOverDuration);
        DoMove(restartButton.image, buttonStartOffset, buttonDuration);
        DoMove(exitButton.image, buttonStartOffset, buttonDuration);
    }
    void DoMove(Image image, Vector3 startOffset, float duration) {
        Vector3 buttonPos = image.transform.position;
        image.color = new Color(1, 1, 1, 0);
        image.transform.position += startOffset;
        image.transform.DOMove(buttonPos, duration);
        image.DOColor(Color.white, duration);
    }
    void RestartClick() {
        SceneLoader.Instance.SceneRestart(new ScreenEffectData("Normal", 2, 1, false));
    }
    void ExitClick() {
        SceneLoader.Instance.SceneChange("Main", new ScreenEffectData("Normal", 2, 1, false));
    }
}
