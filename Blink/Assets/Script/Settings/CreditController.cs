using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditController : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(ScrollCredit());
    }

    private IEnumerator ScrollCredit()
    {
        while(transform.localPosition.y < 500)
        {
            transform.Translate(0, 50 * Time.deltaTime, 0);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ResetCredit();
    }

    private void ResetCredit()
    {
        transform.localPosition = new Vector3(0, -500f, 0);
    }
}
