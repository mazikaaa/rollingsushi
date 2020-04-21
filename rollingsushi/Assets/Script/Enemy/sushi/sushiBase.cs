using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sushiBase : MonoBehaviour
{
    /// <summary>
    /// 移動関係
    /// </summary>

    public float x, y;
    public float init_x, init_y;
    public int init_other_direction;
    public int direction, other_direction;
    //otherdirection(進行方向との逆方向)は壁に衝突した際にその方向に進まないために必要
    void Start()
    {
        Invoke("Initmove", 0.1f);
    }
    // Start is called before the first frame update
    void Update()
    {
        Vector3 pos = this.gameObject.transform.position;
        this.gameObject.transform.position = new Vector3(pos.x + x, pos.y +y, pos.z);
    }

    void Initmove()
    {
        x = init_x;
        y = init_y;
        other_direction = init_other_direction;
    }

   public void MoveUpdate(float new_x,float new_y,int new_direction)
    {
        x = new_x;
        y = new_y;
        other_direction = new_direction;
    }

 
}
