using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPoison : Event
{
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
            unitmanager.poisonflag = true;
        }

    }

    public override void ExitEvent()
    {
        foreach (UnitManager unitmanager in unitmanagers)
        {
            unitmanager.poisonflag = false;
        }
    }

    public override string GetTitle()
    {
        return "食あたり";
    }

    public override string GetText()
    {
        return "寿司を食べた時に,低確率(5%)で「食あたり」が発生します.「食あたり」が発生すると，すぐに食事席から離れ評価を1段階下げます";
    }
}
