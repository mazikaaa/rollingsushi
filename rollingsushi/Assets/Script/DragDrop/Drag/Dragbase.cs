using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragbase : UnitDataBase
{
    public bool iconflag = false;
    public Sprite test;

    protected float deleteicon_time,generateicon_time=5.0f;//消去、生成までの時間
    protected string iconname;
    protected string guageflag = "generate";
    protected float deletetime, generatetime;
    protected int unittype;
    protected GameObject gamemanager,unitdatabase;

    protected void Start()
    {
        gamemanager = GameObject.Find("GameManager");
        unitdatabase = GameObject.Find("UnitDataBase");
        SetUnit();
    }

    protected void GenerateUnit()
    {
        int i;

        i = Random.Range(0, 8);//ユニットの追加時に変更忘れずに
        iconname = unitobject[i].name;
        deleteicon_time=unitobject[i].GetComponentInChildren<Generatedata>().deletespan;
        this.gameObject.GetComponent<Image>().sprite = unitobject[i].GetComponentInChildren<Generatedata>().Generateicon;
        gameObject.GetComponent<Image>().color = Vector4.one;
        unittype = unitobject[i].GetComponent<Unitdata>().unittype;
        guageflag = "delete";
        generatetime = 0.0f;
        iconflag = true;
    }

    public void DeleteShadowUnit()
    {
        GameObject[] drops = GameObject.FindGameObjectsWithTag("drop");
        foreach (GameObject drop in drops)
        {
            if (drop.GetComponent<Drop>().shadowUnit)
            {
                drop.GetComponent<Drop>().ExitShadowImage();
                drop.GetComponent<Drop>().shadowUnit = false;
            }
        }
    }
    public void DeleteUnit()
    {
        this.gameObject.GetComponent<Image>().sprite = null;
        this.gameObject.GetComponent<Image>().color = Vector4.zero;
        guageflag = "generate";
        generateicon_time = 10.0f - gamemanager.GetComponent<GameManager>().Rep * 0.5f;
        // Debug.Log(generateicon);
        unittype = 0;
        deletetime = 0.0f;
        iconflag = false;
    }


    //ユニット編成画面で編成したユニットを生成候補にセットする
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
            checkname = PlayerPrefs.GetString("Unit" +(i+1));
            for (j = 0; j < 24; j++)//データベースにあるユニットの数
            {
             //   Debug.Log(checkname);
                //名前が一致するオブジェクトを生成候補に追加
                if (checkname == unitnamedata[j])
                {
                    unitobject[i] = unitdatabase.GetComponent<UnitDataBase>().unitobject[j];
                    unitname[i]= unitdatabase.GetComponent<UnitDataBase>().unitname[j];
                }
            }
        }
    }

}
