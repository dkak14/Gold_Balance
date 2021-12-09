using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameSave;
using UnityEngine.SceneManagement;
public enum SoundType {
    BGM,SFX
}
public class SoundManager : SingletonBehaviour<SoundManager>
{
    [SerializeField] SOSoundData soSoundData;
    Dictionary<SoundType, Dictionary<string, SoundClipData>> soundDataDIc;
    Dictionary<SoundType, AudioSource> audioSourceDIc;

    [SerializeField] SOStageData soStageData;
    Dictionary<string, StageData> stageDataDic;

    bool isSFX = true;
    public override void Awake() {
        base.Awake();
        audioSourceDIc = new Dictionary<SoundType, AudioSource>();
        soundDataDIc = new Dictionary<SoundType, Dictionary<string, SoundClipData>>();

        for (int k = 0; k < soSoundData.soundDataList.Count; k++) {
            SoundType type = soSoundData.soundDataList[k].soundType;
            GameObject audioObject = new GameObject("Audio");
            audioObject.transform.parent = transform;
            AudioSource audioSource = (AudioSource)audioObject.AddComponent(typeof(AudioSource));
            audioSourceDIc.Add(type, audioSource);
            soundDataDIc.Add(type, new Dictionary<string, SoundClipData>());
            audioSource.volume = 0.5f;
            Dictionary<string, SoundClipData> dic = soundDataDIc[type];
            for (int p = 0; p < soSoundData.soundDataList[k].clipDataList.Count; p++) {
                dic.Add(soSoundData.soundDataList[k].clipDataList[p].clipID, soSoundData.soundDataList[k].clipDataList[p]);
            }
        }

        stageDataDic = new Dictionary<string, StageData>();
        for (int i = 0; i < soStageData.stageDatas.Count; i++) {
            stageDataDic.Add(soStageData.stageDatas[i].sceneName, soStageData.stageDatas[i]);
        }
        Load();
        EventManager.Instance.SceneChangeEnd += SceneChange;
        EventManager.Instance.PlayerDie += PlayerDie;
    }
    private void Start() {
        SceneChange("",SceneManager.GetActiveScene().name);
    }
    void PlayerDie(PlayerController player) {
        isSFX = false;
    }
    void SceneChange(string beforeStage, string name) {
        if (stageDataDic.ContainsKey(name)) {
            StageData data = stageDataDic[name];
            Play(SoundType.BGM, data.bgm, true);
        }
        else {
            Debug.LogWarning(name + "스테이지 데이터는 없습니다");
        }
        isSFX = true;
    }
    private void OnDisable() {
        Save();
    }
    void Save() {
        Array arr = Enum.GetValues(typeof(SoundType));
        SaveData data = new SaveData();
        for (int i = 0; i < arr.Length; i++) {
            SoundType type = (SoundType)arr.GetValue(i);
            data.AddData(type.ToString(),new SaveData(audioSourceDIc[type].volume));
            Debug.Log("세이브" + type.ToString() +" " + audioSourceDIc[type].volume);
        }
        SaveSystem.SaveSerailizeData("Option", "Sound", data);
    }
    void Load() {
        Array arr = Enum.GetValues(typeof(SoundType));
        SaveData data = SaveSystem.LoadDeSerailizedData("Option", "Sound");
        for (int i = 0; i < arr.Length; i++) {
            SoundType type = (SoundType)arr.GetValue(i);
            if (data != null) {
                SaveData volumeData = data.GetData(type.ToString());
                if (volumeData != null) {
                    float volume = volumeData.GetFloat();
                    audioSourceDIc[type].volume = volume;
                    Debug.Log("로드" + type.ToString() + " " + audioSourceDIc[type].volume);
                }
            }
        }
    }
    public float GetVoulme(SoundType type) {
        return audioSourceDIc[type].volume;
    }
    public void SetVolume(SoundType type, float volume) {
        volume = Mathf.Clamp(volume, 0, 1);
        audioSourceDIc[type].volume = volume;
    }
    public void Play(SoundType type, string clipID, bool loop) {
        SoundClipData soundData;
        if (soundDataDIc[type].TryGetValue(clipID, out soundData)) {
            audioSourceDIc[type].loop = loop;
            audioSourceDIc[type].clip = soundData.audioClip;
            audioSourceDIc[type].Play();
        }
        else {
            Debug.LogWarning(clipID + "는 없다.");
        }
    }
    public void PlayOneShot(SoundType type, string clipID, float volumeScale = 1) {
        SoundClipData soundData;
        if (soundDataDIc[type].TryGetValue(clipID, out soundData)) {
            audioSourceDIc[type].PlayOneShot(soundData.audioClip, volumeScale);
        }
        else {
            Debug.LogWarning(clipID + "는 없다.");
        }
    }

}
