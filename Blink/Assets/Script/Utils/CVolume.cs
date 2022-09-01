using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CVolume : MonoBehaviour
{
    [SerializeField]
    private List<Text> text;
    [SerializeField]
    private List<Slider> slider;
    private Dictionary<string, int> volumeSetting;
    // Start is called before the first frame update
    void Awake()
    {
        volumeSetting = FindObjectOfType<VolumeSetting>().Volumes;
    }

    private void OnEnable()
    {
        for(int i =0;i<slider.Count;++i)
        {
            slider[i].value = volumeSetting[VolumeSetting.VolumeType[i]];
            print(string.Format("{0} - {1}", VolumeSetting.VolumeType[i], volumeSetting[VolumeSetting.VolumeType[i]]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < text.Count; ++i)
        {
            text[i].text = ((int)volumeSetting[VolumeSetting.VolumeType[i]]).ToString();
        }
    }
}
