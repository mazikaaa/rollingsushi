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
   //protected Guest[] guest = new Guest[10];

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
                GetComponent<UnitManagr>().probability_like=unitobject[i].GetComponent<Unitdata>().probability_like;
                GetComponent<UnitManagr>().probability_normal = unitobject[i].GetComponent<Unitdata>().probability_normal;
                GetComponent<UnitManagr>().waittime_like = unitobject[i].GetComponent<Unitdata>().waittime_like;
                GetComponent<UnitManagr>().waittime_normal = unitobject[i].GetComponent<Unitdata>().waittime_normal;
                GetComponent<UnitManagr>().like = unitobject[i].GetComponent<Unitdata>().like;
                GetComponent<UnitManagr>().dislike = unitobject[i].GetComponent<Unitdata>().dislike;
                GetComponent<UnitManagr>().eatamount = unitobject[i].GetComponent<Unitdata>().eatamount;
                GetComponent<UnitManagr>().leavetime = unitobject[i].GetComponent<Unitdata>().leavetime;
                return i;
            }
        }
        return 0;
    }
}
