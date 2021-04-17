using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dragbase : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerEnterHandler,IPointerExitHandler
{

   //ドラッグをし始めた時の処理
    public virtual void OnBeginDrag(PointerEventData pointerEventData)
    {

    }

    //ドラッグが入っている最中の処理
    public virtual void OnDrag(PointerEventData pointerEventData)
    {
    
    }

    //ドラッグを止めた時の処理
    public virtual void OnEndDrag(PointerEventData pointerEventData)
    {
      
    }

    // ドラッグ中に、動くお客さんの画像を生成
    public virtual void CreateDragObject()
    {
       
    }

    //アニメーションを発動（チュートリアル用）
    public virtual bool SetAnimation()
    {
        return true;
    }

    //カーソルが入ってきた時の処理
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
    }

    //カーソルが出て行った時の処理
    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }

    //rectTransform型をVector2型に変換する
    public virtual Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        return Vector2.one;
    }
}



