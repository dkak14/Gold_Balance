using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Triggers;
[CustomEditor(typeof(AcDialog), true), CanEditMultipleObjects]
public class AcDialogEditor : TriggerActionEditor {
    AcDialog acDialog;
    SerializedProperty dialog;
    SerializedProperty clickNextProperty;
    SerializedProperty closeBoxAfterEndProperty;

    public override void DrawGUI(SerializedObject serializedObject, Rect position) {
        position.y += 18;
        if(GUI.Button(position, new GUIContent("��� ����"))) {
            AcDialogWindow.Open(serializedObject, dialog, acDialog.soDialogData);
        }
        position.y += 18;
        closeBoxAfterEndProperty.boolValue = EditorGUI.Toggle(position, "��ȭ ������ ��ȭâ ����", closeBoxAfterEndProperty.boolValue);
        position.y += 18;
        clickNextProperty.boolValue = EditorGUI.Toggle(position, "Ŭ������ ��� �ѱ�", clickNextProperty.boolValue);
        position.y += 18;
    }

    public override void OnTriggerEnable(SerializedObject serializedObject) {
        acDialog = (AcDialog)target;
        dialog = serializedObject.FindProperty("dialog");
        clickNextProperty = dialog.FindPropertyRelative("clickNext");
        closeBoxAfterEndProperty = dialog.FindPropertyRelative("closeBoxAfterEnd");
    }
}
