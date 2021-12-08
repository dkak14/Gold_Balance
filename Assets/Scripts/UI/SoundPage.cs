using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class SoundPage : MonoBehaviour, IInitialization
{
    [SerializeField] UIInputController uiInputController;
    [SerializeField] GameObject pageObject;

    [SerializeField] Button exitButton;
    [SerializeField] Button backButton;
    float beforeTimeScale = 1;

    bool sceneChange = false;
    void Awake() {
        backButton.onClick.AddListener(BackClick);
        exitButton.onClick.AddListener(ExitClick);
        EventManager.Instance.SceneChangeEnd += SceneChangeEnd;
    }
    void SceneChangeEnd() {
        sceneChange = false;
        Close();
    }
    void OnEnable() {
        uiInputController.GetInputAction("ESC").inputAction.started += OpenPage;
    }
    void OnDisable() {
        uiInputController.GetInputAction("ESC").inputAction.started -= OpenPage;
    }
    void BackClick() {
        if (!sceneChange)
            Close();
    }
    void ExitClick() {
       if(SceneLoader.Instance.SceneChange("Main", new ScreenEffectData("Normal",2, 1, false)))
        sceneChange = true;
    }
    public void CancleButton() {
        Close();
    }
    void OpenPage(InputAction.CallbackContext context) {
        if(pageObject.activeSelf == false) {
            Open();
        }
        else {
            Close();
        }
    }
    void Open() {
        if (!sceneChange) {
            if (pageObject.activeSelf == false) {
                pageObject.gameObject.SetActive(true);
                beforeTimeScale = Time.timeScale;
                Time.timeScale = 0;
            }
        }
    }
    void Close() {
        if (!sceneChange) {
            if (pageObject.activeSelf == true) {
                pageObject.gameObject.SetActive(false);
                Time.timeScale = beforeTimeScale;
            }
        }
    }
    public void Initialization() {
       // uiInputController.GetInputAction("ESC").inputAction.started += OpenPage;
    }
}
