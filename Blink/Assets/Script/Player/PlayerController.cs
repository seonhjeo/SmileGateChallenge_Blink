using System;
using System.Collections;
using System.Collections.Generic;
using Script.Item;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigid;
    private GroundCheck myGround;

    private int movePositionX;
    private Vector2 movePos;
    private float jumpForce;
    private bool isCharging;

    [Header("점프 풀차징까지 걸리는 시간")]
    public float chargeTime;
    [Header("최대 점프력")]
    public float maxJumpForce;
    [Header("점프의 반탄력")]
    public float bounceForce;
    [Header("플레이어 움직임 속도")]
    public float moveSpeed;
    [Header("바닥 바운스 스위치")]
    public bool checkBoundOption;


    private void Awake()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myGround = transform.GetChild(0).GetComponent<GroundCheck>();
    }
    // Start is called before the first frame update
    void Start()
    {
        movePositionX = 0;
        maxJumpForce = 24f;
        jumpForce = 0f;
        moveSpeed = 6f;
        chargeTime = 1f;
        bounceForce = 0.5f;
        checkBoundOption = false;
        isCharging = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        GetMovePositionX();
        Move();
        Jump();
        movePos = myRigid.velocity;
    }

    // 입력값을 받는 함수
    private void GetMovePositionX()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            movePositionX = -1;
        else if (Input.GetKey(KeyCode.RightArrow))
            movePositionX = 1;
        else
            movePositionX = 0;
    }

    // 움직임 구현 함수
    private void Move()
    {
        if (!isCharging && myGround.isGrounded)
            myRigid.velocity = new Vector2(moveSpeed * movePositionX, myRigid.velocity.y);
        else if (isCharging)
            myRigid.velocity = new Vector2(0, myRigid.velocity.y);
    }

    // 점프 구현 함수
    private void Jump()
    {
        if (myGround.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isCharging = true;
                jumpForce += maxJumpForce * (1 / chargeTime) * Time.deltaTime;
                if (jumpForce > maxJumpForce)
                    jumpForce = maxJumpForce;
            }
            if (isCharging && !Input.GetKey(KeyCode.Space))
            {
                isCharging = false;
                myRigid.velocity = new Vector2(moveSpeed * movePositionX, jumpForce);
                jumpForce = 0;
            }
        }
    }

    // 벽과의 충돌시 실행되는 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!myGround.isGrounded)
        {
            ContactPoint2D con = collision.contacts[0];
            Vector2 temp = con.point - Get2DPos.Get2DPosition(transform.position);
            if (Mathf.Sign(temp.x) != Mathf.Sign(temp.y))
                myRigid.velocity = new Vector2(-movePos.x, movePos.y) * bounceForce;
            else
                myRigid.velocity = new Vector2(movePos.x, -movePos.y) * bounceForce;
        }
    }

}
