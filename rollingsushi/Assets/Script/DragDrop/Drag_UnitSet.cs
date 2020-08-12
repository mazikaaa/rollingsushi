using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Drag_UnitSet : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform canvasTran;
    private GameObject draggingObject;
    public string iconname;
    GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        canvasTran = this.gameObject.transform;
        text = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
            CreateDragObject();
            draggingObject.transform.position = pointerEventData.position;   
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
            draggingObject.transform.position = pointerEventData.position;   
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
        text.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.SetActive(false);
    }
}
