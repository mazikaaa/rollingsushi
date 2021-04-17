using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowLeave : Event
{
    //お客さんが離席するまでの時間が長くなるイベント

    List<UnitManager> unitmanagers = new List<UnitManager>();

    public override void InitEvent()
    {
        foreach (GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
        {
            unitmanagers.Add(drop.GetComponent<UnitManager>());
        }
    }

    public override void ActionEvent()
    {
        foreach (UnitManager unitmanager in unitmanagers)
        {
            unitmanager.eventtime = -5.0f;
        }
    }

    public override void ExitEvent()
    {
        foreach (UnitManager unitmanager in unitmanagers)
        {
            unitmanager.eventtime =0.0f;
        }
    }

    public override string GetTitle()
    {
        return "優雅な休日";
    }

    public override string GetText()
    {
        return "食事席に着席している時間が長くなります\n";
    }
}
