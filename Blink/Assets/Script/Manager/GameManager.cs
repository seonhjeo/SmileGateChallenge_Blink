using Script.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public FileIOHelper fileIOHelper;
    public bool isNewGame;
    [HideInInspector]
    public KeySetting keySetting;
    [HideInInspector]
    public VolumeSetting volumeSetting;
    [HideInInspector]
    public ResolutionSetting resolutionSetting;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
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
        fileIOHelper = new FileIOHelper();
        isNewGame = true;

        //세팅 값 불러오기
        keySetting = FindObjectOfType<KeySetting>();
        volumeSetting = FindObjectOfType<VolumeSetting>();
        resolutionSetting = FindObjectOfType<ResolutionSetting>();
    }
}
