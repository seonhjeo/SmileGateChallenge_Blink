using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGroundController : MonoBehaviour
{
    private BoxCollider2D myCol;
    private GameObject myPlayer;
    private BoxCollider2D playerCol;
    private float overPosY;
    private float underPosY;

    // Start is called before the first frame update
    void Start()
    {
        myCol = GetComponent<BoxCollider2D>();
        myPlayer = GameObject.Find("Player");
        playerCol = myPlayer.GetComponent<BoxCollider2D>();
        myCol.enabled = false;
        float temp = (myCol.size.y / 2 + myCol.offset.y) * transform.localScale.y + (playerCol.size.y / 2 + playerCol.offset.y) * playerCol.transform.localScale.y;
        overPosY = transform.position.y + temp;
        underPosY = transform.position.y - temp;
    }

    // Update is called once per frame
    void Update()
    {
        if (!myCol.enabled)
        {
            if (myPlayer.transform.position.y > overPosY)
                myCol.enabled = true;
        }
        else
        {
            if (myPlayer.transform.position.y < underPosY)
                myCol.enabled = false;
        }
    }
}
