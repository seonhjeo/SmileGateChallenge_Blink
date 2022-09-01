using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerater : MonoBehaviour
{
    public GameObject Ground;
    private GameObject myObject;

    private bool isResetted;
    // Start is called before the first frame update
    void Start()
    {
        myObject = Instantiate(Ground, this.transform.position, Quaternion.identity);
        isResetted = false;
    }

    void Update()
    {
        // 리셋당하면 현재 오브젝트 삭제하고 다시 생성
        if (WorldController.Instance.playerRestart && !isResetted)
        {
            if (!myObject)
                Destroy(myObject);
            myObject = Instantiate(Ground, this.transform.position, Quaternion.identity);
            isResetted = true;
        }
        if (!WorldController.Instance.playerRestart)
            isResetted = false;
    }
}
