using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigmoney : Skill
{

    //一定確率で、寿司を食べた時に得られる利益が増える
    public override void AfterEat(int price,GameManager gamemanager,UnitManager unitmanager,bool like) {

        int random = Random.Range(0, 3);

        if (random == 0)
        {
            gamemanager.GainProfit(price);
            unitmanager.SkillAnim();
        }
    }

}
