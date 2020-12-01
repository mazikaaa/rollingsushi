using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollider :MonoBehaviour
{
    private UnitManagr unitmanager;
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //GameObject unit = transform.parent.gameObject;
        if (collision.gameObject.tag == "sushi.type")
        {
            unitmanager.Eat(collision.gameObject.transform.parent.gameObject);
        }
    }

    public void setUnitManager(UnitManagr unitmanager)
    {
        this.unitmanager = unitmanager;
    }
}
