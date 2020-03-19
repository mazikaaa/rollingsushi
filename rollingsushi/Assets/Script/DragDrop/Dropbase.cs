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

    protected string[] guestname = { "maguro","guest" };
    public GameObject[] guestobject = new GameObject[10];

    protected void Start()
    {
        GuestSort();
    }

    public void GuestSort()
    {
        string[] guestname_copy = new string[guestname.Length];
        GameObject[] guestobject_copy = new GameObject[guestobject.Length];
        Array.Copy(guestname, guestname_copy, guestname.Length);
        Array.Copy(guestobject, guestobject_copy, guestname.Length);
        Array.Sort(guestname);

        for (i = 0; i < guestname.Length; i++)
        {
            for (j=0; j < guestname_copy.Length; j++)
            {
                if (guestname[i] == guestname_copy[j])
                    guestobject[i] = guestobject_copy[j];

            }
        }
        
    }
    public int GuestSearch(string name)
    {
        for (i = 0; i <guestname.Length; i++)
        {
            if (name == guestname[i])
            {
                return i;
            }
        }

        return 0;
    }
}
