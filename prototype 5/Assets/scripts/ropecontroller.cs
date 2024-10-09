using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropecontroller : MonoBehaviour
{
    public LineRenderer LR;
    public Transform[] points;

    public void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            LR.SetPosition(i,points[i].position);
        }
    }
}
