using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropUnitSet : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image iconImage;
    public Sprite nowSprite;

    /*
    private GameObject[] unit_sub = new GameObject[10];
    private string[] name_sub = new string[10];
    private Sprite[] img_sub = new Sprite[10];
    */

    public GameObject[] unitobject = new GameObject[10];
    public string[] unitname = new string[10];
    public Sprite[] unitimage = new Sprite[10];

    private int i;
    private GameObject unitdatabase;

    public int DropNo;
    public string dropname;
    protected string[] default_unit = { "DK", "JK", "OL", "salaryman", "wife", "oldman", "DKpair", "salarypair" };


    // Start is called before the first frame update
    void Start()
    {
        nowSprite = null;
        iconImage.sprite = nowSprite;

        unitdatabase = GameObject.Find("UnitDataBase");
        this.unitobject = unitdatabase.GetComponent<UnitDataBase>().unitobject;
        this.unitname = unitdatabase.GetComponent<UnitDataBase>().unitname;

        this.unitimage = new Sprite[unitobject.Length];

        for (i = 0; i < unitobject.Length; i++)
        {
            unitimage[i] = unitobject[i].GetComponent<SpriteRenderer>().sprite;
        }

        UnitSort();
        Init_SetUnit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;

        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = droppedImage.sprite;
        iconImage.color = Vector4.one * 0.6f;
     }
    public void OnPointerExit(PointerEventData pointerEventData)
    {

        if (pointerEventData.pointerDrag == null)
            return;

        iconImage.sprite = nowSprite;
        if (nowSprite == null)
            iconImage.color = Vector4.zero;
        else
            iconImage.color = Vector4.one;
    }

    public void OnDrop(PointerEventData pointerEventData)
    {
            dropname = pointerEventData.pointerDrag.name;
            
            Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
            iconImage.sprite = droppedImage.sprite;
            nowSprite = droppedImage.sprite;
            iconImage.color = Vector4.one;
     }

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

    private void Init_SetUnit()
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
