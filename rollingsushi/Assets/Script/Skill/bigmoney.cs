using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigmoney : Skill
{


    public override void AfterEat(int price,GameManager gamemanager,UnitManager unitmanager,bool like) {

        int random = Random.Range(0, 3);

        if (random == 0)
        {
            gamemanager.GainProfit(price);
            unitmanager.SkillAnim();
        }
    }

}
