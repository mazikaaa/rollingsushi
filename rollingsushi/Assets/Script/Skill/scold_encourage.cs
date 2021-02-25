using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scold_encourage : Skill
{
    int eatamount;

    public override void LeaveSkill(int amount ,GameManager gamemanager, UnitManager unitmanager) {

        this.eatamount = unitmanager.eatamount;

        if ((int)eatamount * 0.8f <= amount)
        {
            gamemanager.RaiseRep();
        }
        else if ((int)eatamount * 0.5f >= amount)
        {
            gamemanager.LowerRep();
        }
    }
}
