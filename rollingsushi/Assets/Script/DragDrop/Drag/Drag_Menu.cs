using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//メニュー画面のドラック機構
public class Drag_Menu : Dragbase
{
    //テキストデータ関連
    Transform[] text=new Transform[3];
    GameObject[] textobj = new GameObject[3];


    UnitDataBase unitdatabase;
    int i;
   
    //お客さん関連
    private GameObject[] unitobject = new GameObject[8];
    public int DropNo;
    public string dropname;
    protected string[] default_unit = { "DK", "JK", "OL", "salaryman", "wife", "oldman", "DKpair", "salarypair" };

    //画像関連
    public Image iconImage;
    public Sprite nowSprite;

    // Start is called before the first frame update
    void Start()
    {
        //テキスト表示のオブジェクトを所得
        for (i = 0; i < 3; i++)
        {
            text[i] = transform.Find("Text" + i);
            textobj[i] = text[i].gameObject;
        }

        //画像の初期化
        nowSprite = null;
        iconImage.sprite = nowSprite;

        //各ドロップの機構にお客さんをセットする
        unitdatabase = GameObject.Find("UnitDataBase").GetComponent<UnitDataBase>();
        Init_SetUnit();

        //テキストデータを付与する
        TextSet();
    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        for (i = 0; i < 3; i++)
        {
            textobj[i].SetActive(true);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        for (i = 0; i < 3; i++)
        {
            textobj[i].SetActive(false);
        }
    }

    //お客さんの初期化（各機構にお客さんのデータを付与する）
    public void Init_SetUnit()
    {
        int i;
        dropname = PlayerPrefs.GetString("Unit" + DropNo, default_unit[DropNo - 1]);

        GameObject[] unitobject_copy= unitdatabase.unitobject;
        string[] unitname_copy = unitdatabase.unitname;
        Sprite[] unitimages = new Sprite[unitobject_copy.Length];

        for (i = 0; i < unitobject_copy.Length; i++)
        {
            unitimages[i] = unitobject_copy[i].GetComponent<SpriteRenderer>().sprite;
        }


        for (i = 0; i < unitname_copy.Length; i++)
        {
            if (dropname == unitname_copy[i])
            {

                iconImage.sprite = unitimages[i];
                nowSprite = unitimages[i];
                iconImage.color = Vector4.one;
                unitobject[DropNo - 1] = unitobject_copy[i];
            }
        }
    }

　　//テキストデータの付与
    private void TextSet()
    {
        Text detail = textobj[0].GetComponent<Text>();
        Text detail2 = textobj[1].GetComponent<Text>();
        Text detail3 = textobj[2].GetComponent<Text>();

        textdata data = unitobject[DropNo - 1].GetComponent<textdata>();

        string name = data.name;
        string like = data.like;
        string leavetime = data.leavetime;
        string amount = data.amount;
        string rate = data.rate;
        string recast = data.recast;
        string eattime = data.eattime;
        string skill = data.skill;

        detail.text = "名前:" + name + "\n" +
                      "好きな寿司:" + like + "\n" +
                      "食べる確率:" + rate + "\n" +
                      "食べる量:     " + amount;

        detail2.text= "\n待機時間:" + leavetime +"\n"+ 
                      "リキャスト:" + recast +"\n"+
                       "着席時間:" + eattime;
        detail3.text = "\n特殊能力" + "\n" +
                        skill + "\n";
    }

}
