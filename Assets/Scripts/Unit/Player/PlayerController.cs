using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitControllerBase
{
    PlayerAnimController animController;
    Animator animator;
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
}
