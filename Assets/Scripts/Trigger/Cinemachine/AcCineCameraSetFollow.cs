using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine;
namespace Triggers {
    [System.Serializable, CreateAssetMenu(fileName = "Cine Camera SetFollow", menuName = "Trigger/Cinemachine/Cine Camera SetFollow", order = 1)]
    public class AcCineCameraSetFollow : TriggerAction {
        [SerializeField] CinemachineVirtualCamera camera = null;
        [SerializeField] Transform follower;
        public override bool Action() {
            if (camera != null && follower != null)
                camera.Follow = follower;
            else {
                Debug.LogWarning("ī�޶� ���ų� ���� ��ü�� �����ϴ�");
            }
            return true;
        }
        public override TriggerAction GetCopyTrigger() {
            AcCineCameraSetFollow cineCameraSetFollow = CreateInstance<AcCineCameraSetFollow>();
            cineCameraSetFollow.camera = camera;
            cineCameraSetFollow.follower = follower;
            return cineCameraSetFollow;
        }
    }
}