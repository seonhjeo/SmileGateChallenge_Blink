using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private Image myImage;
    private float fadeTime;

    private static Fade _instance;
    public static Fade Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
        myImage = GetComponent<Image>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (WorldController.Instance != null)
            fadeTime = WorldController.Instance.fadingTime;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeImage(1, 0));
    }
    public void FadeOut()
    {
        StartCoroutine(FadeImage(0, 1));
    }

    private IEnumerator FadeImage(float start, float end)
    {
        float currentTime = 0f;
        float percent = 0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = myImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            myImage.color = color;
            yield return null;
        }
    }
}
