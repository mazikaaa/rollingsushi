using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claim : Event
{
    //評判を強制的に、１段階下げるイベント

    GameObject gamemanager,eventmanager;

    public override void InitEvent()
    {
        gamemanager = GameObject.Find("GameManager");
        eventmanager = GameObject.Find("EventManager");
    }

    public override void ActionEvent()
    {
        gamemanager.GetComponent<GameManager>().LowerRep();
        //このイベントは評価を下げるだけで終わるため、少し次のイベントまで時間を短くする
        eventmanager.GetComponent<EventManager>().eventTime +=20.0f;
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
