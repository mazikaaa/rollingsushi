using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropBase : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    //カーソルが入った時の処理
    public virtual void OnPointerEnter(PointerEventData pointerEventData)
    {

    }

    //カーソルが出た時の処理
    public virtual void OnPointerExit(PointerEventData pointerEventData)
    {
  
    }

    //ドロップした時の処理
    public virtual void OnDrop(PointerEventData pointerEventData)
    {
    }
}
