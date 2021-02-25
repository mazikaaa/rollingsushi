using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollider :MonoBehaviour
{
    private UnitManager unitmanager;
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //GameObject unit = transform.parent.gameObject;
        if (collision.gameObject.tag == "sushi.type")
        {

            if (unitmanager.Eat(collision.gameObject.GetComponent<sushidata>()))
            {
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
        }
    }

    public void setUnitManager(UnitManager unitmanager)
    {
        this.unitmanager = unitmanager;
    }
}
