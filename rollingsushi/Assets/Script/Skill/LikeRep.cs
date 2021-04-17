using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeRep : Skill
{

    //好きな寿司を食べた時に、低確率で「評判」を上げてくれる
    public override void AfterEat(int price, GameManager gamemanager, UnitManager unitmanager,bool like) {

        if (like)
        {
            int random = Random.Range(0, 6);

            if (random == 0)
            {
                gamemanager.RaiseRep();
                unitmanager.SkillAnim();
            }
        }
    }
}
