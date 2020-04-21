using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_sushiGenerate : MonoBehaviour
{
    int i;
    public GameObject[] cheapsushi = new GameObject[4];
    public GameObject[] normalsushi = new GameObject[4];
    public GameObject[] expensivesushi = new GameObject[4];

    protected void GenerateCheapSushi()
    {
        i = Random.Range(0, 3);//寿司の追加時に変更忘れずに
        GameObject sushi=Instantiate(cheapsushi[i], new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
        sushi.GetComponent<sushi>().init_other_direction = 3;
        sushi.GetComponent<sushi>().init_x = 2.0f;
        sushi.GetComponent<sushi>().init_y = 0.0f;
    }
}
