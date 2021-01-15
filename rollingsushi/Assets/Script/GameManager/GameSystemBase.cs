using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemBase : MonoBehaviour
{

    public GameObject profit_text, disposal_text, time_text;
    public GameObject Gameclear, Gameover;
    public GameObject[] star = new GameObject[7];

    public AudioClip discard_SE;
    protected AudioSource Audio;

    private float volume;

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
        }

        get
        {
            return rep;
        }

    }
    private int rep = 4;



    //ゲームのシステムはこちらに

    protected void Start()
    {
        Audio = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("SE", 1.0f);
        Audio.volume *= volume;
    }
    public void Discard()
    {
        Disposal = 1;
        Audio.PlayOneShot(discard_SE);
        disposal_text.GetComponent<Text>().text = Disposal.ToString();
    }

    public void GainProfit(int price)
    {
        Profit = price;
        profit_text.GetComponent<Text>().text = Profit.ToString();
    }

    public void RaiseRep()
    {
        Rep += 1;
        Rep = Mathf.Clamp(Rep, 1, 7);
      //  Debug.Log("評判が上がりました");
        star[Rep - 1].SetActive(true);
    }

    public void LowerRep()
    {
        Rep -= 1;
        Rep = Mathf.Clamp(Rep, 1, 7);
        //Debug.Log("評判が下がりました");
        star[Rep].SetActive(false);
    }

    //ゲーム内のオブジェクトを一括で止める
    public void AllObjectFalse()
    {
        GameObject sushigenerator = GameObject.Find("SushiGenerator");
        sushigenerator.GetComponent<sushiGenerator>().enabled = false;

        GameObject eventmanager = GameObject.Find("EventManager");
        eventmanager.GetComponent<EventManager>().enabled = false;

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
            drag.transform.GetChild(1).GetComponent<Drag>().enabled = false;
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

        GameObject eventmanager = GameObject.Find("EventManager");
        eventmanager.GetComponent<EventManager>().enabled = false;

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
            drag.transform.GetChild(1).GetComponent<Drag>().enabled = true;
        }

        foreach (GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
        {
            drop.GetComponent<UnitManagr>().enabled = true;
        }
    }
}
