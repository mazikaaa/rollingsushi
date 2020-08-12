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
    // Start is called before the first frame update
    void Start()
    {
        UnitSort();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void UnitSort()
    {
        string[] guestname_copy = new string[unitname.Length];
        GameObject[] guestobject_copy = new GameObject[unitobject.Length];
        Array.Copy(unitname, guestname_copy, unitname.Length);
        Array.Copy(unitobject, guestobject_copy, unitname.Length);
        Array.Sort(unitname);

        for (i = 0; i < unitname.Length; i++)
        {
            for (j = 0; j < guestname_copy.Length; j++)
            {
                if (unitname[i] == guestname_copy[j])
                    unitobject[i] = guestobject_copy[j];

            }
        }

    }
}
