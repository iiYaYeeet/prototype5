using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockcontroller : MonoBehaviour
{
    //headbob stuff
    public float transitionSpeed = 10f;
    public float bobSpeed = 4.8f;
    public float bobAmount = 0.001f;
    float timer = Mathf.PI / 2;
    //objs
    public GameObject boat;
    public Quaternion camPos;
    public Quaternion restPosition;
    

    void Update()
    {
        timer += bobSpeed * Time.deltaTime;
        Quaternion newPosition = new Quaternion(Mathf.Sin(timer) * bobAmount,
                    boat.transform.localRotation.y,
                    restPosition.z,0); //abs val of y for a parabolic path
        camPos = newPosition;
        boat.transform.localRotation = camPos;
        if (timer > Mathf.PI *
            2) //completed a full cycle on the unit circle. Reset to 0 to avoid bloated values.
        {
            timer = 0;
        }
        boat.transform.localRotation = camPos;
    }
}
