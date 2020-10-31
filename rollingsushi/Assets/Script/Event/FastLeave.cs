﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastLeave : Event
{
    GameObject[] drops;

    public override void InitEvent()
    {
       drops = GameObject.FindGameObjectsWithTag("drop");

        foreach (GameObject drop in drops)
        {
           drop.GetComponent<UnitManagr>().eventtime =5.0f;
        }

    }

    public override void ExitEvent()
    {
        foreach (GameObject drop in drops)
        {
            drop.GetComponent<UnitManagr>().eventtime = 0.0f;
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
