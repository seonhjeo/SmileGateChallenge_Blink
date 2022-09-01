using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    private GameObject SettingUI;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += Init;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Init(Scene scene, LoadSceneMode mode)
    {
        SettingUI = GameObject.Find("SettingUI").transform.Find("Canvas").gameObject;
    }

   public void SetActiveSettingUI(bool isActive)
    {
        SettingUI.SetActive(isActive);
    }

    public void ChangeScreenResolution(Tuple<int, int> resolution, ScreenMode screenMode)
    {
        switch(screenMode)
        {
            case ScreenMode.FULLSCREEN:
                Screen.SetResolution(resolution.Item1, resolution.Item2, true);
                break;
            case ScreenMode.WINDOW:
                Screen.SetResolution(resolution.Item1, resolution.Item2, false);
                break;
        }
    }
}
