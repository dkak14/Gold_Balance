using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    UnitControllerBase controller;
    [SerializeField] float invokeDst;
    [SerializeField] string soundID;
    [SerializeField] float volume = 1;
    float dst;
    Vector2 beforePos;

    private void Awake() {
        TryGetComponent(out controller);
    }
    private void Update() {
        if (controller.onPlatform) {
            dst += Mathf.Abs(beforePos.x - transform.position.x);
            if(dst > invokeDst) {
                SoundManager.Instance.PlayOneShot(SoundType.SFX, soundID, volume);
                dst = 0;
            }
        }
        beforePos = transform.position;
    }
}
