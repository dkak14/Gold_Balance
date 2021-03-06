using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : SingletonBehaviour<SceneLoader>
{
    bool changeScene;
    string loadScene;
    public bool SceneChange(string sceneID, ScreenEffectData effectData) {
        if (Application.CanStreamedLevelBeLoaded(sceneID)) {
            if (!changeScene) {
                StartCoroutine(C_SceneChange(sceneID, effectData));
                return true;
            }
        }
        else {
            Debug.LogError($"{sceneID}라는 씬은 존재하지 않습니다");
        }
        return false;
    }
    public void SceneRestart(ScreenEffectData effectData) {
        Scene scene = SceneManager.GetActiveScene();
        SceneChange(scene.name, effectData);
    }
    IEnumerator C_SceneChange(string sceneID, ScreenEffectData effectData) {
        if (!changeScene) {
            loadScene = sceneID;
            changeScene = true;
            ScreenManager.Instance.SetActiveScreenEffect(effectData.id, effectData.duration, effectData.screenValue, false);
            Scene activeScene = SceneManager.GetActiveScene();
            EventManager.Instance.SceneChangeStart(activeScene.name, sceneID);
            yield return new WaitForSecondsRealtime(effectData.duration);
            AsyncOperation deActiveAO = SceneManager.UnloadSceneAsync(activeScene);
            while (!deActiveAO.isDone) {
                yield return null;
            }
            
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Additive);
            ao.completed += OperationOnCompleted;
            while (!ao.isDone) {
                yield return null;
            }
            ScreenManager.Instance.SetActiveScreenEffect(effectData.id, effectData.duration, effectData.screenValue, true);
            Scene nowScene = SceneManager.GetSceneByName(sceneID);
            SceneManager.SetActiveScene(nowScene);
            EventManager.Instance.SceneChangeEnd(activeScene.name, sceneID);
            Time.timeScale = 1;
            changeScene = false;
            ao.completed -= OperationOnCompleted;
        }
    }
    void OperationOnCompleted(AsyncOperation obj) {
        Scene scene = SceneManager.GetSceneByName(loadScene);
        SceneManager.SetActiveScene(scene);

    }
}
public struct ScreenEffectData {
    public string id;
    public float duration;
    public float screenValue;
    public bool fadeIn;

    public ScreenEffectData(string id, float duration, float screenValue, bool fadeIn) {
        this.id = id;
        this.duration = duration;
        this.screenValue = screenValue;
        this.fadeIn = fadeIn;
    }
}