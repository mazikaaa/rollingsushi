using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Drag : Dragbase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform canvasTran;
    private GameObject draggingObject;
    public Image guage;
    public bool animeflag = false;


    void Awake()
    {
        canvasTran = this.gameObject.transform;
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
            draggingObject.transform.position = pointerEventData.position;
            animeflag = true;
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
        animeflag = false;
    }

    // ドラッグオブジェクト作成
    private void CreateDragObject()
    {
        draggingObject = new GameObject("Dragging Object");
        draggingObject.name = iconname;
        draggingObject.tag = "dragingobject";
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

    public override void DeleteUnit()
    {
        this.gameObject.GetComponent<Image>().sprite = null;
        this.gameObject.GetComponent<Image>().color = Vector4.zero;
        guageflag = "generate";
        generateicon = 10.0f - gamemanager.GetComponent<GameManager>().Rep * 0.5f;
        // Debug.Log(generateicon);
        deletetime = 0.0f;
        iconflag = false;
    }

    public bool SetAnimation()
    {
        if (animeflag)
            return true;
        else
            return false;
    }
}

