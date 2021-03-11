using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyExpenSushi : Event
{
    GameObject[] sushigenerators;
    GameObject[] sushis;
    float[] rate;
    float[] ratestack = new float[16];
    List<int> prices = new List<int>();
    int i, j;

    public override void InitEvent()
    {
        sushigenerators = GameObject.FindGameObjectsWithTag("sushigenerator");
        foreach (GameObject sushigenerator in sushigenerators)
        {
            sushis = sushigenerator.GetComponent<sushiGenerator>().sushis;
        }

        prices.Clear();
        for (i = 0; i < sushis.Length; i++)
        {
            prices.Add(sushis[i].GetComponentInChildren<sushidata>().price);
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
            foreach (int price in prices)
            {
                if (price< 150)
                {
                    ratestack[j] = rate[i];//元の確率を保持しておく
                    rate[i] = 0;
                    j++;
                }
                i++;
            }
            i = 0;
            j = 0;
        }
    }

    public override void ExitEvent()
    {
        i = 0;
        j = 0;
        foreach (GameObject sushigenerator in sushigenerators)
        {
            sushis = sushigenerator.GetComponent<sushiGenerator>().sushis;
            rate = sushigenerator.GetComponent<sushiGenerator>().sushirate;
            foreach (int price in prices)
            {
                if (price < 150)
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
        return "高級志向";
    }

    public override string GetText()
    {
        return "150円以上の寿司しか登場しなくなる";
    }
}
