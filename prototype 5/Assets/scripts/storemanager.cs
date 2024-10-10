using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class storemanager : MonoBehaviour
{
    public CanvasGroup over,openstoretext;
    public TMP_Text cash,swords,selltext,buytext,display_Text;
    public int cooldown, buyamount, sellamount;
    public float sellboundl, sellboundh, buyboundl, buyboundh, swordcount, money, sellp, buyp;
    public List<GameObject> storelist;
    public GameObject closeststore;
    public bool storeuiopen,openable;
    public bool cooled = true;
    public int avgFrameRate;
    public static class God
    {
        public static storemanager SM;
        public static playercont PC;
        public static boatcontroller BM;
    }

    //gamemanager set
    void Awake()
    {
        God.SM = this;
    }

    public void Update()
    { 
        float disttoclose=999;
        foreach (GameObject s in storelist)
        {
            if (Vector3.Distance(s.transform.position, God.PC.transform.position)<disttoclose)
            {
                disttoclose = Vector3.Distance(s.transform.position, God.PC.transform.position);
                closeststore = s;
            }
        }
        if (Vector3.Distance(closeststore.transform.position, God.PC.transform.position) < 8 && storeuiopen==false)
        {
            openstoretext.alpha = 1;
            openable=true;
        }
        else
        {
            openstoretext.alpha = 0;
            openable = false;
        }

        if (openable && Input.GetKeyDown(KeyCode.R) && cooled)
        {
            sellp = Mathf.Round(Random.Range(sellboundh, sellboundl) * 100f) / 100f;
            buyp = Mathf.Round(Random.Range(buyboundl, buyboundh) * 100f) / 100f;
            Updatedisplay();
            openable = false;
            storeuiopen = true;
            cooled = false;
            over.alpha = 1;
            God.PC.unlock();
            StartCoroutine(cd());
        }
        if (storeuiopen && Input.GetKeyDown(KeyCode.R) && cooled)
        {
            openable = true;
            storeuiopen = false;
            cooled = false;
            over.alpha = 0;
            God.PC.relock();
            StartCoroutine(cd());
        }
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        display_Text.text = avgFrameRate.ToString() + " FPS";
    }

    public IEnumerator cd()
    {
        yield return new WaitForSeconds(5);
        cooled = true;
    }

    public void buysword()
    {
        if (money >= buyp * buyamount)
        {
            money -= buyp * buyamount;
            swordcount += buyamount;
            Updatedisplay();
        }
    }
    public void sellsword()
    {
        if (swordcount >= sellamount)
        {
            money += sellp * sellamount;
            swordcount -= sellamount;
            Updatedisplay();
        }
    }

    public void updatesellamount(string s)
    {
        sellamount = Convert.ToInt32(s);
    }
    public void updatebuyamount(string b)
    {
        buyamount = Convert.ToInt32(b);
    }

    public void Updatedisplay()
    {
        sellp = Mathf.Round(sellp * 100f) / 100f;
        buyp = Mathf.Round(buyp * 100f) / 100f;
        money = Mathf.Round(money * 100f) / 100f;
        swordcount = Mathf.Round(swordcount * 100f) / 100f;
        selltext.text = "Sell price: " + sellp;
        buytext.text = "Buy price: " + buyp;
        cash.text = "Money: " + money;
        swords.text = "Swords: " + swordcount;
    }
}
