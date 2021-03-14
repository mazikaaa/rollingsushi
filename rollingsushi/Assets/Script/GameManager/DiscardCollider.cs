using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCollider:MonoBehaviour
{
    //廃棄エリアに寿司が到達した時に廃棄する
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "sushi.type")
        {
            this.gameObject.transform.parent.gameObject.GetComponent<GameManager>().Discard();//廃棄する関数
            Destroy(collision.transform.parent.gameObject);
        }
    }
}

