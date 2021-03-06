﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPoint : MonoBehaviour
{
    //寿司を進行させる方向を決める
    public enum Direction{ 
        UP,
        RIGHT,
        DOWN,
        LEFT,
    };

    public Direction direction;
    TurnPoint turnpoint;

    private void Start()
    {
        turnpoint = GetComponent<TurnPoint>();
    }

    //曲がり角に到達した際に進行方向を変更する
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "sushi.type")
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<sushi>().MoveUpdate(turnpoint);         
        }
    }
}
