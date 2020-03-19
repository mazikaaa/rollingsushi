using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorBase : MonoBehaviour
{
    // 最大体力.
    [SerializeField] int maxHp = 100;

    // 現在の体力.
    protected int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            // 最大体力を越える値は設定できないようにする.
            _hp = Mathf.Min(maxHp, value);

            if (hp <= 0)
            {
                Death();
            }
        }
    }
    protected int _hp;

   public void Start()
    {
        hp = maxHp-100;
    }

    // ダメージを与える.
    public void AddDamage(int damage)
    {
        hp -= Mathf.Max(0, damage);
        Debug.Log(damage + " ポイントのダメージ受けました");
        Debug.Log(hp);
    }

    public void Death()
    {
        Debug.Log("すでに死んでおります");
    }
}
