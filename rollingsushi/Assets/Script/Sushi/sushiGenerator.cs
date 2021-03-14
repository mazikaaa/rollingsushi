using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sushiGenerator : MonoBehaviour
{

    int i, j;

    public float sushigeneratetime;//寿司を出す周期
    private float sushitime=0.0f;
    private int sushikey;//出てくる寿司の種類を決めるキー

    //出てくる寿司を決める辞書
    Dictionary<int, float> choosesushirate;

    //寿司を格納する配列
    public GameObject[] sushis = new GameObject[8];
    public float[] sushirate = new float[8];

    public float speed_x, speed_y;


    // Update is called once per frame
    void Update()
    {
        sushitime += Time.deltaTime;

        //一定周期毎に寿司を生成する
        if(sushitime>sushigeneratetime)
        {
            GenerateSushi();
            sushitime = 0.0f;
        }
    }

    //寿司を生成する
    public void GenerateSushi()
    {
        choosesushirate = new Dictionary<int, float>();

        for (i = 0; i < sushirate.Length; i++)
        {
            choosesushirate.Add(i, sushirate[i]);
        }

        int sushikey = Choose(choosesushirate);//生成する寿司を決める

        GameObject sushi = Instantiate(sushis[sushikey], new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
        sushi sushi_data = sushi.GetComponent<sushi>();
        sushi_data.direction = 1;
        sushi_data.init_x = speed_x;
        sushi_data.init_y = 0.0f;
        sushi_data.speed_x = speed_x;
        sushi_data.speed_y = speed_y;
    }

    //生成する寿司を確率で選択する関数
    private int Choose(Dictionary<int, float> dic)
    {
        float total = 0;

        foreach (KeyValuePair<int, float> elem in dic)
        {
            total += elem.Value;
            //Debug.Log(elem.Value);
        }

        float randomPoint = Random.value * total;

        foreach (KeyValuePair<int, float> elem in dic)
        {
            if (randomPoint < elem.Value)
            {
                return elem.Key;
            }
            else
            {
                randomPoint -= elem.Value;
            }
        }
        return 0;
    }

}
