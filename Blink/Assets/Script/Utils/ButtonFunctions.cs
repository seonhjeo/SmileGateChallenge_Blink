using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public GameObject CreditUI;
    public void LoadNewGame()
    {
        SceneManager.LoadSceneAsync("LevelDesign");
    }

    public void LoadContinueGame()
    {
        GameManager.instance.isNewGame = false;
        SceneManager.LoadSceneAsync("LevelDesign");
    }

    public void LoadSettingUI()
    {
        UIManager.instance.SetActiveSettingUI(true);
    }

    public void LoadCredit()
    {
        CreditUI.SetActive(true);
    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Go2MainScene()
    {
        if (Time.timeScale != 1f)
            Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void QuitSetting()
    {
        GameManager.instance.keySetting.CheckKeyOverlap();
        UIManager.instance.SetActiveSettingUI(false);
        if(SceneManager.GetActiveScene().name == "LevelDesign")
        {
            WorldController.Instance.ExitSetting();
        }
    }

}
