using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
public class Wanted : MonoBehaviour
{
    [SerializeField] bool anykeyDisable;
    [SerializeField] string diableEventID;
    [SerializeField] string enableSound = "ReadWanted";
    [SerializeField] float volume = 1;
    bool frame;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int textID;
    private void Update() {
        if (anykeyDisable && Keyboard.current.anyKey.wasPressedThisFrame && frame) {
            gameObject.SetActive(false);
        }
        if (!frame)
            frame = true;
    }
    private void OnEnable() {
        EventManager.Instance.SetActiveCutScene(true);
        SoundManager.Instance.PlayOneShot(SoundType.SFX, enableSound, volume);
        frame = false;

        if(text != null) {
            text.text = TalkManager.Instance.GetDialogData(textID).dialog;
        }
    }

    private void OnDisable() {
        EventManager.Instance.SetActiveCutScene(false);
        if(diableEventID != "") {
            EventManager.Instance.TriggerEventMessage(diableEventID);
        }
        //UnitControllerBase unit = UnitManager.Instance.GetUnit("Player");
        //UnitMoveControllerBase playerMove;
        //if (unit && unit.TryGetComponent(out playerMove)){
        //    playerMove.isMove = true;
        //    playerMove.isJump = true;
        //}
        frame = true;
    }
}
