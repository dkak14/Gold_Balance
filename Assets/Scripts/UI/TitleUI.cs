using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleUI : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button settingButton;
    [SerializeField] Button exitButton;

    private void Awake() {
        newGameButton.onClick.AddListener(NewGameButton);
        settingButton.onClick.AddListener(SettingButon);
        exitButton.onClick.AddListener(ExitButton);
    }
    public void NewGameButton() {
        SceneLoader.Instance.SceneChange("Bar", new ScreenEffectData("Normal", 1, 1, false));
    }

    public void SettingButon() {
        EventManager.Instance.ClickSettingButton();
    }
    public void ExitButton() {
        Application.Quit();
    }
}
