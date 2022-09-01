using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteraction
{
    public AudioSource doorOpen;

    public virtual void Interact(GameObject target)
    {
        if (target.tag == "Player")
        {
            doorOpen.Play();
            PlayerController2 myPlayer = target.GetComponent<PlayerController2>();
            myPlayer.MovetoSpot(WorldController.Instance.savePoints[myPlayer.tempSaveSpot].transform.position);
            WorldController.Instance.doBlinkFunc = true;
        }
    }
}
