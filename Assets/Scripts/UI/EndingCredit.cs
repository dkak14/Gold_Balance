using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class EndingCredit : MonoBehaviour
{
    [SerializeField] RectTransform text;
    [SerializeField] float duration;
    private void Awake() {
        text.DOAnchorPos3D(Vector3.zero, duration).OnComplete(goMain).SetEase(Ease.Linear);
    }
    void goMain() {
        StartCoroutine(GoMain());
    }
    IEnumerator GoMain() {
        yield return new WaitForSecondsRealtime(5);
        SceneLoader.Instance.SceneChange("Main", new ScreenEffectData("Normal", 5, 1, false));
    }
}
