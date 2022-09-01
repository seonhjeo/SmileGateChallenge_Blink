using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CKey : MonoBehaviour
{
    private int idx;
    public List<Text> btnText;
    private Dictionary<KeyAction, KeyCode> keySetting;
    private void Start()
    {
        keySetting = FindObjectOfType<KeySetting>().UserKey;
        idx = -1;
    }

    private void OnGUI()
    {
        Event ev = Event.current;
        if (ev.isKey)
        {
            keySetting[(KeyAction)idx] = ev.keyCode;
            idx = -1;
        }
    }

    public void ExchangeNewKey(int action)
    {
        idx = action;
    }

    void Update()
    {
        for (int i = 0; i < btnText.Count; ++i)
        {
            btnText[i].text = keySetting[(KeyAction)i].ToString();
        }
    }
}
