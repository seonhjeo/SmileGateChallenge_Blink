using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkBarController : MonoBehaviour
{
    Slider slider;
    PlayerBlink playerBlink;
    void Awake()
    {
        slider = GetComponent<Slider>();
        playerBlink = FindObjectOfType<PlayerBlink>();
    }

    void Update()
    {
        if(playerBlink.ForcedClose)
        {
            slider.value = Mathf.Clamp(1 - playerBlink.FcTime / playerBlink.forceClosedTimer, 0.025f, 1f);
        }
        else
        {
            slider.value = Mathf.Clamp(1 - playerBlink.Eyetime / playerBlink.eyeOpenTime, 0.025f, 1f);
        }
        
    }
}
