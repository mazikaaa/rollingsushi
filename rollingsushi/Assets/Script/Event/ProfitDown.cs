using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfitDown : Event
{
    GameObject gamemanager,eventmanager;
    int profit=0;

   public override void InitEvent()
    {
        gamemanager = GameObject.Find("GameManager");
        eventmanager = GameObject.Find("EventManager");

        profit = gamemanager.GetComponent<GameManager>().Profit;
        gamemanager.GetComponent<GameManager>().Profit = Ceiling_Hun(profit*0.8);
        eventmanager.GetComponent<EventManager>().eventTime += 30.0f;
    
    }

    public override void ExitEvent()
    {
 
    }

    public override string GetTitle()
    {
        return "ネタの高騰";
    }

    public override string GetText()
    {
        return "寿司のネタの高騰により,利益が20％減少する";
    }

    public int Ceiling_Hun(double num)
    {
        double money = num;
        int prof = 0;

        while (money > 1000)
        {
            money -= 1000;
            prof += 1000;
        }

        while (money > 100)
        {
            money -= 100;
            prof += 100;
        }

        return prof;
    }
}
