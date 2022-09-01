using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGroundController : MonoBehaviour
{
    [SerializeField]
    float fallSec = 0.5f;
    [SerializeField]
    float destroySec = 3f;

    private bool isTriggerd;

    Rigidbody2D myRigid;
    BoxCollider2D myCol;
    // Start is called before the first frame update
    void Start()
    {
        isTriggerd = false;
        myRigid = GetComponent<Rigidbody2D>();
        myCol = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            BoxCollider2D playerCol = collision.gameObject.GetComponent<BoxCollider2D>();
            float temp = (myCol.size.y / 2 + myCol.offset.y) * transform.localScale.y + (playerCol.size.y / 2 + playerCol.offset.y) * collision.transform.localScale.y;
            float overPosY = transform.position.y + temp;
            if (collision.transform.position.y > overPosY)
            {
                isTriggerd = true;
                Invoke("FallPlatform", fallSec);
                Destroy(gameObject, destroySec);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && isTriggerd)
        {
            myCol.enabled = false;
        }
    }

    private void FallPlatform()
    {
        myRigid.isKinematic = false;
    }
}
