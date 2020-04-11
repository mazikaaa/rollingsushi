using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Drag : Dragbase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvasTran;
    private GameObject draggingObject;
    public Image guage;

    void Awake()
    {
        canvasTran = this.gameObject.transform;
    }

    private void Start()
    {
        guage.color = Vector4.one * 0.2f;
    }
    private void Update()
    {
        //時間経過によるユニット生成
        if (guageflag == "generate")
        {
            generatetime += Time.deltaTime;
            guage.fillAmount = 1.0f - generatetime / generateicon;
            if (generatetime>=generateicon)
            {
                GenerateUnit();
            }
        }
        //時間経過によるユニット消去
        else if (guageflag == "delete")
        {
            deletetime += Time.deltaTime;
            guage.fillAmount = deletetime / deleteicon;
            if (deletetime >= deleteicon)
            {
                DeleteUnit();
                if (draggingObject)
                {
                   DeleteShadowUnit();
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
            draggingObject.transform.position = pointerEventData.position;
        }
     }

    public void OnDrag(PointerEventData pointerEventData)
    {
        //ユニットの画像が表示されているときだけオブジェクトを生成
        if (guageflag == "delete")
        {
            draggingObject.transform.position = pointerEventData.position;
        }
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        gameObject.GetComponent<Image>().color = Vector4.one;
       Destroy(draggingObject);
    }

    // ドラッグオブジェクト作成
    private void CreateDragObject()
    {
        draggingObject = new GameObject("Dragging Object");
        draggingObject.name = iconname;
        draggingObject.transform.SetParent(canvasTran);
        draggingObject.transform.SetAsLastSibling();
        draggingObject.transform.localScale = Vector3.one;

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
}

