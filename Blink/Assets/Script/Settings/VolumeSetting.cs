using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public static string[] VolumeType = { "MASTER", "BGM", "SFX" };
    [SerializeField]
    private AudioMixer audioMixer;
    private Dictionary<string, int> volumes;
    public Dictionary<string, int> Volumes
    {
        get => volumes;
        private set => volumes = value;
    }

    void Start()
    {
        var fName = string.Format("{0}/{1}.json", Application.streamingAssetsPath + "/DataFiles", "VolumeSetting");
        if (File.Exists(fName))
        {
            FileStream fileStream = new FileStream(fName, FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            var jsonData = Encoding.UTF8.GetString(data);
            volumes = new Dictionary<string, int>(JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonData));
            for(int i=0;i<VolumeType.Length;++i)
            {
                audioMixer.SetFloat(VolumeType[i], volumes[VolumeType[i]] * 0.4f - 30f);
            }
        }
        else
        {
            volumes = new Dictionary<string, int>()
            {
                { "MASTER", 30 },
                {"BGM", 30 },
                {"SFX", 30 }
            };
            audioMixer.SetFloat("MASTER", 30f * 0.4f - 30f);
            audioMixer.SetFloat("BGM", 30f * 0.4f - 30f);
            audioMixer.SetFloat("SFX", 30f * 0.4f - 30f);
        }
    }
    public void SetMasterVolume(float value)
    {
        var sound = value * 0.4f - 30f;
        if (sound == -30f)
        {
            audioMixer.SetFloat("MASTER", -80f);
        }
        else
        {
            audioMixer.SetFloat("MASTER", sound);
        }
        volumes[VolumeType[0]] = (int)value;
    }

    public void SetBGMVolume(float value)
    {
        var sound = value * 0.4f - 30f;
        if (sound == -30f)
        {
            audioMixer.SetFloat("BGM", -80f);
        }
        else
        {
            audioMixer.SetFloat("BGM", sound);
        }
        volumes[VolumeType[1]] = (int)value;
    }
    public void SetSFXVolume(float value)
    {
        var sound = value * 0.4f - 30f;
        if (sound == -30f)
        {
            audioMixer.SetFloat("SFX", -80f);
        }
        else
        {
            audioMixer.SetFloat("SFX", sound);
        }
        volumes[VolumeType[2]] = (int)value;
    }

    private void OnApplicationQuit()
    {
        var jsonData = JsonConvert.SerializeObject(volumes);
        print(jsonData);
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.streamingAssetsPath + "/DataFiles", "VolumeSetting", jsonData);
    }

    private void OnDestroy()
    {
        var jsonData = JsonConvert.SerializeObject(volumes);
        print(jsonData);
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.streamingAssetsPath + "/DataFiles", "VolumeSetting", jsonData);
    }
}
