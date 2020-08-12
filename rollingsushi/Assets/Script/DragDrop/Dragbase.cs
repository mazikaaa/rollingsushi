using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragbase : MonoBehaviour
{
    int i;

    [SerializeField] GameObject[] unit = new GameObject[8];
    public bool iconflag = false;
    public Sprite test;

    protected float deleteicon,generateicon=5.0f;//消去、生成までの時間
    protected string iconname;
    protected string guageflag = "generate";
    protected float deletetime, generatetime;
    protected GameObject gamemanager,unitdatabase;

    protected void Start()
    {
        gamemanager = GameObject.Find("GameManager");
        unitdatabase = GameObject.Find("UnitDataBase");
        SetUnit();
    }

    protected void GenerateUnit()
    {
        i = Random.Range(0, 4);//寿司の追加時に変更忘れずに
        iconname = unit[i].name;
        deleteicon=unit[i].GetComponentInChildren<Generatedata>().deletespan;
        this.gameObject.GetComponent<Image>().sprite = unit[i].GetComponentInChildren<Generatedata>().Generateicon;
        gameObject.GetComponent<Image>().color = Vector4.one;
        guageflag = "delete";
        generatetime = 0.0f;
        iconflag = true;
    }

    public void DeleteUnit()
    {
        this.gameObject.GetComponent<Image>().sprite = null;
        this.gameObject.GetComponent<Image>().color = Vector4.zero;
        guageflag = "generate";
        generateicon = 10.0f - gamemanager.GetComponent<GameManager>().Rep*1.0f;
       // Debug.Log(generateicon);
        deletetime = 0.0f;
        iconflag = false;
    }

    public void DeleteShadowUnit()
    {
        GameObject[] drops = GameObject.FindGameObjectsWithTag("drop");
        foreach (GameObject drop in drops)
        {
            if (drop.GetComponent<Drop>().shadowUnit)
            {
                drop.GetComponent<Drop>().iconImage.color = Vector4.zero;
                drop.GetComponent<Drop>().shadowUnit = false;
            }
        }
    }

    public void SetUnit()
    { 
        string checkname;
        string[] unitname = new string[24];
        int i,j;

        //先にデータベースからユニット名前を取ってくる
        for (j = 0; j < 4; j++)
        {
            unitname[j] = unitdatabase.GetComponent<UnitDataBase>().unitname[j];
        }

        for (i = 0; i < 8; i++)
        {
            checkname = PlayerPrefs.GetString("Unit" +(i+1));
            for (j = 0; j < 24; j++)
            {
             //   Debug.Log(checkname);
                //名前が一致するオブジェクトを生成候補に追加
                if (checkname == unitname[j])
                {
                    unit[i] = unitdatabase.GetComponent<UnitDataBase>().unitobject[j];
                }
            }
        }

        
    }
   

}
