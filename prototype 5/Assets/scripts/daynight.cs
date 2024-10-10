using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daynight : MonoBehaviour
{
    public GameObject sun, moon, overall,boat;
    public Light sunl, moonl;
    public ParticleSystem stars;
    public float timeofday, daytimeframe;

    public void FixedUpdate()
    {
        sun.transform.Rotate(new Vector3(daytimeframe*24,0,0));
        moon.transform.Rotate(new Vector3(daytimeframe*24,0,0));
        timeofday += daytimeframe;
        if (timeofday > 15)
        {
            timeofday = 0;
        }

        if (timeofday >= 4)
        {
            sunl.enabled = false;
            moonl.enabled = true;
        }
        if (timeofday >= 11)
        {
            sunl.enabled = true;
            moonl.enabled = false;
        }

        overall.transform.position = boat.transform.position;
    }
}
