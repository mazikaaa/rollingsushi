using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneSkill : Skill
{
    //ユニットがセットされたときに行う処理
    public void InitSkill()
    {

    }

    //寿司を食べる前に行う
    public bool BeforeEat(int price=0)
    {
        return true;
    }

    //寿司を食べた後に行う処理
    public void AfterEat()
    {

    }
}
