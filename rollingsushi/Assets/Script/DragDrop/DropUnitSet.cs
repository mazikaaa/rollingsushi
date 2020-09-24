using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropUnitSet : UnitDataBase, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image iconImage;
    public Sprite nowSprite;
    public int DropNo;
    public string dropname;
    public Sprite[] unit_images = new Sprite[24];
    protected string[] default_unit = { "DK", "JK", "OL", "salaryman", "wife", "oldman", "DKpair", "salarypair" };


    // Start is called before the first frame update
    void Start()
    {
        nowSprite = null;
        iconImage.sprite = nowSprite;
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
            dropname = pointerEventData.pointerDrag.transform.GetChild(2).name;
            
            Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
            iconImage.sprite = droppedImage.sprite;
            nowSprite = droppedImage.sprite;
            iconImage.color = Vector4.one;
     }

    public override void UnitSort()
    {
        int i,j;

        string[] guestname_copy = new string[unitname.Length];
        Sprite[] guestsprite_copy = new Sprite[unit_images.Length];
        Array.Copy(unitname, guestname_copy, unitname.Length);
        Array.Copy(unit_images, guestsprite_copy, unitname.Length);
        Array.Sort(unitname);

        for (i = 0; i < unitname.Length; i++)
        {
            for (j = 0; j < guestname_copy.Length; j++)
            {
                if (unitname[i] == guestname_copy[j])
                    unit_images[i] = guestsprite_copy[j];

            }
        }

}

    public void Init_SetUnit()
    {
        int i;
        int length = unitname.Length;
        dropname = PlayerPrefs.GetString("Unit" + DropNo, default_unit[DropNo-1]);

        for (i = 0; i < length; i++)
        {
            if (dropname == unitname[i])
            {
                
                iconImage.sprite = unit_images[i];
                nowSprite = unit_images[i];
                iconImage.color = Vector4.one;
            }
        }
    }


}
