using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPoison : Event
{
    GameObject[] drops;

    public override void InitEvent()
    {
        drops = GameObject.FindGameObjectsWithTag("drop");

        foreach (GameObject drop in drops)
        {
            drop.GetComponent<UnitManager>().poisonflag = true;
        }

    }

    public override void ExitEvent()
    {
        foreach (GameObject drop in drops)
        {
            drop.GetComponent<UnitManager>().poisonflag = false;
        }
    }

    public override string GetTitle()
    {
        return "食あたり";
    }

    public override string GetText()
    {
        return "寿司を食べた時に,低確率(5%)で「食あたり」が発生します\n"
               + "「食あたり」が発生すると，すぐに食事席から離れ評価を1段階下げます";
    }
}
