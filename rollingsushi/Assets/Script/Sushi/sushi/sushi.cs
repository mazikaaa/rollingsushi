using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sushi : MonoBehaviour
{
    public float x, y;

    //移動関連
    public float init_x, init_y;
    public float speed_x, speed_y;
    public int direction = 0;

    void Start()
    {
        Invoke("Initmove", 0.1f);
    }
    void Update()
    {
        Vector3 pos = this.gameObject.transform.position;
        this.gameObject.transform.position = new Vector3(pos.x + x, pos.y + y, pos.z);//寿司の移動
    }

    //移動のスピードの初期設定
    void Initmove()
    {
        x = init_x;
        y = init_y;
    }

    //移動するスピードと方法を変更する
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
