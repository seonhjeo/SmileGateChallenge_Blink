using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public bool canMove;
    public bool canJump;
    public bool isGrounded;
    public bool isSlippered;
    public bool isSloped;
    public bool isSuperJump;

    private void Update()
    {
        canMove = isGrounded || isSlippered || isSuperJump;
        canJump = isGrounded || isSloped || isSlippered;

        if (isGrounded && isSloped)
            isSloped = false;
        if (isGrounded && isSlippered)
            isSlippered = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isGrounded)
        {
            switch (collision.tag)
            {
                case "Ground":
                    isGrounded = true;
                    break;
                case "LadderGround":
                    isGrounded = true;
                    break;
                case "Slope":
                    isSloped = true;
                    break;
                case "Slipper":
                    isSlippered = true;
                    break;
                case "SuperJump":
                    isSuperJump = true;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Ground":
                isGrounded = false;
                break;
            case "LadderGround":
                isGrounded = false;
                break;
            case "Slope":
                isSloped = false;
                break;
            case "Slipper":
                isSlippered = false;
                break;
            case "SuperJump":
                isSuperJump = false;
                break;
            default:
                break;
        }
    }
}
