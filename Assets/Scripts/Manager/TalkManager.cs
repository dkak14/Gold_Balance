using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using System.Text;
using DG.Tweening;
using TMPro;
public partial class TalkManager : SingletonBehaviour<TalkManager> {
    [SerializeField] SODialogData soDialogData;
    [SerializeField] UIInputController uiInputController;
    [SerializeField] CinemachineVirtualCamera cvc;
    [SerializeField] DialogBox dialogBoxObject;
    Dialog dialog;
    int dialogIndex;

    Dictionary<int, DialogData> dialogDataDic;
    Dictionary<int, string> characterNameDic;
    Dictionary<int, Sprite> portraitDIc;

    Dictionary<string, TalkCommand> commandDic = new Dictionary<string, TalkCommand>();
    Dictionary<string, string> defaultCommandDic = new Dictionary<string, string>();
    public override void Awake() {
        base.Awake();
        commandDic.Add("Debug", new DebugCommand(dialogBoxObject));
        commandDic.Add("typingSpeed", new TypingCommand(dialogBoxObject));
        commandDic.Add("shakeCamera", new ShakeCamera(dialogBoxObject));

        defaultCommandDic.Add("i", "i");
        defaultCommandDic.Add("B", "i");
        defaultCommandDic.Add("size", "i");
        defaultCommandDic.Add("color", "i");

        dialogDataDic = new Dictionary<int, DialogData>();
        for (int i = 0; i < soDialogData.dialogData.Count; i++) {
            if(!dialogDataDic.ContainsKey(soDialogData.dialogData[i].id))
            dialogDataDic.Add(soDialogData.dialogData[i].id, soDialogData.dialogData[i]);
        }

        characterNameDic = new Dictionary<int, string>();
        dialogBoxObject.typingEnd += TypingEnd;
    }
    private void Start() {
        uiInputController.GetInputAction("LeftClick").inputAction.started += Click;
    }
    void Click(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        if(dialog.clickNext) {
            if (dialogBoxObject.isTyping) {
                dialogBoxObject.DialogCompletion();
                Debug.Log("Ÿ���� �Ϸ�");
            }
            else {
                TextUpdate();
            }
        }
    }
    public void StartTalk(Dialog dialog) {
        this.dialog = dialog;
        dialogIndex = 0;
        TextUpdate();
    }
    void TextUpdate() {
        if(dialogIndex < dialog.dialogDatas.Count) {
            DialogData data = GetDialogData(dialog.dialogDatas[dialogIndex].id);
            data.typingStartEvent = dialog.dialogDatas[dialogIndex].typingStartEvent;
            data.typingEndEvent = dialog.dialogDatas[dialogIndex].typingEndEvent;

            dialogBoxObject.StartDialog(data, null);
            Debug.Log(data.dialog + " ��ȭ ����");
        }
        else {
            if (dialog.closeBoxAfterEnd) {
                dialogBoxObject.gameObject.SetActive(false);
            }
        }
    }
    void TypingEnd() {
        StartCoroutine(C_TypingEnd());
    }
    public DialogData GetDialogData(int id) {
        DialogData data;
        dialogDataDic.TryGetValue(id, out data);
        return data;
    }
    IEnumerator C_TypingEnd() {
        if (!dialog.clickNext) {
            yield return new WaitForSeconds(dialog.dialogDatas[dialogIndex].remainingTime);
            dialogIndex++;
            TextUpdate();
        }
        else {
            dialogIndex++;
        }
    }
    public bool Command(string command) {
        string[] commandArea = command.Split('=');
        string[] commandParam = null;
        if(commandArea.Length >= 2) {
            commandParam = commandArea[1].Split('|');
        }
        if (commandDic.ContainsKey(commandArea[0])) {
            commandDic[commandArea[0]].Execute(commandParam);
            return true;
        }
        else {
            string commandName = commandArea[0];
            if (commandArea[0].Substring(0,1) == "/") {
                commandName = commandArea[0].Substring(1, commandArea[0].Length - 1);
            }
            if (!defaultCommandDic.ContainsKey(commandName)) {
                Debug.LogWarning(commandName + "�̶�� ��ɾ�� �����ϴ�.");
            }
            return false;
        }      
    }
}
[System.Serializable]
public struct Dialog {
    // Ŭ������ ���� ��ȭ�� �ѱ�� �ƴϸ� �ڵ����� �Ѿ
    public bool clickNext;
    // ��� �ؽ�Ʈ ������ �ڽ��� ������
    public bool closeBoxAfterEnd;
    public List<DialogData> dialogDatas;
}

public abstract class TalkCommand {
    protected DialogBox dialogBox;
    public TalkCommand(DialogBox box) {
        dialogBox = box;
    }
    public abstract void Execute(string[] param);
}
public class DebugCommand : TalkCommand {
    public DebugCommand(DialogBox box) : base(box) { }
    public override void Execute(string[] param) {
        Debug.Log("����� " + param[0] + " " + param[1]);
    }
}
public class TypingCommand : TalkCommand {
    public TypingCommand(DialogBox box) : base(box) { }
    public override void Execute(string[] param) {
        float value;
        if (float.TryParse(param[0], out value)) {
            dialogBox.TypingSpeed = value;
            Debug.Log("Ÿ���� ���ǵ� " + value);
        }
    }
}
public class ShakeCamera : TalkCommand {

    public ShakeCamera(DialogBox box) : base(box) {  }
    public override void Execute(string[] param) {
        float power, duration;
        Camera mainCamera = Camera.main;
        CinemachineBrain brain = mainCamera.GetComponent<CinemachineBrain>();
        CinemachineVirtualCamera CVC = brain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin noise = CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (float.TryParse(param[0], out power)) {
            noise.m_AmplitudeGain = power;
            noise.m_FrequencyGain = power;
        }
        if (float.TryParse(param[1], out duration)) {
            dialogBox.StartCoroutine(C_Noise(noise,duration));
        }
    }
    IEnumerator C_Noise(CinemachineBasicMultiChannelPerlin noise,float duration) {
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }
}