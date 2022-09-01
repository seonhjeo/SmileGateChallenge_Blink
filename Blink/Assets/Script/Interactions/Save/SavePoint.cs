using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteraction
{
    public Transform insideBuilding;
    private int spotNumber;
    public bool isFixed;
    public bool isStartPoint;

    public AudioSource doorOpen;

    // Start is called before the first frame update
    void Start()
    {
        spotNumber = WorldController.Instance.savePoints.IndexOf(this);
        isFixed = false;
        if (isStartPoint)
            isFixed = true;
    }

    public virtual void Interact(GameObject target)
    {
        if (target.tag == "Player" && !isStartPoint)
        {
            if (!insideBuilding)
                Debug.LogError("There is No Door to Enter!!!");
            PlayerController2 myPlayer = target.GetComponent<PlayerController2>();
            if (myPlayer != null)
            {
                if (doorOpen != null)
                    doorOpen.Play();
                myPlayer.tempSaveSpot = spotNumber;
                myPlayer.MovetoSpot(insideBuilding.position);
                Invoke("AvoidBlinkFunc", WorldController.Instance.fadingTime);
            }
        }
    }

    private void AvoidBlinkFunc()
    {
        WorldController.Instance.doBlinkFunc = false;
    }
}
