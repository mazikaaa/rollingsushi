using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Skill
{
    //ユニットがセットされたときに行う処理
   void InitSkill();

    //寿司を食べる前に行う
    bool BeforeEat(int price);

    //寿司を食べた後に行う処理
    void AfterEat();
}
