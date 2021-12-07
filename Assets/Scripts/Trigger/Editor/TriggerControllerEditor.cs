using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Triggers {
    [CustomEditor(typeof(TriggerController))]
    public class TriggerControllerEditor : Editor {
        TriggerController m_TriggerController;
        private void OnEnable() {
            m_TriggerController = (TriggerController)target;
            if(m_TriggerController.instanceID != m_TriggerController.GetInstanceID()) {
                m_TriggerController.instanceID = m_TriggerController.GetInstanceID();
                List<TriggerAction> copyAction = new List<TriggerAction>();
                for(int i = 0; i < m_TriggerController.Sequence.Count; i++) {
                    copyAction.Add(m_TriggerController.Sequence[i].GetCopyTrigger());
                }
                m_TriggerController.Sequence.Clear();
                m_TriggerController.Sequence = copyAction;
            }
        }
        public override void OnInspectorGUI() {
            if (GUILayout.Button("Ʈ���� ����")) {
                if (m_TriggerController != null) {
                    TriggerWindow.Open(m_TriggerController);
                }
            }
            bool eventError = false;
            //for (int i = 0; i < m_TriggerController.EventObjects.Count; i++) {
            //    if (m_TriggerController.EventObjects[i] == null || m_TriggerController.EventObjects[i].gameObject == null) {
            //        eventError = true;
            //    }
            //}
            if (m_TriggerController.EventObjects.Count == 0)
                eventError = true;
            if (eventError) {
                EditorGUILayout.HelpBox("�̺�Ʈ ������ �ʿ��մϴ�", MessageType.Warning);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
