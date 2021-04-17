using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodLooking : Skill
{
    //高級な寿司（180円以上）を食べる確率が上がる。ただし、リキャスト時間が長くなる

    public override bool BeforeEat(int price,UnitManager unitmanager) {

        if (price >= 180)
        {
            unitmanager.Eat_Rate -= 10.0f;
            unitmanager.SkillAnim();
        }

        return true;
    }

    public override void AfterEat(int price, GameManager gamemanager, UnitManager unitmanager, bool like) {
        if (price >= 180)
        {
            unitmanager.waittime_base += 3.0f;
        }
    }
}
