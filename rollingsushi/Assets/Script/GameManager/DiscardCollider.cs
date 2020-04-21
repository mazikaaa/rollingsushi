using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCollider:MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "sushi.type")
        {
            this.gameObject.transform.parent.gameObject.GetComponent<GameManager>().Discard();
            Destroy(collision.transform.parent.gameObject);
        }
    }
}

