using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Drag_UnitSet : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform canvasTran;
    private RectTransform dragTransform, rectTransform;
    private GameObject draggingObject;
    public string iconname;
    Transform text;
    Text textdetail;
    GameObject textobj, unitdatabase;
    [SerializeField] GameObject findunit;
    // Start is called before the first frame update
    void Start()
    {
        canvasTran = this.gameObject.transform;
        rectTransform = GetComponent<RectTransform>();
        text = transform.Find("Text");
        textobj = text.gameObject;
        textdetail = textobj.GetComponent<Text>();
        unitdatabase = GameObject.Find("UnitDataBase");
        FindUnitObj();
        TextSet();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
         CreateDragObject();
        dragTransform = draggingObject.GetComponent<RectTransform>();
         Vector2 localPosition = GetLocalPosition(pointerEventData.position);
         dragTransform.localPosition = localPosition;
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        Vector2 localPosition = GetLocalPosition(pointerEventData.position);
        dragTransform.localPosition = localPosition;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        gameObject.GetComponent<Image>().color = Vector4.one;
        Destroy(draggingObject);
    }

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        textobj.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textobj.SetActive(false);
    }

    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPosition, Camera.main, out result);

        return result;
    }

    private void FindUnitObj()
    {
        int i;
        GameObject[] unitobject_copy = unitdatabase.GetComponent<UnitDataBase>().unitobject;
        string[] unitname_copy = unitdatabase.GetComponent<UnitDataBase>().unitname;

        for (i = 0; i < unitname_copy.Length; i++)
        {
            if (iconname == unitname_copy[i])
            {
                findunit= unitobject_copy[i];
            }
        }
    }

    private void TextSet()
    {
        textdata data = findunit.GetComponent<textdata>();

        string name = data.name;
        string like = data.like;
        string leavetime = data.leavetime;
        string amount = data.amount;
        string rate = data.rate;
        string recast = data.recast;
        string eattime = data.eattime;
        string cost = data.cost;
        string skill = data.skill;
        string skilldetail = data.skill_detail;

        textdetail.text = "名前:" + name + "\n" +
                      "好きな寿司:" + like + "\n" +
                      "食べる確率:" + rate + "\n" +
                      "食べる量:" + amount + "\n" +
                      "待機時間:" + leavetime + "\n" +
                      "リキャスト:" + recast + "\n" +
                       "着席時間:" + eattime + "\n" +
                       "特殊能力:" +skill +"\n";
        if (skill != "なし")
        {
            textdetail.text += skilldetail;
        }
    }

}
