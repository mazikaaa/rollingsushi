using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragbase : UnitDataBase
{
    public bool iconflag = false;

    Dictionary<int, float> chooserate;

    protected int[] unitrate = { 10, 10, 10, 10, 10, 10, 10, 10 };

    protected float deleteicon_time,generateicon_time=5.0f;//消去、生成までの時間
    protected string iconname;
    protected string guageflag = "generate";
    protected float deletetime, generatetime;
    protected int unittype;
    protected GameObject gamemanager,unitdatabase;

    public  float eventplustime=0.0f;
    public Image guage;

    protected void Start()
    {
        gamemanager = GameObject.Find("GameManager");
        unitdatabase = GameObject.Find("UnitDataBase");
        SetUnit();
    }

    protected void GenerateUnit()
    {
        int i;
        chooserate = new Dictionary<int, float>();

        for (i = 0; i <unitrate.Length; i++)
        {
            chooserate.Add(i, unitrate[i]);
        }
        int unitkey = Choose(chooserate);

        iconname = unitobject[unitkey].name;
        deleteicon_time=unitobject[unitkey].GetComponentInChildren<Generatedata>().deletespan;
        guage.fillOrigin = 0;
        this.gameObject.GetComponent<Image>().sprite = unitobject[unitkey].GetComponentInChildren<Generatedata>().Generateicon;
        gameObject.GetComponent<Image>().color = Vector4.one;
        unittype = unitobject[unitkey].GetComponent<Unitdata>().unittype;
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
            }
        }
    }
    public void DeleteUnit(float time=0.0f)
    {
        this.gameObject.GetComponent<Image>().sprite = null;
        this.gameObject.GetComponent<Image>().color = Vector4.zero;
        guageflag = "generate";

        //評判の値,イベントによる効果によって次のお客が来るまでの時間を決める
        generateicon_time = 11.0f - gamemanager.GetComponent<GameManager>().Rep * 0.6f+eventplustime+time;
        guage.fillOrigin = 1;
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

    private int Choose(Dictionary<int, float> dic)
    {
        int i = 0;
        float total = 0;

        foreach (KeyValuePair<int, float> elem in dic)
        {
            total += elem.Value;
            //Debug.Log(elem.Value);
        }

        float randomPoint = UnityEngine.Random.value * total;

        foreach (KeyValuePair<int, float> elem in dic)
        {
            if (randomPoint < elem.Value)
            {
                for (i = 0; i < unitrate.Length; i++)
                {
                    //今回選ばれたユニットは選ばれにくくする
                    if (i == elem.Key)
                    {
                        unitrate[i] -= 7;
                        if (unitrate[i] < 0)
                        {
                            unitrate[i] = 0;
                        }
                    }
                    else
                    {
                        unitrate[i] += 1;
                    }

                }
                return elem.Key;
            }
            else
            {
                randomPoint -= elem.Value;
            }
        }
        return 0;
    }

}
