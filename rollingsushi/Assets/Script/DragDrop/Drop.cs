using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Drop : Dropbase, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected Sprite[] nowSprite=new Sprite[4];
    public AudioClip sit_SE;
    public Image[] iconImage=new Image[1];

    private GameObject dropobject;
    private string dropname,tagname;

    AudioSource audiosource;

    new void Start()
    {
        int i = 0;

        foreach (Image icon in iconImage)
        {
            nowSprite[i] = null;
            icon.sprite = nowSprite[i];
            i++;
        }
        audiosource = GetComponent<AudioSource>();
        base.Start();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;

        //ドラック側でユニットを生成している時だけ画像を表示する
        //またユニットがすでにいる時は表示しない
        if (pointerEventData.pointerDrag.GetComponent<Drag>().iconflag==true && setUnit==false)
        {
            dropname = pointerEventData.pointerDrag.transform.GetChild(2).name;
            tagname= pointerEventData.pointerDrag.transform.GetChild(2).tag;

            //そのユニットがこの場所におけるかどうかをチェックする
            if (CheckUnitType(dropname))
            {
                SetImage_Enter(pointerEventData,tagname);
            }
        }
     
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        int i = 0;

        if (pointerEventData.pointerDrag == null)
            return;

        foreach (Image icon in iconImage)
        {
            icon.sprite = nowSprite[i];
            if (nowSprite[i] == null)
                icon.color = Vector4.zero;
            else
                icon.color = Vector4.one;
            i++;
        }
    }
    public void OnDrop(PointerEventData pointerEventData)
    {
        //ドラック側でユニットを生成している時
        if (pointerEventData.pointerDrag.GetComponent<Drag>().iconflag==true && setUnit==false)
        {
            dropname = pointerEventData.pointerDrag.transform.GetChild(2).name;
            tagname = pointerEventData.pointerDrag.transform.GetChild(2).tag;
            //Debug.Log(dropname);
            //そのユニットがこの場所におけるかどうかをチェックする
            if (CheckUnitType(dropname))
            {
                SetImage_OnDrop(pointerEventData,tagname);

                //ドロップした画像と適合する名前のオブジェクトがあればオブジェクトを生成
                UnitSearch(dropname);
                this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
                audiosource.PlayOneShot(sit_SE);

                //ドロップしたらドラック側の画像を消す,そして薄く表示されているユニットを消す
                pointerEventData.pointerDrag.GetComponent<Drag>().DeleteUnit();
                shadowUnit = false;
            }
            
        }
    }

    public void SetImage_Enter(PointerEventData pointer, string type)
    {
        int i = 0;

        switch (type) {
            case "dragingobject":
                Image droppedImage = pointer.pointerDrag.GetComponent<Image>();
                iconImage[0].sprite = droppedImage.sprite;
                iconImage[0].color = Vector4.one * 0.6f;
                break;
            case "dragingobject_pair":
                FetchUnitImage();
                foreach (Image icon in iconImage)
                {
                    icon.sprite = unitsprite[i];
                    icon.color = Vector4.one*0.6f;
                }
                break;
        }

        shadowUnit = true;
    }

    public void SetImage_OnDrop(PointerEventData pointer, string type)
    {
        int i = 0;

        switch (type)
        {
            case "dragingobject":
                Image droppedImage = pointer.pointerDrag.GetComponent<Image>();
                iconImage[0].sprite = droppedImage.sprite;
                nowSprite[0] = droppedImage.sprite;
                iconImage[0].color = Vector4.one;
                break;
            case "dragingobject_pair":
                FetchUnitImage();
                foreach(Image icon in iconImage)
                {
                    icon.sprite = unitsprite[i];
                    nowSprite[i] = unitsprite[i];
                    icon.color = Vector4.one;
                    i++;
                }
                break;

        }

    }

    public void ExitImage()
    {
        int i = 0;
        foreach(Image icon in iconImage)
        {
            icon.sprite = null;
            icon.color =Vector4.zero;
            nowSprite[i]=null;
            i++;
        }
        setUnit = false;
    }
 
    public void ExitShadowImage()
    {
        foreach (Image icon in iconImage)
        {
            icon.color = Vector4.zero;
        }
        shadowUnit=false;
    }
}

