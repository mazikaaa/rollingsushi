using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfitDown : Event
{
    GameObject gamemanager;

    public override void InitEvent()
    {
        gamemanager = GameObject.Find("GameManager");
    }

    public override void ActionEvent()
    {
        gamemanager.GetComponent<GameManager>().expensiveflag=true;
    }

    public override void ExitEvent()
    {
        gamemanager.GetComponent<GameManager>().expensiveflag= false;
    }

    public override string GetTitle()
    {
        return "ネタの高騰";
    }

    public override string GetText()
    {
        return "寿司のネタの高騰により,得られる利益が20％減少する";
    }
}
