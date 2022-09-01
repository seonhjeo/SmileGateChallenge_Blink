using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingController : MonoBehaviour
{
    Text text;
    void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        StartCoroutine(FadeText());
    }

    private void Update()
    {
    }

    private IEnumerator FadeText()
    {
        float textAlpha = text.color.a;
        while(true)
        {
            yield return null;
            textAlpha += Time.deltaTime;
            text.color = new Color(1, 1, 1, textAlpha);
            print(textAlpha);
            if(textAlpha >= 1f)
            {
                text.color = new Color(1, 1, 1, 1);
                print("break");
                break;
            }
        }
        yield return new WaitForSeconds(3f);
        while (true)
        {
            yield return null;
            textAlpha -= Time.deltaTime;
            text.color = new Color(1, 1, 1, textAlpha);
            if (textAlpha <= 0)
            {
                text.color = new Color(1, 1, 1, 0);
                break;
            }
        }
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainScene");
    }
}
