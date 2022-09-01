using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 카메라컨트롤러 싱글톤화
    private static CameraController _instance;
    public static CameraController Instance { get { return _instance; } }

    private Vector3 camPos;
    private Vector2 playerPos;

    private float offset = 0.2f;

    private float camearDepth = -10f;

    private Transform myPlayer;
    private Camera myCamera;

    [Header("시작 시 카메라를 플레이어 기준에서 위치시킬 X좌표")]
    public float initialPosX;
    [Header("시작 시 카메라를 플레이어 기준에서 위치시킬 Y좌표")]
    public float initialPosY;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        myCamera = GetComponent<Camera>();
        myPlayer = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        camearDepth = myCamera.transform.position.z;
        SetCameraPos();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        moveCamera();
    }

    // 플레이어가 순간이동할 때 시행되는 카메라 움직임
    public void SetCameraPos()
    {
        myCamera.transform.position = myPlayer.transform.position + new Vector3(initialPosX, initialPosY, camearDepth);
    }

    public void SetCameraPos(Vector2 prevPos)
    {
        myCamera.transform.position = new Vector3(prevPos.x, prevPos.y, camearDepth);
    }

    // 플레이어가 걸어 이동할 때 시행되는 카메라 움직임
    private void moveCamera()
    {
        
        playerPos = myPlayer.position;
        camPos = myCamera.transform.position;

        if (playerPos.x - camPos.x >= myCamera.aspect * myCamera.orthographicSize - offset)
            camPos += new Vector3((myCamera.aspect * myCamera.orthographicSize - offset) * 2, 0, 0);
        if (playerPos.x - camPos.x <= -(myCamera.aspect * myCamera.orthographicSize - offset))
            camPos += new Vector3(-(myCamera.aspect * myCamera.orthographicSize - offset) * 2, 0, 0);
        if (playerPos.y - camPos.y >= myCamera.orthographicSize - offset)
            camPos += new Vector3(0, (myCamera.orthographicSize - offset) * 2, 0);
        if (playerPos.y - camPos.y <= -(myCamera.orthographicSize - offset))
            camPos += new Vector3(0, -(myCamera.orthographicSize - offset) * 2, 0);
        if (camPos.y < 2.8f)
            camPos.y = 2.8f;
        camPos.z = camearDepth;
        myCamera.transform.position = camPos;
    }
}
