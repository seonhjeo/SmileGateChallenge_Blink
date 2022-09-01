using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGController : MonoBehaviour
{
    private PlayerController2 myPlayer;
    private Image myImage;
    private List<float> savePointPos = new List<float>();

    private float camPosY;

    public Sprite innerBuilding;
    public Sprite darkSprite;
    public Sprite brightSprite;
    public Sprite low1;
    public Sprite low2;
    public Sprite middle1;
    public Sprite middle2;
    public Sprite high1;
    public Sprite high2;
    public Sprite in_Low1;
    public Sprite in_Low2;
    public Sprite in_Middle1;
    public Sprite in_Middle2;
    public Sprite in_High1;
    public Sprite in_High2;

    private void Awake()
    {
        myImage = GetComponent<Image>();
        myPlayer = GameObject.Find("Player").GetComponent<PlayerController2>();
    }

    private void Start()
    {
        foreach (SavePoint t in WorldController.Instance.savePoints)
        {
            if (t != null)
                savePointPos.Add(t.gameObject.transform.position.y);
        }
        savePointPos.RemoveAt(0);
        savePointPos.Add(GameObject.Find("EndPoint").transform.position.y);
        savePointPos.Sort();

        List<float> temp = new List<float>();
        for (int i = 1; i < savePointPos.Count; i++)
            temp.Add((savePointPos[i] - savePointPos[i - 1]) / 2 + savePointPos[i - 1]);
        foreach (float t in temp)
            savePointPos.Add(t);
        temp.Clear();

        savePointPos.Sort();
        savePointPos.Reverse();
    }

    // Update is called once per frame
    void Update()
    {
        camPosY = CameraController.Instance.gameObject.transform.position.y;

        if (!WorldController.Instance.doBlinkFunc)
        {
            if (myPlayer.tempSaveSpot == 0 || myPlayer.tempSaveSpot == 1)
                myImage.sprite = in_Low1;
            else if (myPlayer.tempSaveSpot == 2)
                myImage.sprite = in_Middle1;
            else if (myPlayer.tempSaveSpot == 3)
                myImage.sprite = in_Middle2;
            else
                myImage.sprite = in_High1;
        }
        else
        {
            if (WorldController.Instance.getWorldBlackOut())
                myImage.sprite = darkSprite;
            else
            {
                if (camPosY > savePointPos[0])
                    myImage.sprite = brightSprite;
                else if (camPosY > savePointPos[1])
                    myImage.sprite = high2;
                else if (camPosY > savePointPos[2])
                    myImage.sprite = high1;
                else if (camPosY > savePointPos[4])
                    myImage.sprite = middle2;
                else if (camPosY > savePointPos[6])
                    myImage.sprite = middle1;
                else if (camPosY > savePointPos[7])
                    myImage.sprite = low2;
                else
                    myImage.sprite = low1;
            }
        }    
    }
}
