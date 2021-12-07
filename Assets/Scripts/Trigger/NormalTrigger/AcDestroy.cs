using UnityEngine;
using UnityEditor;
namespace Triggers {
    [System.Serializable, CreateAssetMenu(fileName = "Destroy", menuName = "Trigger/Destroy", order = 1)]
    public class AcDestroy : TriggerAction {
        [SerializeField] GameObject gameObject;
        [SerializeField] float timer;
        public override bool Action() {
            if (gameObject != null)
                Destroy(gameObject, timer);
            else {
                Debug.LogWarning("������ ������Ʈ�� �����ϴ�");
            }
            return true;
        }
        public override TriggerAction GetCopyTrigger() {
            AcDestroy destroy = CreateInstance<AcDestroy>();
            destroy.gameObject = gameObject;
            destroy.timer = timer;
            return destroy;
        }
    }
}
