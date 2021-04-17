using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scold_encourage : Skill
{
    int eatamount;

    //離席する時の、「評判」の上げ下げが2倍になる(2段階上がるか、2段階下がるか)
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
