using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard1 : MonoBehaviour
{
    
        Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main;
        }

        void LateUpdate()
        {
            transform.LookAt(mainCamera.transform.position);
            transform.Rotate(0, 180, 0);
        }
    
}
