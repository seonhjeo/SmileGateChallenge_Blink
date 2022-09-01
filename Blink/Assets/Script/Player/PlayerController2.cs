using Script.Item;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private Animator myAnim;
    private BoxCollider2D myCol;
    private Rigidbody2D myRigid;
    private GroundCheck myGround;
    private WallCheck myWall;
    private PlayerBlink myBlink;

    [HideInInspector]
    public PhysicsMaterial2D groundPM;
    [HideInInspector]
    public PhysicsMaterial2D slipperPM;

    [Header("�̵��ӵ�")]
    public float speed = 6f;
    [Header("������")]
    public float jumpForce = 12f;
    [Header("��ư ������ ���� ������ ����")]
    public float jumpDiff = 0.6f;

    private int horizontal = 0;
    [HideInInspector]
    public int tempSaveSpot = 0;

    private KeySetting keySetting;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myCol = GetComponent<BoxCollider2D>();
        myRigid = GetComponent<Rigidbody2D>();
        myGround = transform.GetChild(0).GetComponent<GroundCheck>();
        myWall = transform.GetChild(1).GetComponent<WallCheck>();
        myBlink = GetComponent<PlayerBlink>();
        keySetting = FindObjectOfType<KeySetting>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.instance.isNewGame)
        {
            string fName = string.Format(Application.streamingAssetsPath + "/DataFiles", "PlayerInfo");
            if (File.Exists(fName))
            {
                PlayerInfo info = GameManager.instance.fileIOHelper.LoadJsonFile<PlayerInfo>(Application.streamingAssetsPath + "/DataFiles", "PlayerInfo");
                WorldController.Instance.hightestSpot = info.saveSpot;
                transform.position = WorldController.Instance.savePoints[info.saveSpot].transform.position;
            }    
        }
        else
            transform.position = WorldController.Instance.savePoints[0].transform.position;
        CameraController.Instance.SetCameraPos();
        speed = 6f;
        jumpForce = 12f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!WorldController.Instance.getIsPause() && WorldController.Instance.playerCanMove)
        {
            GetHorizontalDirection();
            ChangeFriction();
            Move();
            PlayerAnimation();
        }
    }

    // �¿� �Է¿� ���� ���� �޾ƿ��� �Լ�
    private void GetHorizontalDirection()
    {
        if (Input.GetKey(keySetting.UserKey[KeyAction.LEFT]) && Input.GetKey(keySetting.UserKey[KeyAction.RIGHT]))
            horizontal = 0;
        else if (Input.GetKey(keySetting.UserKey[KeyAction.LEFT]))
            horizontal = -1;
        else if (Input.GetKey(keySetting.UserKey[KeyAction.RIGHT]))
            horizontal = 1;
        else
            horizontal = 0;
    }

    // ���� ��� �ִ� ������ ������ ���� �������� �ٲ�
    private void ChangeFriction()
    {
        if (myGround.isGrounded || !myGround.canJump)
            myCol.sharedMaterial = groundPM;
        else
            myCol.sharedMaterial = slipperPM;
    }

    // ������ ���� �Լ�
    private void Move()
    {
        // �� ������Ʈ�� ������ �� �ְų�, ������ �� ������ y������ �������� ������ �¿� ������ ����
        if (myGround.canMove || (!myGround.canMove && myRigid.velocity.y == 0f && !myWall.hitWall))
            myRigid.AddForce(new Vector2(horizontal * speed * 5, 0f), ForceMode2D.Force);

        if (myRigid.velocity.x > speed)
            myRigid.velocity = new Vector2(speed, myRigid.velocity.y);
        else if (myRigid.velocity.x < -speed)
            myRigid.velocity = new Vector2(-speed, myRigid.velocity.y);

        DoWallJump(horizontal);
        
        // ���� �� ����Ű ���� ������ ���� ������ ����
        if (myGround.canJump && Input.GetKeyDown(keySetting.UserKey[KeyAction.JUMP]))
        {
            myRigid.AddForce(new Vector2(myRigid.velocity.x, jumpForce), ForceMode2D.Impulse);
            myBlink.jump.Play();
        }
        if (Input.GetKeyUp(keySetting.UserKey[KeyAction.JUMP]) && myRigid.velocity.y > 0f)
            myRigid.velocity = (new Vector2(myRigid.velocity.x, myRigid.velocity.y * jumpDiff));
    }

    // �� ������Ʈ�� ���� ����ְ�, ������ �� ������ ���� �ݴ� �������� �������� ������
    private void DoWallJump(float horizontal)
    {
        if (myWall.hitWall && !myGround.canMove)
        {
            if (myWall.hitRightWall && horizontal < 0 && Input.GetKeyDown(keySetting.UserKey[KeyAction.JUMP]))
            {
                myRigid.AddForce(new Vector2(-speed, jumpForce), ForceMode2D.Impulse);
                myBlink.jump.Play();
            }
            else if (myWall.hitLeftWall && horizontal > 0 && Input.GetKeyDown(keySetting.UserKey[KeyAction.JUMP]))
            {
                myRigid.AddForce(new Vector2(speed, jumpForce), ForceMode2D.Impulse);
                myBlink.jump.Play();
            }
        }
    }

    private void PlayerAnimation()
    {
        myBlink.holding = false;
        myAnim.SetBool("isHolding", false);
        myBlink.sliding = false;
        myAnim.SetBool("isSliding", false);
        myBlink.jumping = false;

        if (myRigid.velocity.x < -0.1f && transform.localScale.x > 0f)
            transform.localScale = transform.localScale - new Vector3(transform.localScale.x * 2, 0f, 0f);
        if (myRigid.velocity.x > 0.1f && transform.localScale.x < 0f)
            transform.localScale = transform.localScale - new Vector3(transform.localScale.x * 2, 0f, 0f);

        if (myGround.isGrounded || myGround.isSlippered)
            myAnim.SetBool("isJumping", false);
        else
        {
            myAnim.SetBool("isJumping", true);
            myBlink.jumping = true;
        }
           

        if (!(myGround.isGrounded || myGround.isSlippered) && myWall.hitWall)
        {
            myAnim.SetBool("isHolding", true);
            myBlink.holding = true;
        }
        if (myGround.isSloped && myRigid.velocity.y != 0f)
        {
            myAnim.SetBool("isSliding", true);
            myBlink.sliding = true;
        }
        else if (myGround.isSloped && myRigid.velocity.y == 0f)
        {
            myAnim.SetBool("isJumping", false);
            myAnim.SetBool("isWalking", false);
            myBlink.sliding = false;
            myBlink.walking = false;
        }

        if (myGround.canMove && horizontal == 0)
        {
            myAnim.SetBool("isWalking", false);
            myBlink.walking = false;
        }

        if (myGround.canMove && horizontal != 0)
        {
            myAnim.SetBool("isWalking", true);
            myBlink.walking = true;
        }

    }

    // �÷��̾ ���� �̵�(�ǹ� ���� �̵� ��)�� ���ϴ� ��Ȳ�� �� ȣ��Ǵ� �Լ�.
    public void MovetoSpot(Vector2 pos)
    {
        WorldController.Instance.playerCanMove = false;
        myRigid.bodyType = RigidbodyType2D.Static;
        Fade.Instance.FadeOut();
        StartCoroutine(MoveSpot(pos));
        publicFadeIn();
    }

    public void publicFadeIn()
    {
        Invoke("FadeIn", WorldController.Instance.fadingTime * 2);
    }

    private void FadeIn()
    {
        Fade.Instance.FadeIn();
        myRigid.bodyType = RigidbodyType2D.Dynamic;
        if (WorldController.Instance.playerRestart)
            WorldController.Instance.playerRestart = false;
        WorldController.Instance.playerCanMove = true;
    }


    private IEnumerator MoveSpot(Vector2 pos)
    {
        yield return new WaitForSeconds(WorldController.Instance.fadingTime);
        transform.position = pos;
        CameraController.Instance.SetCameraPos();
    }

    private void OnApplicationQuit()
    {
        PlayerInfo info = new PlayerInfo(this);
        var jsonData = JsonUtility.ToJson(info);
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.streamingAssetsPath + "/DataFiles", "PlayerInfo", jsonData);
    }

    public int getHorizontal()
    {
        return horizontal;
    }
    
    public float getSpeed()
    {
        return speed;
    }
}