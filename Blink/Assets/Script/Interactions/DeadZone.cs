using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController2 myPlayer = collision.GetComponent<PlayerController2>();
            WorldController.Instance.playerRestart = true;
            myPlayer.MovetoSpot(WorldController.Instance.savePoints[0].transform.position);
        }
    }
}
