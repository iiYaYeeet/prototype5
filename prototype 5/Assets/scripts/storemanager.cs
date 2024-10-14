using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class storemanager : MonoBehaviour
{
    public List<constelationcontroller> constelations;
    public GameObject hitcon;
    public static class God
    {
        public static storemanager SM;
        public static playercont PC;
        public static boatcontroller BM;
        public static daynight time;
    }

    //gamemanager set
    void Awake()
    {
        God.SM = this;
    }
}
