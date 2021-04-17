using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : Skill
{

    //寿司を食べた後、一定確率で無条件に離席する
    public override void AfterEat(int price, GameManager gamemanager, UnitManager unitmanager, bool like) {
        int random = Random.Range(0, 6);

        if (random == 0)
        {
            unitmanager.SkillAnim();
            unitmanager.Leave();
        }
    }
}
