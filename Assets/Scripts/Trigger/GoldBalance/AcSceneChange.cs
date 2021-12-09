using System.Collections;
using UnityEngine;
using UnityEditor;
namespace Triggers {
    [System.Serializable, CreateAssetMenu(fileName = "Scene Change", menuName = "Trigger/Gold/Scene Change", order = 1)]

    public class AcSceneChange : TriggerAction {
        [SerializeField] string sceneID;

        public override bool Action() {
            SceneLoader.Instance.SceneChange(sceneID, new ScreenEffectData("Normal", 0, 1, false));
            return true;
        }
        public override TriggerAction GetCopyTrigger() {
            AcSceneChange copy = CreateInstance<AcSceneChange>();
            copy.sceneID = sceneID;
            return copy;
        }
    }
}
