using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    private SpriteRenderer mySprite;

    public Color eyeOpendColor = Color.black;
    public Color eyeClosedColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        eyeOpendColor.a = 1f;
        eyeClosedColor.a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Color tmp;

        if (WorldController.Instance.getWorldBlackOut())
        {
            tmp = eyeClosedColor;
            tmp.a = WorldController.Instance.getWorldAlpha();
            mySprite.color = tmp;
        }
        else
        {
            tmp = eyeOpendColor;
            tmp.a = 1f;
            mySprite.color = tmp;
        }
    }
}
