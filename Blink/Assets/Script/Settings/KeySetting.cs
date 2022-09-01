using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using System.Text;

public enum KeyAction { INTERACT, JUMP, LEFT, RIGHT, BLINK }

public class KeySetting : MonoBehaviour
{
    private Dictionary<KeyAction, KeyCode> defaultKey = new Dictionary<KeyAction, KeyCode>
    {
        { KeyAction.INTERACT, KeyCode.UpArrow },
        { KeyAction.JUMP, KeyCode.Space },
        { KeyAction.LEFT,  KeyCode.LeftArrow },
        { KeyAction.RIGHT, KeyCode.RightArrow },
        { KeyAction.BLINK,  KeyCode.D }
    };
    
    private Dictionary<KeyAction, KeyCode> userKey;
    public Dictionary<KeyAction, KeyCode> UserKey
    {
        get => userKey;
        private set => userKey = value;
    }
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        var fName = string.Format("{0}/{1}.json", Application.streamingAssetsPath + "/DataFiles", "KeySetting");
        if(File.Exists(fName))
        {
            FileStream fileStream = new FileStream(fName, FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            var jsonData = Encoding.UTF8.GetString(data);
            userKey = new Dictionary<KeyAction, KeyCode>(JsonConvert.DeserializeObject<Dictionary<KeyAction, KeyCode>>(jsonData));
        }
        else
        {
            UserKey = new Dictionary<KeyAction, KeyCode>(defaultKey);
        }
    }

    public bool CheckKeyOverlap()
    {
        for (int i = 0; i < userKey.Count; ++i)
        {
            for (int j = i + 1; j < userKey.Count - 1; ++j)
            {
                //유저가 세팅한 키 중 같은 값을 가진 키가 두 개 이상인 경우
                //원래의 키 값으로 복원
                if (userKey[(KeyAction)i] == userKey[(KeyAction)j])
                {
                    RestoreDefault();
                    return true;
                }
            }
        }
        return false;
    }

    private void RestoreDefault()
    {
        foreach (var pair in defaultKey)
        {
            userKey[pair.Key] = defaultKey[pair.Key];
        }
    }

    private void OnDisable()
    {
        var jsonData = JsonConvert.SerializeObject(userKey);
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.streamingAssetsPath + "/DataFiles", "KeySetting", jsonData);
    }
}
