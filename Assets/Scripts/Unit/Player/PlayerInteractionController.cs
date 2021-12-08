using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    PlayerInputController playerInputController;
    PlayerPickUpController pickUpController;
    PlayerController playerController;
    Collider2D collider2d;
    RaycastHit2D npcHit;
    int interactionMask;

    [SerializeField] Vector2 iconOffset;
    [SerializeField] GameObject interactionIcon;
    void Start()
    {
        TryGetComponent(out playerInputController);
        TryGetComponent(out pickUpController);
        TryGetComponent(out playerController);
        TryGetComponent(out collider2d);
        interactionMask = (1 << LayerMask.NameToLayer("NPC")) + (1 << LayerMask.NameToLayer("PickUpObject"));

        playerInputController.GetInputAction("F").inputAction.started += Interaction;
        interactionIcon.gameObject.SetActive(false);
    }
    private void OnDestroy() {
        playerInputController.GetInputAction("F").inputAction.started -= Interaction;
    }

    private void FixedUpdate() {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(collider2d.bounds.center, collider2d.bounds.size, 0, Vector2.up,1, interactionMask);
        if (hits.Length > 0) {
            float minDst = 100000;
            int minIndex = 0;
            for (int i = 0; i < hits.Length; i++) {
                float dst = Vector2.Distance(transform.position, hits[i].transform.position);
                if(dst < minDst) {
                    minDst = dst;
                    minIndex = i;
                }
            }
            RaycastHit2D hit = hits[minIndex];
            if (npcHit.transform != hit.transform) {
                npcHit = hit;
            }
        }
        else {
            npcHit = new RaycastHit2D();
        }

    }
    private void LateUpdate() {
        if (npcHit && !playerController.IsActiveState(UnitAnimState.Cinematic)) {
            interactionIcon.gameObject.SetActive(true);
            interactionIcon.transform.position = (Vector2)npcHit.transform.position + iconOffset;
        }
        else {
            interactionIcon.gameObject.SetActive(false);
        }
    }
    void Interaction(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        if (npcHit && !playerController.IsActiveState(UnitAnimState.Cinematic)) {
            NPC npc;
            if(npcHit.transform.TryGetComponent(out npc)) {
                npc.Interaction();
            }
            PickUpObject puo;
            if (npcHit.transform.TryGetComponent(out puo)) {
                if(pickUpController)
                pickUpController.GetItem(puo);
            }
        }
    }
    private void OnDrawGizmos() {
        if (npcHit) {
            MyGizmos.DrawWireCicle(npcHit.transform.position, 1, 30);
        }
    }
}
