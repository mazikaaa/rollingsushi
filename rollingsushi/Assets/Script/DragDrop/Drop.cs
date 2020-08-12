using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Drop : Dropbase, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite nowSprite;
    public AudioClip sit_SE;
    private GameObject dropobject;
    private string dropname;

    private AudioSource audiosource;

    protected new void Start()
    {
        base.Start();
        nowSprite = null;
        iconImage.sprite = nowSprite;
        audiosource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;

        //ドラック側でユニットを生成している時だけ画像を表示する
        //またユニットがすでにいる時は表示しない
        if (pointerEventData.pointerDrag.GetComponent<Drag>().iconflag==true && setUnit==false)
        {
            dropname = pointerEventData.pointerDrag.transform.GetChild(2).name;
            //Debug.Log(dropname);
            //そのユニットがこの場所におけるかどうかをチェックする
            if (CheckUnitType(dropname))
            {
                Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
                iconImage.sprite = droppedImage.sprite;
                iconImage.color = Vector4.one * 0.6f;
                shadowUnit = true;
            }
        }
     
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
        //ドラック側でユニットを生成している時
        if (pointerEventData.pointerDrag.GetComponent<Drag>().iconflag==true && setUnit==false)
        {
            dropname = pointerEventData.pointerDrag.transform.GetChild(2).name;
            //Debug.Log(dropname);
             //そのユニットがこの場所におけるかどうかをチェックする
            if (CheckUnitType(dropname))
            {
                Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
                iconImage.sprite = droppedImage.sprite;
                nowSprite = droppedImage.sprite;
                iconImage.color = Vector4.one;

                //ドロップした画像と適合する名前のオブジェクトがあればオブジェクトを生成
                UnitSearch(dropname);
                this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
                audiosource.PlayOneShot(sit_SE);

                //ドロップしたらドラック側の画像を消す,そしてshadowUnitをfalseに
                pointerEventData.pointerDrag.GetComponent<Drag>().DeleteUnit();
                shadowUnit = false;
            }
            
        }
    }

}

