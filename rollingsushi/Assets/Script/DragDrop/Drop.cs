using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : Dropbase, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int objectindex;
    private Sprite nowSprite;
    private GameObject dropobject;
    private string dropname;

    void Start()
    {
        base.Start();
        nowSprite = null;
        iconImage.sprite = nowSprite;
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
        if (pointerEventData.pointerDrag == null) return;
        iconImage.sprite = nowSprite;
        if (nowSprite == null)
            iconImage.color = Vector4.zero;
        else
            iconImage.color = Vector4.one;
    }
    public void OnDrop(PointerEventData pointerEventData)
    {
        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = droppedImage.sprite;
        nowSprite = droppedImage.sprite;
        iconImage.color = Vector4.one;

        dropname = pointerEventData.pointerDrag.name;
        Debug.Log(dropname);

       objectindex=base.GuestSearch(dropname);

        dropobject = Instantiate(guestobject[objectindex], new Vector3(transform.position.x, transform.position.y, 0),Quaternion.identity);
        dropobject.transform.SetParent(this.gameObject.transform);
    }
}

