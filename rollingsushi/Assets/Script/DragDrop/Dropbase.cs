using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dropbase : UnitDataBase
{
    
    public bool setUnit = false;//ユニットがすでに設置されているかを判断
    public bool shadowUnit = false;//色が薄いユニットが表示される状態にあるか。
    public int unitcapable;//許されるユニットの形態

    protected string[] default_unit = { "DK", "JK", "OL", "salaryman", "wife", "oldman", "DKpair", "salarypair" };
    protected string setdropname;//席に配置されるユニットの名前を入れる
    protected int unittype;
    protected Sprite[] unitsprite = new Sprite[4];

    private GameObject unitdatabase;

    protected void Start()
    {
        unitdatabase = GameObject.Find("UnitDataBase");
        SetUnit();
       // UnitSort();
    }


    //ユニットのオブジェクトを名前の順にソート
    /*
    public void UnitSort()
    {
        string[] unitname_copy = new string[unitname.Length];
        GameObject[] unitobject_copy = new GameObject[unitobject.Length];
        Array.Copy(unitname, unitname_copy, unitname.Length);
        Array.Copy(unitobject, unitobject_copy, unitname.Length);
        Array.Sort(unitname);

        for (i = 0; i < unitname.Length; i++)
        {
            for (j=0; j < unitname_copy.Length; j++)
            {
                if (unitname[i] == unitname_copy[j])
                    unitobject[i] = unitobject_copy[j];

            }
        }
        
    }
    */
    
    //ユニットのタイプを確認
    public bool CheckUnitType(string name)
    {
        int i;

        for (i = 0; i < unitname.Length; i++)
        {
            if (name == unitname[i])
            {
                if (unitobject[i].GetComponent<Unitdata>().unittype <= unitcapable) {
                    setdropname = name;
                    unittype = unitobject[i].GetComponent<Unitdata>().unittype;
                    return true;
                }
                else
                    return false;
            }
        }
        return false;
    }

    public void FetchUnitImage()
    {
        int i,j;
        for (i= 0; i < unitname.Length; i++)
        {
            if (setdropname == unitname[i])
            {
                for (j = 0; j < unittype; j++)
                {
                    unitsprite[j] = unitobject[i].GetComponent<Unitdata>().Separate_image[j];
                }
            }
        }
    }

    //ソート済みのユニットから名前による検索
    public void UnitSearch(string name)
    {
        int i;
        UnitManagr unitmanager = GetComponent<UnitManagr>();

        for (i = 0; i < unitname.Length; i++)
        {
            if (name == unitname[i])
            {
                Unitdata unitdata = unitobject[i].GetComponent<Unitdata>();
                //ユニットの情報を書き込む
                    unitmanager.probability_like = unitdata.probability_like;
                    unitmanager.probability_normal =unitdata.probability_normal;
                    unitmanager.waittime_like = unitdata.waittime_like;
                    unitmanager.waittime_normal = unitdata.waittime_normal;
                    unitmanager.like = unitdata.like;
                    unitmanager.dislike = unitdata.dislike;
                    unitmanager.SetSkill(unitdata.skillNo);
                    unitmanager.eatamount = unitdata.eatamount;
                    unitmanager.setUnit = true;
                    unitmanager.SetTime(unitdata.leavetime);

                this.gameObject.GetComponentInChildren<UnitCollider>().unitmanager = unitmanager;
                    setUnit = true;
            }
        }
    }

    public void SetUnit()
    {
        int i, j;
        string checkname;
        string[] unitnamedata = new string[24];
        int length = unitdatabase.GetComponent<UnitDataBase>().unitname.Length;

        //先にデータベースからユニット名前を取ってくる
        for (j = 0; j < length; j++)
        {
            unitnamedata[j] = unitdatabase.GetComponent<UnitDataBase>().unitname[j];
        }

        for (i = 0; i < 8; i++)//選択したユニットの数
        {
            checkname = PlayerPrefs.GetString("Unit" + (i + 1),default_unit[i]);
            for (j = 0; j < 24; j++)//データベースにあるユニットの数
            {
                //名前が一致するオブジェクトを生成候補に追加
                if (checkname == unitnamedata[j])
                {
                    unitobject[i] = unitdatabase.GetComponent<UnitDataBase>().unitobject[j];
                    unitname[i] = unitdatabase.GetComponent<UnitDataBase>().unitname[j];
                }
            }
        }
    }


}
