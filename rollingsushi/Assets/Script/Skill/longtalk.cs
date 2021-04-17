using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class longtalk : Skill
{
    //リキャスト時間の時間が長くなる
    public override void AfterEat(int price, GameManager gamemanager, UnitManager unitmanager, bool like) {

        unitmanager.waittime_base += 2.0f;
        unitmanager.SkillAnim();
    }
}
