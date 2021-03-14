using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//お客さんを編成する画面でのドロップの機構
public class DropUnitSet : DropBase
{
    //画像関連
    public Image iconImage;
    public Sprite nowSprite;

    //お客さん関連
    public GameObject[] unitobject = new GameObject[10];
    public string[] unitname = new string[10];
    public Sprite[] unitimage = new Sprite[10];

    private int i;
    private UnitDataBase unitdatabase;

    //ドロップの機構が持っている情報
    public int DropNo;
    public string dropname;
    protected string[] default_unit = { "DK", "JK", "OL", "salaryman", "wife", "oldman", "DKpair", "salarypair" };


    // Start is called before the first frame update
    void Start()
    {
        //画像の初期化
        nowSprite = null;
        iconImage.sprite = nowSprite;

        //お客さんの候補をデータベースより取得
        unitdatabase = GameObject.Find("UnitDataBase").GetComponent<UnitDataBase>();
        this.unitobject = unitdatabase.unitobject;
        this.unitname = unitdatabase.unitname;

        this.unitimage = new Sprite[unitobject.Length];
        for (i = 0; i < unitobject.Length; i++)
        {
            unitimage[i] = unitobject[i].GetComponent<SpriteRenderer>().sprite;
        }

        //お客さんをドロップの機構にセットしておく
        UnitSort();
        Init_SetUnit();
    }

    public override void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;

        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = droppedImage.sprite;
        iconImage.color = Vector4.one * 0.6f;
     }
    public override void OnPointerExit(PointerEventData pointerEventData)
    {

        if (pointerEventData.pointerDrag == null)
            return;

        iconImage.sprite = nowSprite;
        if (nowSprite == null)
            iconImage.color = Vector4.zero;
        else
            iconImage.color = Vector4.one;
    }

    public override void OnDrop(PointerEventData pointerEventData)
    {
            dropname = pointerEventData.pointerDrag.name;
            
            Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
            iconImage.sprite = droppedImage.sprite;
            nowSprite = droppedImage.sprite;
            iconImage.color = Vector4.one;
     }

    //お客さんを名前の順にソートする
    private void UnitSort()
    {
        int i,j;

        string[] guestname_copy = new string[unitname.Length];
        Sprite[] guestsprite_copy = new Sprite[unitimage.Length];
        Array.Copy(unitname, guestname_copy, unitname.Length);
        Array.Copy(unitimage, guestsprite_copy, unitname.Length);
        Array.Sort(unitname);

        for (i = 0; i < unitname.Length; i++)
        {
            for (j = 0; j < guestname_copy.Length; j++)
            {
                if (unitname[i] == guestname_copy[j])
                    unitimage[i] = guestsprite_copy[j];
            }
        }
　　}
    //現状のユニット編成を表示する（初期化）
    public void Init_SetUnit()
    {
        int i;
        int length = unitname.Length;
        dropname = PlayerPrefs.GetString("Unit" + DropNo, default_unit[DropNo-1]);

        for (i = 0; i < length; i++)
        {
            if (dropname == unitname[i])
            {
                iconImage.sprite = unitimage[i];
                nowSprite = unitimage[i];
                iconImage.color = Vector4.one;
            }
        }
    }

}
