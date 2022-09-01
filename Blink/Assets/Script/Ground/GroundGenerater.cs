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
        // ���´��ϸ� ���� ������Ʈ �����ϰ� �ٽ� ����
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
