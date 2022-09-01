using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoAnimator : MonoBehaviour
{
    private Animator animator;
    private Image image;
    public event EventHandler EventAnimationEnd;
    void Awake()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
        animator.speed = 0f;
    }

    //------------------------------------------------------------------------------------
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            animator.speed = 0f;
            if(EventAnimationEnd != null)
            {
                EventAnimationEnd(gameObject, EventArgs.Empty);
            }
        }
    }

    //------------------------------------------------------------------------------------
    public void StartAnimation()
    {
        Debug.Log(animator.speed);
        animator.speed = 1f;
    }

    //------------------------------------------------------------------------------------
    public void SetComponentState(bool isAble)
    {
        animator.enabled = isAble;
        image.enabled = isAble;
    }
}
