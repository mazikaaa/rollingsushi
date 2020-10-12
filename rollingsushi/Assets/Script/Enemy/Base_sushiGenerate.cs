using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base_sushiGenerate : MonoBehaviour
{
    /*
    //各値段帯の寿司が出る比率
    public float cheaprate;
    public float normalrate;
    public float expensiverate;

    protected float[] rates = new float[3];

    //出てくる寿司の種類を決める辞書
    Dictionary<int, float> generatesushirate;
    */
    int i,j;

    /*
    public GameObject[] cheapsushi = new GameObject[4];
    public GameObject[] normalsushi = new GameObject[4];
    public GameObject[] expensivesushi = new GameObject[4];
    */

    //出てくる寿司を決める辞書
    Dictionary<int, float> choosesushirate;

    public GameObject[] sushis = new GameObject[8];
    public float[] sushirate = new float[8];

    public float speed_x,speed_y;

    /*
    protected void GenerateCheapSushi()
    {
        sushirate = new Dictionary<int, float>();
        Debug.Log("安い寿司の個数" + cheapsushi.Length);

        for (i = 0; i < cheapsushi.Length; i++)
        {
            sushirate.Add(i, cheapsushi[i].GetComponentInChildren<sushidata>().rate);
        }

        int sushikey=Choose(sushirate);

        GameObject sushi =Instantiate(cheapsushi[sushikey], new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
        sushi.GetComponent<sushi>().init_other_direction = 3;
        sushi.GetComponent<sushi>().init_x = 2.0f;
        sushi.GetComponent<sushi>().init_y = 0.0f;
    }

    protected void GenerateNormalSushi()
    {
        sushirate = new Dictionary<int, float>();
        Debug.Log("普通の寿司の個数" + normalsushi.Length);

        for (i = 0; i < normalsushi.Length; i++)
        {
            sushirate.Add(i, normalsushi[i].GetComponentInChildren<sushidata>().rate);
        }

        int sushikey = Choose(sushirate);

        GameObject sushi = Instantiate(normalsushi[sushikey], new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
        sushi.GetComponent<sushi>().init_other_direction = 3;
        sushi.GetComponent<sushi>().init_x = 2.0f;
        sushi.GetComponent<sushi>().init_y = 0.0f;
    }

    //出てくる値段帯の寿司を決める関数
    protected int ChooseGenerateSushi()
    {
     
        generatesushirate = new Dictionary<int, float>();
        generatesushirate.Add(0, cheaprate);
        generatesushirate.Add(1, normalrate);
        generatesushirate.Add(2, expensiverate);

        return Choose(generatesushirate);
    }

    private int Choose(Dictionary<int,float> dic)
    {
        float total = 0;

        foreach (KeyValuePair<int, float> elem in dic)
        {
            total += elem.Value;
            //Debug.Log(elem.Value);
        }

        float randomPoint = UnityEngine.Random.value * total;

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
    */

    public void ChooseSushi() { 

        choosesushirate = new Dictionary<int, float>();

        for (i = 0; i < sushirate.Length; i++)
        {
            choosesushirate.Add(i,sushirate[i]);
        }

        int sushikey = Choose(choosesushirate);

        GameObject sushi = Instantiate(sushis[sushikey], new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
        sushi.GetComponent<sushi>().other_direction = 3;
        sushi.GetComponent<sushi>().direction = 1;
        sushi.GetComponent<sushi>().init_x = speed_x;
        sushi.GetComponent<sushi>().init_y = 0.0f;
        sushi.GetComponent<sushi>().speed_x = speed_x;
        sushi.GetComponent<sushi>().speed_y = speed_y;
    }

    private int Choose(Dictionary<int, float> dic)
    {
        float total = 0;

        foreach (KeyValuePair<int, float> elem in dic)
        {
            total += elem.Value;
            //Debug.Log(elem.Value);
        }

        float randomPoint = UnityEngine.Random.value * total;

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
