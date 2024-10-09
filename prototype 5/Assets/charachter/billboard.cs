using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    Camera mainCamera;

void Start()
	{
    		mainCamera = Camera.main;
	}

void LateUpdate()
	{
    		transform.LookAt(new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z));
    		transform.Rotate(0, 180, 0);
	}
}
