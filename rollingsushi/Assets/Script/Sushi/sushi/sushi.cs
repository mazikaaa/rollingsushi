using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sushi : MonoBehaviour
{
    /// <summary>
    /// 移動関係
    /// </summary>

    public float x, y;
    public float init_x, init_y;
    public float speed_x, speed_y;
    public int direction = 0, other_direction = 0;

    Rigidbody2D sushirigidbody;

    //otherdirection(進行方向との逆方向)は壁に衝突した際にその方向に進まないために必要
    void Start()
    {
        Invoke("Initmove", 0.1f);
        //sushirigidbody = GetComponent<Rigidbody2D>();

    }
    // Start is called before the first frame update
    void Update()
    {
        Vector3 pos = this.gameObject.transform.position;
        //sushirigidbody.MovePosition(new Vector3(pos.x + x, pos.y + y, pos.z));
        this.gameObject.transform.position = new Vector3(pos.x + x, pos.y + y, pos.z);
    }

    void Initmove()
    {
        x = init_x;
        y = init_y;
    }

    public void MoveUpdate(TurnPoint turnpoint)
    {
        int new_direction = (int)turnpoint.direction;

        switch (new_direction)
        {
            case 0:
                x = 0.0f;
                y = speed_y;
                break;
            case 1:
                x = speed_x;
                y = 0.0f;
                break;
            case 2:
                x = 0.0f;
                y = -speed_y;
                break;
            case 3:
                x = -speed_x;
                y = 0.0f;
                break;
        }
        direction = new_direction;
    }

    //スピードだけ変えたい時の関数
    public void SpeedUpdate(float vx, float vy)
    {
        speed_y += vy;
        speed_x += vx;

        switch (direction)
        {
            case 0:
                x = 0.0f;
                y = speed_y;
                break;
            case 1:
                x = speed_x;
                y = 0.0f;
                break;
            case 2:
                x = 0.0f;
                y = -speed_y;
                break;
            case 3:
                x = -speed_x;
                y = 0.0f;
                break;
        }
    }

}
