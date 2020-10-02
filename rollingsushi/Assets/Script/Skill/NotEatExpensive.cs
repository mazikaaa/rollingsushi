using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEatExpensive : Skill
{
    GameObject gamemanager;
    public void InitSkill()
    {

    }

    //寿司を食べる前に行う
    public bool BeforeEat(int price)
    {
        if (price >= 200)
            return false;
        else
            return true;

    }

    //寿司を食べた後に行う処理
    public void AfterEat()
    {

    }
}
