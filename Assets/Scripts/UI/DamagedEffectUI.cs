using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DamagedEffectUI : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] Color color;
    Image image;
    UnitControllerBase unit;
    void Start()
    {
        TryGetComponent(out image);
        image.raycastTarget = false;
        unit = UnitManager.Instance.GetUnit("Player");
        if (unit) {
            unit.hpValueChange += HpChange;
        }
    }
    private void OnDestroy() {
        image.DOKill();
    }
    void HpChange(int before, int now) {
        if(before > now) {
            image.DOKill();
            image.color = color;
            image.DOColor(new Color(color.r, color.g, color.b, 0), duration).SetEase(Ease.Linear);
        }
    }
}
