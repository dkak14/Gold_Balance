using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NPC : UnitControllerBase
{
    [Header("��ȣ�ۿ� �̺�Ʈ �̸�")]
    public string InteractionEventID;
    public event Action InteractionEvent;

    public void Interaction() {
        EventManager.Instance.TriggerEventMessage(InteractionEventID);
    }
}
