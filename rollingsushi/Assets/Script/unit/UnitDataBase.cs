using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

//全お客さんの情報を格納しておくスクリプト
public class UnitDataBase : MonoBehaviour
{

    int i, j;


    public GameObject[] unitobject=new GameObject[10];
    public string[] unitname = new string[10];

    void Awake()
    {
        UnitSort();
    }

    //お客さんを名前の順に整列させる
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
