﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyCheapSushi : Event
{
    GameObject[] sushigenerators;
    GameObject[] sushis;
    float[] rate;
    float[] ratestack=new float[16];
    int i,j;

    public override void InitEvent()
    {
        i = 0;
        j = 0;
        sushigenerators = GameObject.FindGameObjectsWithTag("sushigenerator");

        foreach (GameObject sushigenerator in sushigenerators)
        {
            sushis = sushigenerator.GetComponent<sushiGenerator>().sushis;
            rate = sushigenerator.GetComponent<sushiGenerator>().sushirate;
            foreach(GameObject sushi in sushis)
            {
                if (sushi.GetComponentInChildren<sushidata>().price >= 150)
                {
                    ratestack[j] = rate[i];//元の確率を保持しておく
                    rate[i] = 0;
                    j++;
                }
                i++;
            }
        }
    }

    public override void ExitEvent()
    {
        i = 0;
        j = 0;
        foreach (GameObject sushigenerator in sushigenerators)
        {
            foreach (GameObject sushi in sushis)
            {
                if (sushi.GetComponentInChildren<sushidata>().price >= 150)
                {
                    rate[i] = ratestack[j];//元の確率に戻す
                    j++;
                }
                i++;
            }
        }
    }

    public override string GetTitle()
    {
        return "お財布に優しく";
    }

    public override string GetText()
    {
        return "150円未満の寿司しか登場しなくなります";
    }
}
