using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EffectObject : MonoBehaviour
{
    public Animator anim;
    private void Awake() {
        TryGetComponent(out anim);
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        Destroy(gameObject, clips[0].length);
    }
}
