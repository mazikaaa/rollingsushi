using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEat : Skill
{
    protected bool flag = true;
    protected float rate_like=0.0f;
    protected float rate_normal = 0.0f;

    //一定確率で、寿司を食べた後のリキャスト時間を無くす
    public override void AfterEat(int price, GameManager gamemanager,UnitManager unitmanager,bool like) {

        if (flag)
        {
            int random = Random.Range(0, 3);

            if (random == 0)
            {
                
                unitmanager.waittime_base = 0;
                rate_like = unitmanager.Rate_Like;
                rate_normal = unitmanager.Rate_Normal;
                unitmanager.Rate_Like = 15.0f;
                unitmanager.Rate_Normal = 15.0f;
                flag = false;
                unitmanager.SkillAnim();
            }

        }
        else
        {
            unitmanager.Rate_Like = rate_like;
            unitmanager.Rate_Normal = rate_normal;
            flag = true;
        }

    }
}
