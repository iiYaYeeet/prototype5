using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class boatcontroller : MonoBehaviour
{
    public GameObject sail,sail2,sail3;
    public float maxspeed;
    public float rotspeed;
    public Rigidbody RB;
    public float saildepth,saildepth2;
    public bool sailsdown, anchor;
    public float helmturnpos,anchordepth;
    public GameObject helm;
    public GameObject capstan;
    public GameObject indicator;
    public AudioSource helmas, sailas, boatas, anchoras;
    public AudioClip raiseanchorac, dropanchorad, wheelac, saildropad, sailraisead, backgroundac;
    public float transitionSpeed = 10f;
    public float bobSpeed = 4.8f;
    public float bobAmount = 0.001f;
    float timer = Mathf.PI / 2;
    public GameObject boat;
    
    public void Awake()
    {
        storemanager.God.BM = this;
    }

    public void FixedUpdate()
    {
        if (RB.velocity.magnitude > 75 && anchor==false)
        {
            RB.drag = 5;
        }
        else if (anchor)
        {
            RB.drag = 2;
        }
        else if (anchor==false)
        {
            RB.drag = 0.55f;
        }

        bobSpeed = RB.velocity.magnitude / 10+0.5f;
        bobAmount = RB.velocity.magnitude / 5+0.1f;
        timer += bobSpeed * Time.deltaTime;
        if (timer > Mathf.PI * 2) //completed a full cycle on the unit circle. Reset to 0 to avoid bloated values.
        {
            timer = 0;
        }

        float bob = Mathf.Sin(timer) * bobAmount;
        if (anchor == false)
        {
            RB.AddTorque(new Vector3(0, 180, 0) * helmturnpos / 20, ForceMode.Acceleration);
        }
        RB.AddTorque(boat.transform.right * bob, ForceMode.Force);
        

        if (saildepth >= 1.5f)
        {
            saildepth = 1.5f;
        }
        if (saildepth <= 0)
        {
            saildepth = 0;
        }
        if (saildepth2 >= 2f)
        {
            saildepth2 = 2f;
        }
        if (saildepth2 <= 0)
        {
            saildepth2 = 0;
        }
        if (helmturnpos <=0.05 && helmturnpos >=-0.05 && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            helmturnpos = 0;
        }
        if (helmturnpos >= 2)
        {
            helmturnpos = 2;
        }
        if (helmturnpos <= -2)
        {
            helmturnpos = -2;
        }
        helm.transform.localRotation = Quaternion.Euler(helmturnpos*-180,0,90);
        sail.transform.localScale =
            new Vector3(saildepth / 2 + 0.1f, sail.transform.localScale.y, saildepth / 3 + 0.1f);
        sail2.transform.localScale =
            new Vector3(saildepth2 / 2 + 0.1f, sail2.transform.localScale.y, saildepth2 / 3 + 0.1f);
        sail3.transform.localScale =
            new Vector3(saildepth / 3 +0.5f, saildepth / 3 + 0.75f, saildepth / 3 + 0.2f);
        if (sailsdown && anchor==false)
        {
            float sails = (saildepth+saildepth2) * 6;
            RB.AddForce(transform.right * sails);
        }
    }

    public void Update()
    {
        if (storemanager.God.PC.onhelm)
        {
            if (Input.GetKey(KeyCode.D))
            {
                helmturnpos += 0.75f*Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.A))
            {
                helmturnpos -= 0.75f*Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                helmas.PlayOneShot(wheelac);
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                helmas.Stop();
            }
        }
        if (storemanager.God.PC.onsail || storemanager.God.PC.onsail2)
        {
            if (Input.GetKey(KeyCode.S))
            {
                saildepth += 1.5f*Time.deltaTime;
                saildepth2 += 2f*Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.W))
            {
                saildepth -= 0.65f*Time.deltaTime;
                saildepth2 -= 0.65f*Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                sailas.PlayOneShot(sailraisead);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                sailas.PlayOneShot(saildropad);
            }
            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
            {
                sailas.Stop();
            }
        }
    }

    public IEnumerator dropanchor()
    {
        anchoras.PlayOneShot(dropanchorad);
        
        var vector3 = indicator.transform.localPosition;
        while (anchordepth<30)
        {
            capstan.transform.Rotate(0,4,0,Space.Self);
            vector3.y += -0.04f;
            anchordepth += 0.30f;
            RB.AddTorque(new Vector3(0,180,0) * helmturnpos/12,ForceMode.Acceleration);
            indicator.transform.localPosition = vector3;
            yield return new WaitForFixedUpdate();
        }
        anchor = true;
    }
    public IEnumerator raiseanchor()
    {
        anchoras.PlayOneShot(raiseanchorac);
        var vector3 = indicator.transform.localPosition;
        while (anchordepth>0)
        {
            capstan.transform.Rotate(0,-2,0,Space.Self);
            vector3.y += 0.02f;
            anchordepth -= 0.15f;
            indicator.transform.localPosition = vector3;
            yield return new WaitForFixedUpdate();
        }
        anchor = false;
    }
}
