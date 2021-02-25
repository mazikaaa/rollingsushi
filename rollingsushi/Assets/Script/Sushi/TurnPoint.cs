using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPoint : MonoBehaviour
{
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "sushi.type")
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<sushi>().MoveUpdate(turnpoint);         
        }
    }
}
