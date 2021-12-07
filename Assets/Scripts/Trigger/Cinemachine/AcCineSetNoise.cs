using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine;
namespace Triggers {
    [System.Serializable, CreateAssetMenu(fileName = "Cine SetNoise", menuName = "Trigger/Cinemachine/Cine SetNoise", order = 1)]
    public class AcCineSetNoise : TriggerAction {
        [SerializeField] CinemachineVirtualCamera CVC;
        [SerializeField] NoiseSettings noiseSetting;
        [SerializeField] float amplitudeGain = 1;
        [SerializeField] float frequencyGain = 1;
        [SerializeField] Vector3 pivotOffset = Vector3.zero;
        public override bool Action() {
            if (CVC != null) {
                CinemachineBasicMultiChannelPerlin CBMCP = CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                if (CBMCP != null) {
                    CBMCP.m_NoiseProfile = noiseSetting;
                    CBMCP.m_PivotOffset = pivotOffset;
                    CBMCP.m_AmplitudeGain = amplitudeGain;
                    CBMCP.m_FrequencyGain = frequencyGain;
                }
                else {
                    Debug.LogWarning("CinemachineBasicMultiChannelPerlin�� �����ϴ�.");
                }
            }
            else {
                Debug.LogWarning("Noise�� ������ �ó׸ӽ��� �����ϴ�.");
            }
            return true;
        }
        public override TriggerAction GetCopyTrigger() {
            AcCineSetNoise copy = CreateInstance<AcCineSetNoise>();
            copy.CVC = CVC;
            copy.noiseSetting = noiseSetting;
            copy.amplitudeGain = amplitudeGain;
            copy.frequencyGain = frequencyGain;
            copy.pivotOffset = pivotOffset;
            return copy;
        }
    }
}
