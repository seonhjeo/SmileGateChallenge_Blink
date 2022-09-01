using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJumpGroundController : MonoBehaviour
{
    public float jumpHeight = 16f;

    private float rotationZ;
    private float forceToX;
    private float forceToY;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        rotationZ = transform.rotation.eulerAngles.z * Mathf.PI / 180;
        forceToX = Mathf.Sin(rotationZ) * jumpHeight;
        forceToY = Mathf.Cos(rotationZ) * jumpHeight;
        if (collision.transform.tag == "Player")
        {
            Rigidbody2D colRigid = collision.gameObject.GetComponent<Rigidbody2D>();
            PlayerController2 colCon = collision.gameObject.GetComponent<PlayerController2>();
            colRigid.AddForce(new Vector2(colCon.getSpeed() * colCon.getHorizontal() + forceToX, forceToY), ForceMode2D.Impulse);
        }
    }
}
