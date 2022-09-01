using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    
    private SpriteRenderer mySprite;

    public Material eyeOpenMat;
    public Material eyeCloseMat;

    [SerializeField]
    private bool isChanged;

    private void Awake()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mySprite.material = eyeOpenMat;
        isChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        Color tmp = mySprite.color;
        if (WorldController.Instance.getWorldBlackOut() && WorldController.Instance.doBlinkFunc)
        {
            if (tmp.a != 0f)
            {
                tmp.a = WorldController.Instance.getWorldAlpha();
                mySprite.material = eyeCloseMat;
                mySprite.color = tmp;
            }
            isChanged = false;
        }
        else
        {
            if (!isChanged)
            {
                mySprite.material = eyeOpenMat;
                tmp.a = 1f;
                mySprite.color = tmp;
                isChanged = true;
            }
        }
       /* if (!WorldController.Instance.doBlinkFunc)
        {
            tmp.a = WorldController.Instance.getWorldAlpha();
            mySprite.material = eyeOpenMat;
            mySprite.color = tmp;
        }*/
    }
}
