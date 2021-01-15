using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag_Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Transform[] text=new Transform[3];
    GameObject[] textobj = new GameObject[3];
    GameObject unitdatabase;
   
    int i;
   
    private GameObject[] unitobject = new GameObject[8];
    private string[] unitname = new string[8];
    private Sprite[] unitimage = new Sprite[8];

    public Image iconImage;
    public Sprite nowSprite;
    public int DropNo;
    public string dropname;
    protected string[] default_unit = { "DK", "JK", "OL", "salaryman", "wife", "oldman", "DKpair", "salarypair" };


    // Start is called before the first frame update
    void Start()
    {

        for (i = 0; i < 3; i++)
        {
            text[i] = transform.Find("Text" + i);
            textobj[i] = text[i].gameObject;
        }

        nowSprite = null;
        iconImage.sprite = nowSprite;

        //UnitSort();
        Init_SetUnit();

        TextSet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        for (i = 0; i < 3; i++)
        {
            textobj[i].SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        for (i = 0; i < 3; i++)
        {
            textobj[i].SetActive(false);
        }
    }

    /*
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
    */
    public void Init_SetUnit()
    {
        int i;
        dropname = PlayerPrefs.GetString("Unit" + DropNo, default_unit[DropNo - 1]);

        unitdatabase = GameObject.Find("UnitDataBase");
        GameObject[] unitobject_copy= unitdatabase.GetComponent<UnitDataBase>().unitobject;
        string[] unitname_copy = unitdatabase.GetComponent<UnitDataBase>().unitname;
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

    private void TextSet()
    {
        Text detail = textobj[0].GetComponent<Text>();
        Text detail2 = textobj[1].GetComponent<Text>();
        Text detail3 = textobj[2].GetComponent<Text>();

        textdata data = unitobject[DropNo - 1].GetComponent<textdata>();
        /*
        int[] num = { 0,0,0,0,0,0,0};
        int indent = 0; ;
        */

        string name = data.name;
        string like = data.like;
        string leavetime = data.leavetime;
        string amount = data.amount;
        string rate = data.rate;
        string recast = data.recast;
        string eattime = data.eattime;
        string skill = data.skill;

        /*
        num[0] = like.Length;
        Debug.Log(num);
        while (num[0] <= 5)
        {
            if (num[0] <= 5)
            {
                like += "　";
                num[0]++;
            }
        }

        num[1] = leavetime.Length;
        while (num[1] <= 7)
        {
            if (num[1] <= 7)
            {
                leavetime += " ";
                num[1]++;
            }
        }

        num[2] = amount.Length;
        indent = num[2];
        while (num[2] <= 16-indent)
        {
            //字数合わせ
            if (num[2] <= 16-indent)
            {
                amount += " ";
                num[2]++;
            }
        }

        num[3] = rate.Length;
        while (num[3] <= 16)
        {
            if (num[3] <= 16)
            {
                rate += " ";
                num[3]++;
            }
        }

        num[4] = recast.Length;
        indent = num[4];
        while (num[4] <= 7-indent)
        {
            if (num[4] <= 7-indent)
            {
                recast += " ";
                num[4]++;
            }

        }

        num[5] = eattime.Length;
        while (num[5]<= 5)
        {
            num[5] = leavetime.Length;
            if (num[5] <= 5)
            {
                eattime += " ";
                num[5]++;
            }
        }
        */

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
