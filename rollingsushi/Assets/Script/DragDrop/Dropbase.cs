using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class Dropbase : MonoBehaviour
{
    int i,j;
   protected Guest[] guest = new Guest[10];

    public Image iconImage;

    public string[] unitname = new string[10];
    public GameObject[] unitobject = new GameObject[10];

    protected void Start()
    {
        GuestSort();
    }

    //ユニットを名前の順にソート
    public void GuestSort()
    {
        string[] guestname_copy = new string[unitname.Length];
        GameObject[] guestobject_copy = new GameObject[unitobject.Length];
        Array.Copy(unitname, guestname_copy, unitname.Length);
        Array.Copy(unitobject, guestobject_copy, unitname.Length);
        Array.Sort(unitname);

        for (i = 0; i < unitname.Length; i++)
        {
            for (j=0; j < guestname_copy.Length; j++)
            {
                if (unitname[i] == guestname_copy[j])
                    unitobject[i] = guestobject_copy[j];

            }
        }
        
    }

    //ソート済みのユニットから名前による検索
    public int GuestSearch(string name)
    {
        Debug.Log(name);
        for (i = 0; i <unitname.Length; i++)
        {
            if (name == unitname[i])
            {
                return i;
            }
        }
        return 0;
    }
}
