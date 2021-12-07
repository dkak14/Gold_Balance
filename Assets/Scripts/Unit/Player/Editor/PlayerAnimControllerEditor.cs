using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PlayerAnimController), true), CanEditMultipleObjects]
public class PlayerAnimControllerEditor : Editor
{
    PlayerAnimController animController;
    private void OnEnable() {
        animController = (PlayerAnimController)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(GUILayout.Button("�ִϸ��̼� ����")) {
            UnitAnimEditWindow.Open(animController);
        }
    }
}
