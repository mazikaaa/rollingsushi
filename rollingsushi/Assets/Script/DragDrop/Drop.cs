using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : Dropbase, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int objectindex;
    private Sprite nowSprite;
    private GameObject dropobject;
    private string dropname;

    protected void Start()
    {
        base.Start();
        nowSprite = null;
        iconImage.sprite = nowSprite;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;

        //ドラック側でユニットを生成している時だけ画像を表示する
        if (pointerEventData.pointerDrag.GetComponent<Drag>().iconflag)
        {
            Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
            iconImage.sprite = droppedImage.sprite;
            iconImage.color = Vector4.one * 0.6f;
        }
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
        //ドラック側でユニットを生成している時だけ画像を切り替える
        if (pointerEventData.pointerDrag.GetComponent<Drag>().iconflag)
        {
            Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
            iconImage.sprite = droppedImage.sprite;
            nowSprite = droppedImage.sprite;
            iconImage.color = Vector4.one;

            dropname = pointerEventData.pointerDrag.transform.GetChild(2).name;
            Debug.Log(dropname);

            //ドロップした画像と適合する名前のオブジェクトがあればオブジェクトを生成
            objectindex = base.GuestSearch(dropname);

            dropobject = Instantiate(unitobject[objectindex], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            dropobject.transform.SetParent(this.gameObject.transform);
        }
     }
}

