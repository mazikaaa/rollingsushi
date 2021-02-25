using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowLeave : Event
{
    GameObject[] drops;

    public override void InitEvent()
    {
        drops = GameObject.FindGameObjectsWithTag("drop");

        foreach (GameObject drop in drops)
        {
            drop.GetComponent<UnitManager>().eventtime = -5.0f;
        }

    }

    public override void ExitEvent()
    {
        foreach (GameObject drop in drops)
        {
            drop.GetComponent<UnitManager>().eventtime = 0.0f;
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
