using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill:MonoBehaviour
{
    //寿司を食べる前に行う処理
   public virtual bool BeforeEat(int price,UnitManager unitmanager) { return true; }

    //寿司を食べた後に行う処理
    public virtual void AfterEat(int price,GameManager gamemanager,UnitManager unitmanager,bool like) { }

    //席を離れる時に行う処理
    public virtual void LeaveSkill(int amount,GameManager gamemanager,UnitManager unitManager) { }
}
