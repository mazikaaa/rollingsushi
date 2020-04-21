using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollider :MonoBehaviour
{
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject unit = transform.parent.gameObject;
        if (collision.gameObject.tag == "sushi.type")
        {
            unit.GetComponent<UnitManagr>().Eat(collision.gameObject.transform.parent.gameObject);
        }
    }
}
