using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constelationcontroller : MonoBehaviour
{
    public bool litup;
    public SpriteRenderer SR;
    public Camera Eyes;
    public Animator anim;

    public void Update()
    {
        if (storemanager.God.time.day == false)
        {
            var color = SR.color;
            color.a = 255;
            SR.color = color;
            Vector3 viewPos = Eyes.WorldToViewportPoint(transform.position);
            if (viewPos.x >= 0.2f && viewPos.x <= 0.8f && viewPos.y >= 0.2f && viewPos.y <= 0.7f && viewPos.z >= 0)
            {
                anim.SetBool("lookingat", true);
            }
            else
            {
                anim.SetBool("lookingat", false);
            }
        }
        else
        {
            var color = SR.color;
            color.a = 0;
            SR.color = color;
            anim.SetBool("lookingat", false);
        }
    }
}
