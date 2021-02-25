using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEatExpensive : Skill
{
    //寿司を食べる前に行う
    public override bool BeforeEat(int price,UnitManager unitmanager)
    {
        if (price > 150)
            return false;
        else
            return true;

    }

}
