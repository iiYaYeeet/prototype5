using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daynight : MonoBehaviour
{
    public GameObject sun, moon, overall,boat;
    public Light sunl, moonl;
    public ParticleSystem stars;
    public Material star;
    public float timeofday, daytimeframe;
    public bool day;

    public void Start()
    {
        stars.Emit(1000);
        var color = star.color;
        color.a = 0;
        star.color = color;
        storemanager.God.time = this;
    }

    public void FixedUpdate()
    {
        sun.transform.Rotate(new Vector3(daytimeframe*24,0,0));
        moon.transform.Rotate(new Vector3(daytimeframe*24,0,0));
        timeofday += daytimeframe;
        if (timeofday > 15)
        {
            timeofday = 0;
        }

        if (timeofday is >= 4f and <= 5f)
        {
            StartCoroutine(sunset());
        }
        if (timeofday is >= 11 and <= 12)
        {
            StartCoroutine(sunrise());
        }

        overall.transform.position = boat.transform.position;
    }

    public IEnumerator sunset()
    {
        if (sunl.intensity > 0)
        {
            sunl.intensity -= 0.005f;
            yield return new WaitForFixedUpdate();
            sunl.enabled = false;
        }
        if (moonl.intensity < 0.5)
        {
            moonl.enabled = true;
            moonl.intensity += 0.005f;
            yield return new WaitForFixedUpdate();
        }
        if (star.color.a <= 1)
        {
            var color = star.color;
            color.a += 0.05f;
            star.color = color;
            yield return new WaitForFixedUpdate();
        }
        day = false;
    }
    public IEnumerator sunrise()
    {
        if (moonl.intensity > 0)
        {
            moonl.intensity -= 0.005f;
            yield return new WaitForFixedUpdate();
            moonl.enabled = false;
        }
        if (sunl.intensity < 1)
        {
            
            sunl.enabled = true;
            sunl.intensity += 0.005f;
            yield return new WaitForFixedUpdate();
        }
        if (star.color.a >= 0)
        {
            var color = star.color;
            color.a -= 0.1f;
            star.color = color;
            yield return new WaitForFixedUpdate();
        }
        day = true;
    }
}
