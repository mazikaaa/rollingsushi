using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claim : Event
{
    GameObject gamemanager,eventmanager;

    public override void InitEvent()
    {
        gamemanager = GameObject.Find("GameManager");
        eventmanager = GameObject.Find("EventManager");

        gamemanager.GetComponent<GameManager>().LowerRep();
        eventmanager.GetComponent<EventManager>().eventTime += 30.0f;
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
            "ネット上で拡散され評判が1下がる";
    }

}
