using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum ScreenMode { WINDOW, FULLSCREEN }

public class ResolutionSetting : MonoBehaviour
{
    private Tuple<int, int>[] DefaultResolution = new Tuple<int, int>[4]
        {
            new Tuple<int, int>(960, 720),
            new Tuple<int, int>(1280, 720),
            new Tuple<int, int>(1600, 900),
            new Tuple<int, int>(1920, 1080)
        };
    private int resolutionIndex;
    private ScreenMode screenMode;
    private KeyValuePair<int, ScreenMode> userResolution;
    public KeyValuePair<int, ScreenMode> UserResolution
    {
        get => userResolution;
        private set => userResolution = value;
    }

// Start is called before the first frame update
void Start()
    {
        var fName = string.Format("{0}/{1}.json", Application.streamingAssetsPath + "/DataFiles", "ResolutionSetting");
        if(File.Exists(fName))
        {
            FileStream fileStream = new FileStream(fName, FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            var jsonData = Encoding.UTF8.GetString(data);
            userResolution = JsonConvert.DeserializeObject<KeyValuePair<int, ScreenMode>>(jsonData);
            UIManager.instance.ChangeScreenResolution(DefaultResolution[userResolution.Key], userResolution.Value);
        }
        else
        {
            userResolution = new KeyValuePair<int, ScreenMode>(3, ScreenMode.FULLSCREEN);
            UIManager.instance.ChangeScreenResolution(DefaultResolution[userResolution.Key], userResolution.Value);
        }
    }

    public void ChangeResolution(int value)
    {
        resolutionIndex = value;
    }

    public void ChangeScreenMode(int value)
    {
        screenMode = (ScreenMode)value;
    }

    public void AcceptResolution()
    {
        UserResolution = new KeyValuePair<int, ScreenMode>(resolutionIndex, screenMode);
        UIManager.instance.ChangeScreenResolution(DefaultResolution[resolutionIndex], screenMode);
    }

    private void OnDestroy()
    {
        var jsonData = JsonConvert.SerializeObject(userResolution);
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.streamingAssetsPath + "/DataFiles", "ResolutionSetting", jsonData);
    }
    private void OnDisable()
    {
        var jsonData = JsonConvert.SerializeObject(userResolution);
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.streamingAssetsPath + "/DataFiles", "ResolutionSetting", jsonData);
    }
}
