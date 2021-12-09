using UnityEngine;
using UnityEditor;
using Triggers;
[CustomEditor(typeof(AcSceneChange), true), CanEditMultipleObjects]
public class AcSceneChangeEditor : TriggerActionEditor {
    SerializedProperty sceneID;
    public override void DrawGUI(SerializedObject serializedObject, Rect position) {
        position.y += 18;
        EditorGUI.PropertyField(position, sceneID, new GUIContent("sceneID"));
    }

    public override void OnTriggerEnable(SerializedObject serializedObject) {
        sceneID = serializedObject.FindProperty("sceneID");
    }
}
