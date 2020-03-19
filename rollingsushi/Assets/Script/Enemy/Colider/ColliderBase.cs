using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBase : MonoBehaviour
{
    /// <summary>
    /// 当たり判定関係
    /// </summary>

    public GameObject enemy;
    int i;
    bool[] dir_touch = new bool[4];
    //[SerializeField] int direction,other_direction;
    public bool touch;
    [SerializeField] GameObject[] dir = new GameObject[4];
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        touch = true;
        dir_touch[0] = dir[0].GetComponent<upCollider>().touch;
        dir_touch[1] = dir[1].GetComponent<rightCollider>().touch;
        dir_touch[2] = dir[2].GetComponent<downCollider>().touch;
        dir_touch[3] = dir[3].GetComponent<leftCollider>().touch;

        if (collider.gameObject.tag == "wall")
        {
            for (i = 0; i < 4; i++)
            {
                if (dir_touch[i] == false)
                {
                    if (i != enemy.GetComponent<Enemy>().other_direction)
                    {
                        Debug.Log(i);
                        switch (i)
                        {
                            case 0:
                                enemy.GetComponent<Enemy>().MoveUpdate(0.0f, 0.03f, 2);
                                break;
                            case 1:
                                enemy.GetComponent<Enemy>().MoveUpdate(0.03f, 0.0f, 3);
                                break;
                            case 2:
                                enemy.GetComponent<Enemy>().MoveUpdate(0.0f,-0.03f, 0);
                                break;
                            case 3:
                                enemy.GetComponent<Enemy>().MoveUpdate(-0.03f, 0.0f, 1);
                                break;
                        }
                        return;
                    }
                }
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("こんにちは"+direction);
        if (collision.gameObject.tag == "wall")
        {
            touch = false;
        }
    }


}
