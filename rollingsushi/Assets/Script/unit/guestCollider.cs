using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guestCollider :MonoBehaviour
{
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject guest = transform.root.gameObject;
        if (collision.gameObject.tag == "sushi.type")
        {
         guest .GetComponent<Guest>().Eat(collision.GetComponent<sushidata>().sushi_name, collision.GetComponent<sushidata>().sushi_type, collision.gameObject.transform.parent.gameObject);
        }
    }
}
