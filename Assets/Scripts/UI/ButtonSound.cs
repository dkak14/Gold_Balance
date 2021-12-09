using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSound : MonoBehaviour
{
    [SerializeField] string soundID = "Button";
    [SerializeField] float volume = 1;
    // Start is called before the first frame update
    void Start()
    {
        Button button;
        if(TryGetComponent(out button)) {
            button.onClick.AddListener(Click);
        }
    }
    void Click() {
        SoundManager.Instance.PlayOneShot(SoundType.SFX, soundID, volume);
    }
}
