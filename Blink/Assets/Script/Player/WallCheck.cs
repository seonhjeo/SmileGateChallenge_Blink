using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool hitLeftWall = false;
    public bool hitRightWall = false;
    public bool hitWall = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float colPosX = collision.transform.position.x;
        if (collision.tag == "Ground")
        {
            hitWall = true;
            if (!hitRightWall && colPosX > transform.position.x)
                hitRightWall = true;
            if (!hitLeftWall && colPosX < transform.position.x)
                hitLeftWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            hitWall = false;
            hitLeftWall = false;
            hitRightWall = false;
        }   
    }
}
