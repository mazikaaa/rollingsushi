using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemBase : MonoBehaviour
{
    public int Disposal
    {
        set
        {
            disposal += value;
        }
        get
        {
            return disposal;
        }
    }
    private int disposal = 0;

    public int Profit
    {
        set
        {
            profit += value;
        }

        get
        {
            return profit;
        }

    }
    private int profit = 0;

    //ユニットの生成時間に上下させる変数。
    public int Rep
    {
        set
        {
            rep = value;
            Debug.Log(rep);
        }

        get
        {
            return rep;
        }

    }
    private int rep = 5;


    public GameObject profit_text, disposal_text,rep_text,time_text;
    public GameObject Gameclear, Gameover;

    public AudioClip discard_SE;
    private AudioSource audiosource;

    //ゲームのシステムはこちらに

    protected void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void GainProfit(int price)
    {
        Profit = price;
        profit_text.GetComponent<Text>().text = Profit.ToString();
    }

    public void Discard()
    {
        Disposal = 1;
        audiosource.PlayOneShot(discard_SE);
        disposal_text.GetComponent<Text>().text = Disposal.ToString();
    }

    public void RaiseRep()
    {
        Rep += 1;
        Debug.Log("評判が上がりました");
        rep_text.GetComponent<Text>().text = Rep.ToString();
    }

    public void LowerRep()
    {
        Rep -= 1;
        Debug.Log("評判が下がりました");
        rep_text.GetComponent<Text>().text = Rep.ToString();
    }

    //ゲーム内のオブジェクトを一括で止める
    public void AllObjectFalse()
    {
        GameObject sushigenerator = GameObject.Find("SushiGenerator");
        sushigenerator.GetComponent<sushiGenerator>().enabled=false;

        GameObject dragingobject = GameObject.FindGameObjectWithTag("dragingobject");
        if (dragingobject)
        {
            dragingobject.SetActive(false);
        }

        foreach (GameObject sushi in GameObject.FindGameObjectsWithTag("sushi"))
        {
            sushi.GetComponent<sushi>().enabled = false;
        }

        foreach (GameObject drag in GameObject.FindGameObjectsWithTag("drag"))
        {
            drag.transform.GetChild(0).GetComponent<Drag>().enabled = false;
        }

        foreach (GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
        {
            drop.GetComponent<UnitManagr>().enabled = false;
        }
    }

    //停止させたオブジェクトを一括で動かす
    public void AllObjectTrue()
    {
        GameObject sushigenerator = GameObject.Find("SushiGenerator");
        sushigenerator.GetComponent<sushiGenerator>().enabled = true;

        GameObject dragingobject = GameObject.FindGameObjectWithTag("dragingobject");
        if (dragingobject)
        {
            dragingobject.SetActive(true);
        }

        foreach (GameObject sushi in GameObject.FindGameObjectsWithTag("sushi"))
        {
            sushi.GetComponent<sushi>().enabled = true;
        }

        foreach (GameObject drag in GameObject.FindGameObjectsWithTag("drag"))
        {
            drag.transform.GetChild(0).GetComponent<Drag>().enabled = true;
        }

        foreach (GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
        {
            drop.GetComponent<UnitManagr>().enabled = true;
        }
    }
}
