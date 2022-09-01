using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour, IInteraction
{
    public void Interact(GameObject target)
    {
        StartCoroutine(MoveToEndScene());
    }

    IEnumerator MoveToEndScene()
    {
        if(Fade.Instance)
        {
            Fade.Instance.FadeOut();
        }
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("EndingScene");
    }
}
