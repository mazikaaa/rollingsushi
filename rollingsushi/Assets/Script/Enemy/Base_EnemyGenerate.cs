using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_EnemyGenerate : MonoBehaviour
{
    int i;
    public GameObject[] cheapsushi = new GameObject[4];
    public GameObject[] normalsushi = new GameObject[4];
    public GameObject[] expensivesushi = new GameObject[4];

    protected void GenerateCheapSushi()
    {
        i = Random.Range(0, 2);
        GameObject sushi=Instantiate(cheapsushi[i], new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
        sushi.GetComponent<Enemy>().init_other_direction = 3;
        sushi.GetComponent<Enemy>().init_x = 2.0f;
        sushi.GetComponent<Enemy>().init_y = 0.0f;
    }
}
