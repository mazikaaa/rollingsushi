using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag_Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Transform text;
    GameObject textobj,unitdatabase;
   
    int i;
   
    private GameObject[] unitobject = new GameObject[10];
    private string[] unitname = new string[10];
    private Sprite[] unitimage = new Sprite[10];

    public Image iconImage;
    public Sprite nowSprite;
    public int DropNo;
    public string dropname;
    protected string[] default_unit = { "DK", "JK", "OL", "salaryman", "wife", "oldman", "DKpair", "salarypair" };


    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("Text");
        textobj = text.gameObject;

        nowSprite = null;
        iconImage.sprite = nowSprite;

        unitdatabase = GameObject.Find("UnitDataBase");
        this.unitobject = unitdatabase.GetComponent<UnitDataBase>().unitobject;
        this.unitname = unitdatabase.GetComponent<UnitDataBase>().unitname;

        for(i= 0;i < unitobject.Length; i++)
        {
            unitimage[i] = unitobject[i].GetComponent<SpriteRenderer>().sprite;
        }

        UnitSort();
        Init_SetUnit();

        TextSet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textobj.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textobj.SetActive(false);
    }

    private void UnitSort()
    {
        int i, j;

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
    public void Init_SetUnit()
    {
        int i;
        int length = unitname.Length;
        dropname = PlayerPrefs.GetString("Unit" + DropNo, default_unit[DropNo - 1]);

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

    private void TextSet()
    {
        Text detail = textobj.GetComponent<Text>();
        textdata data = unitobject[DropNo - 1].GetComponent<textdata>();
        int num = 0;

        string name = data.name;
        string like = data.like;
        string leavetime = data.leavetime;
        string amount = data.amount;
        string rate = data.rate;
        string recast = data.recast;
        string eattime = data.eattime;
        string skill = data.skill;

        num = like.Length;
        while (num <= 5)
        {
            if (num <= 5)
            {
                like += " ";
                num++;
            }
        }

        num = leavetime.Length;
        while (num <= 17)
        {
            if (num <= 17)
            {
                leavetime += " ";
                num++;
            }
        }

        num = amount.Length;
        while (num <= 5)
        {
            if (num <= 5)
            {
                amount += " ";
                num++;
            }
        }

        num = rate.Length;
        while (num <= 8)
        {
            if (num <= 8)
            {
                rate += " ";
                num++;
            }
        }

        num = recast.Length;
        while (num <= 7)
        {
            if (num <= 7)
            {
                recast += " ";
                num++;
            }

        }

        num = eattime.Length;
        while (num<= 5)
        {
            num = leavetime.Length;
            if (num <= 5)
            {
                eattime += " ";
                num++;
            }
        }

        detail.text = "名前:" + name + "\n"+
                      "好きな寿司:"+like+"待機時間:"+leavetime+"食べる量:"+amount+"\n"+
                      "食べる確率:"+rate+"リキャスト時間:"+recast+"着席時間:"+eattime+"\n"+
                      "特殊能力:"+skill;

    }

}
