using UnityEngine;
using UnityEditor;
namespace Triggers {
    [System.Serializable, CreateAssetMenu(fileName = "Sound", menuName = "Trigger/Gold/Sound", order = 1)]
    public class AcSound : TriggerAction {
        [SerializeField] SoundType type;
        [SerializeField] string soundID;
        [SerializeField] float volume = 1;
        public override bool Action() {
            if(type == SoundType.BGM)
            SoundManager.Instance.Play(SoundType.BGM, soundID, true);
            else {
                SoundManager.Instance.PlayOneShot(SoundType.SFX, soundID, volume);
            }
            return true;
        }
        public override TriggerAction GetCopyTrigger() {
            AcSound copy = CreateInstance<AcSound>();
            copy.type = type;
            copy.soundID = soundID;
            copy.volume = volume;
            return copy;
        }
    }
}
