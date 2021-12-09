using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : UnitControllerBase
{
    PlayerAnimController animController;
    Animator animator;

    Coroutine shakeRoutine;
    CinemachineBasicMultiChannelPerlin noise;
    protected override void die() {
        animController.AnimType = WeaponType.NULL;
        animator.SetBool("isDie", true);
        EventManager.Instance.PlayerDie(this);
    }
    void DebugP(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        HP += 1000;
    }
    protected override void OnDestroy() {
        base.OnDestroy();
        playerInputController.GetInputAction("P").inputAction.started -= DebugP;
    }
    public override void Initialization() {
        base.Initialization();
        TryGetComponent(out animController);
        TryGetComponent(out animator);
        playerInputController.GetInputAction("P").inputAction.started += DebugP;
    }
    protected override void damaged(int damage, UnitControllerBase attacker, WeaponType type) {
        base.damaged(damage, attacker, type);
        Camera mainCamera = Camera.main;
        CinemachineBrain brain = mainCamera.GetComponent<CinemachineBrain>();
        CinemachineVirtualCamera CVC = brain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        noise = CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if(shakeRoutine != null) {
            StopCoroutine(shakeRoutine);
        }
        shakeRoutine = StartCoroutine(C_Shake(noise));
    }
    IEnumerator C_Shake(CinemachineBasicMultiChannelPerlin noise) {
        noise.m_AmplitudeGain = 5;
        noise.m_FrequencyGain = 5;
        yield return new WaitForSecondsRealtime(0.25f);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }
}
