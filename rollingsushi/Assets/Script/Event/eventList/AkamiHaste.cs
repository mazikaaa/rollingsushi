﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkamiHaste : Event
{
    //赤身系統の寿司の生成確率を上げるイベント

    GameObject[] sushigenerators;
    GameObject[] sushis;
    float[] rate;
    float[] ratestack = new float[16];
    private List<string>sushitypes=new List<string>();
    int i, j;

    public  override void InitEvent()
    {
        sushigenerators = GameObject.FindGameObjectsWithTag("sushigenerator");
        foreach (GameObject sushigenerator in sushigenerators)
        {
            sushis = sushigenerator.GetComponent<sushiGenerator>().sushis;
        }

        sushitypes.Clear();
        for (i = 0; i < sushis.Length; i++)
        {
            sushitypes.Add(sushis[i].GetComponentInChildren<sushidata>().sushi_type);
        }
        
        
    }


    public override void ActionEvent()
    {
        i = 0;
        j = 0;
        foreach (GameObject sushigenerator in sushigenerators)
        {
            sushis = sushigenerator.GetComponent<sushiGenerator>().sushis;
            rate = sushigenerator.GetComponent<sushiGenerator>().sushirate;
            foreach (string sushitype in sushitypes)
            {
                if (sushitype == "akami")
                {
                    ratestack[j] = rate[i];//元の確率を保持しておく
                    rate[i] = ratestack[j] + 20.0f;
                    j++;
                }
                i++;
            }
            j = 0;
        }
    }

    public override void ExitEvent()
    {
        i = 0;
        j = 0;
        foreach (GameObject sushigenerator in sushigenerators)
        {
            foreach (string sushitype in sushitypes)
            {
                if (sushitype == "akami")
                {
                    rate[i] = ratestack[j];//元の確率に戻す
                    j++;
                }
                i++;
            }
            i = 0;
            j = 0;
        }
    }

    public override string GetTitle()
    {
        return "赤身祭り";
    }

    public override string GetText()
    {
        return "種類が「赤身」の寿司の出てくる確率が増えます";
    }
}
