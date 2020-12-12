using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class UnitDataBase : MonoBehaviour
{

    int i, j;

    public GameObject[] unitobject=new GameObject[10];
    public string[] unitname = new string[10];

    void Awake()
    {
        UnitSort();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UnitSort()
    {
        string name = null;
        GameObject[] guestobject_copy = new GameObject[unitobject.Length];
        Array.Copy(unitobject, guestobject_copy, unitname.Length);
        Array.Sort(unitname);


        for (i = 0; i < unitname.Length; i++)
        {
            name =guestobject_copy[i].name;
            for (j = 0; j <unitname.Length; j++)
            {
                if (name == unitname[j])
                {
                    unitobject[j] = guestobject_copy[i];
                    break;
                }
            }
        }

    }
}
