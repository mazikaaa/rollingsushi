using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaiHaste : Event
{
    GameObject[] sushigenerators;
    GameObject[] sushis;
    float[] rate;
    float[] ratestack = new float[16];
    int i, j;

    public override void InitEvent()
    {
        i = 0;
        j = 0;
        sushigenerators = GameObject.FindGameObjectsWithTag("sushigenerator");

        foreach (GameObject sushigenerator in sushigenerators)
        {
            sushis = sushigenerator.GetComponent<sushiGenerator>().sushis;
            rate = sushigenerator.GetComponent<sushiGenerator>().sushirate;
            foreach (GameObject sushi in sushis)
            {
                if (sushi.GetComponentInChildren<sushidata>().sushi_type == "kai")
                {
                    ratestack[j] = rate[i];//元の確率を保持しておく
                    rate[i] = ratestack[j] + 20.0f;
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
                if (sushi.GetComponentInChildren<sushidata>().sushi_type == "kai")
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
        return "貝祭り";
    }

    public override string GetText()
    {
        return "種類が「貝類」の寿司の出てくる確率が増えます";
    }
}
