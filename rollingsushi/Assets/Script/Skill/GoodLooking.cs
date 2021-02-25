﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodLooking : Skill
{
    public override bool BeforeEat(int price,UnitManager unitmanager) {

        if (price >= 180)
        {
            unitmanager.Eat_Flag -= 10.0f;
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
