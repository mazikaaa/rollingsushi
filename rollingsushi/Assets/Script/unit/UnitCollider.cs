using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollider :MonoBehaviour
{
    private UnitManager unitmanager;
    void Start()
    {
        unitmanager= this.gameObject.GetComponent<UnitManager>();
    }

    //お客さんの前を寿司が通った時に、一定確率で寿司を食べる
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "sushi.type")
        {

            if (unitmanager.Eat(collision.gameObject.GetComponent<sushidata>()))//寿司を食べるかどうか判定する
            {
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
        }
    }

    //席に配置されているお客さんの情報をセットする
    public void setUnitManager(UnitManager unitmanager)
    {
        this.unitmanager = unitmanager;
    }
}
