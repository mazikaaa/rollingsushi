﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEatExpensive : Skill
{
    //150円以上の寿司は、食べない
    public override bool BeforeEat(int price,UnitManager unitmanager)
    {
        if (price > 150)
            return false;
        else
            return true;

    }

}
