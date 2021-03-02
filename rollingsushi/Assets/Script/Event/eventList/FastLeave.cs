using UnityEngine;
using System.Collections.Generic;

public class FastLeave : Event
{
    List<UnitManager> unitmanagers = new List<UnitManager>();
    public override void InitEvent()
    {
       foreach( GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
        {
            unitmanagers.Add(drop.GetComponent<UnitManager>());
        }
    }

    public override void ActionEvent()
    {
        foreach (UnitManager unitmanager in unitmanagers)
        {
           unitmanager.eventtime =5.0f;
        }
    }

    public override void ExitEvent()
    {
        foreach (UnitManager unitmanager in unitmanagers)
        {
            unitmanager.eventtime = 0.0f;
        }
    }

    public override string GetTitle()
    {
        return "多忙な平日";
    }

    public override string GetText()
    {
        return "食事席に着席している時間が\n"
               +"短くなります\n";
    }
}
