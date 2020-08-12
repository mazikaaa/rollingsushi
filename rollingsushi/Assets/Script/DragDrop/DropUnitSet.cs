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
            dropname = pointerEventData.pointerDrag.transform.GetChild(1).name;
            
            Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
            iconImage.sprite = droppedImage.sprite;
            nowSprite = droppedImage.sprite;
            iconImage.color = Vector4.one;
     }

    public override void UnitSort()
    {
        string[] guestname_copy = new string[unitname.Length];
        Array.Copy(unitname, guestname_copy, unitname.Length);
        Array.Sort(unitname);
    }

    public void Init_SetUnit()
    {
        int i;
        dropname = PlayerPrefs.GetString("Unit" + DropNo);

        for (i = 0; i < 4; i++)
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
