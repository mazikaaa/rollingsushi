using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Drag : Dragbase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvasTran;
    private RectTransform dragTransform,rectTransform;
    private GameObject draggingObject;
    public Image guage;
    public bool animeflag = false;


    void Awake()
    {
        canvasTran = this.gameObject.transform;
        rectTransform = GetComponent<RectTransform>();
    }

    new void Start()
    {
        base.Start();
        guage.color = Vector4.one * 0.2f;
    }
    private void Update()
    {
        //時間経過によるユニット生成
        if (guageflag == "generate")
        {
            generatetime += Time.deltaTime;
            guage.fillAmount = 1.0f - generatetime / generateicon_time;
            if (generatetime>=generateicon_time)
            {
                GenerateUnit();
            }
        }
        //時間経過によるユニット消去
        else if (guageflag == "delete")
        {
            deletetime += Time.deltaTime;
            guage.fillAmount = deletetime / deleteicon_time;
            if (deletetime >= deleteicon_time)
            {
               // gamemanager.GetComponent<GameManager>().LowerRep(); 評判を下げる
                DeleteUnit();
                if (draggingObject)
                {
                   DeleteShadowUnit();//ドラッグしている時にユニット消去されたときの対策
                   Destroy(draggingObject);
                }
            }
        }
    }


    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        //ユニットの画像が表示されているときだけオブジェクトを生成
        if (guageflag == "delete")
        {
            CreateDragObject();
            dragTransform = draggingObject.GetComponent<RectTransform>();
            Vector2 localPosition = GetLocalPosition(pointerEventData.position);
            dragTransform.localPosition = localPosition;
            //draggingObject.transform.position = pointerEventData.position;
            animeflag = true;
        }
     }

    public void OnDrag(PointerEventData pointerEventData)
    {
        //ユニットの画像が表示されているときだけオブジェクトを生成
        if (guageflag == "delete")
        {
            Vector2 localPosition = GetLocalPosition(pointerEventData.position);
            dragTransform.localPosition = localPosition;
            //draggingObject.transform.position = pointerEventData.position;
        }
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        gameObject.GetComponent<Image>().color = Vector4.one;
        dragTransform = null;
        Destroy(draggingObject);
        animeflag = false;
    }

    // ドラッグオブジェクト作成
    private void CreateDragObject()
    {
        draggingObject = new GameObject("Dragging Object");
        draggingObject.name = iconname;
        draggingObject.transform.SetParent(canvasTran);
        draggingObject.transform.SetAsLastSibling();
        draggingObject.transform.localScale = Vector3.one;

        //ユニットのタイプによってタグを変える
        switch (unittype)
        {
            case 1:
                draggingObject.tag = "dragingobject";
                break;
            case 2:
                draggingObject.tag = "dragingobject_pair";
                break;
            case 4:
                draggingObject.tag = "dragingobject_party";
                break;
        }

        // レイキャストがブロックされないように
        CanvasGroup canvasGroup = draggingObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        Image draggingImage = draggingObject.AddComponent<Image>();
        Image sourceImage = GetComponent<Image>();

        draggingImage.sprite = sourceImage.sprite;
        draggingImage.rectTransform.sizeDelta = sourceImage.rectTransform.sizeDelta;
        draggingImage.color = sourceImage.color;
        draggingImage.material = sourceImage.material;

        gameObject.GetComponent<Image>().color = Vector4.one * 0.6f;
    }

    public bool SetAnimation()
    {
        if (animeflag)
            return true;
        else
            return false;
    }

    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPosition, Camera.main, out result);

        return result;
    }
}

