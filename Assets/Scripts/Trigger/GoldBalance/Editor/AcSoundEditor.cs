using UnityEngine;
using UnityEditor;
using Triggers;
[CustomEditor(typeof(AcSound), true), CanEditMultipleObjects]

public class AcSoundEditor : TriggerActionEditor {
    SerializedProperty type;
    SerializedProperty soundID;
    SerializedProperty volume;
    GUIContent volumeContent;
    public override void DrawGUI(SerializedObject serializedObject, Rect position) {
        position.y += 18;
        EditorGUI.PropertyField(position, type);
        position.y += 18;
        EditorGUI.PropertyField(position, soundID);
        if (type.enumValueIndex == 1) {
            position.y += 18;
            volume.floatValue = EditorGUI.Slider(position, volumeContent, volume.floatValue, 0, 1);
        }
    }

    public override void OnTriggerEnable(SerializedObject serializedObject) {
        type = serializedObject.FindProperty("type");
        soundID = serializedObject.FindProperty("soundID");
        volume = serializedObject.FindProperty("volume");
        volumeContent = new GUIContent("Volume");
    }
}
