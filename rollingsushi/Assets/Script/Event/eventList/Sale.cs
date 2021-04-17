using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sale : Event
{

    //ここまで得られた利益が30%減る代わりに、お客さんの来るペースが上がる

    GameObject gamemanager;
    List<Drag> drags = new List<Drag>();

    public override void InitEvent()
    {
        gamemanager = GameObject.Find("GameManager");

        foreach (GameObject drag in GameObject.FindGameObjectsWithTag("drag"))
        {
            drags.Add(drag.transform.GetComponentInChildren<Drag>());
        }
    }

    public override void ActionEvent()
    {
        gamemanager.GetComponent<GameManager>().saleflag = true;

        foreach (Drag drag in drags)
        {
            drag.eventplustime = -3.0f;
        }
    }

    public override void ExitEvent()
    {
        gamemanager.GetComponent<GameManager>().saleflag = false;

        foreach (Drag drag in drags)
        {
            drag.eventplustime = 0.0f;
        }
    }

    public override string GetTitle()
    {
        return "セール";
    }

    public override string GetText()
    {
        return "利益が30％減少する代わりに,待ち席にお客が来るまでの時間が短くなります。";
    }
}
