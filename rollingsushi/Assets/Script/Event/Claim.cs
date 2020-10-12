using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claim : Event
{
    GameObject gamemanager;
    int profit = 0;

    public override void InitEvent()
    {
        gamemanager = GameObject.Find("GameManager");

        gamemanager.GetComponent<GameManager>().LowerRep();
        gamemanager.GetComponent<GameManager>().LowerRep();
    }

    public override void ExitEvent()
    {

    }

    public override string GetTitle()
    {
        return "クレーム発生";
    }

    public override string GetText()
    {
        return "当店を使ったお客様からクレームが発生してしまった\n"+
            "ネット上で拡散され評判が2下がる";
    }

}
