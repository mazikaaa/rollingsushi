using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_EnemyGenerate : MonoBehaviour
{
    int i;
    public GameObject[] cheapsushi = new GameObject[4];
    public GameObject[] normalsushi = new GameObject[4];
    public GameObject[] expensivesushi = new GameObject[4];

    public float GenerateCheapSushi()
    {
        i = Random.Range(0, 2);
            Instantiate(cheapsushi[i], new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
  
        return 0.0f;
    }
}
