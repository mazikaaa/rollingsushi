using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Drag : Dragbase
{
    //お客さんの配列
    private GameObject[] unitobject = new GameObject[10];
    private string[] unitname = new string[10];

    //ドラッグしたオブジェクト関連
    private Transform canvasTran;
    private RectTransform dragTransform, rectTransform;
    private GameObject draggingObject;
    private bool animeflag = false;

    public bool iconflag = false;

    //お客さんの生成確率関連
    Dictionary<int, float> chooserate;
    private int[] unitrate = { 10, 10, 10, 10, 10, 10, 10, 10 };

    //お客さんの生成・消去関連
    private float deleteicon_time, generateicon_time = 5.0f;//消去、生成までの時間
    private string iconname;
    private string guageflag = "generate";//生成モード・消去モードかを決める
    private float deletetime, generatetime;
    private int unittype;

    private GameManager gamemanager;
    private UnitDataBase unitdatabase;
    public float eventplustime = 0.0f;
    public Image guage;


    void Awake()
    {
        canvasTran = this.gameObject.transform;
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        unitdatabase = GameObject.Find("UnitDataBase").GetComponent<UnitDataBase>();
        //お客さんをセットする
        SetUnit();

        guage.color = Vector4.one * 0.2f;
    }
    private void Update()
    {
        //時間経過によるユニット生成
        if (guageflag == "generate")
        {
            generatetime += Time.deltaTime;
            guage.fillAmount = 1.0f - generatetime / generateicon_time;
            if (generatetime >= generateicon_time)
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
                DeleteUnit();
                if (draggingObject)
                {
                    DeleteShadowUnit();//ドラッグしている時にユニット消去されたときの対策
                    Destroy(draggingObject);
                }
            }
        }
    }


    //お客さんの生成関数
    protected void GenerateUnit()
    {
        int i;
        chooserate = new Dictionary<int, float>();

        for (i = 0; i < unitrate.Length; i++)
        {
            chooserate.Add(i, unitrate[i]);
        }
        int unitkey = Choose(chooserate);

        iconname = unitobject[unitkey].name;
        deleteicon_time = unitobject[unitkey].GetComponentInChildren<Generatedata>().deletespan;
        guage.fillOrigin = 0;
        this.gameObject.GetComponent<Image>().sprite = unitobject[unitkey].GetComponentInChildren<Generatedata>().Generateicon;
        gameObject.GetComponent<Image>().color = Vector4.one;
        unittype = unitobject[unitkey].GetComponent<Unitdata>().unittype;
        guageflag = "delete";
        generatetime = 0.0f;
        iconflag = true;
    }

    //半透明のお客さんを消去する
    public void DeleteShadowUnit()
    {
        GameObject[] drops = GameObject.FindGameObjectsWithTag("drop");
        foreach (GameObject drop in drops)
        {
            if (drop.GetComponent<Drop>().shadowUnit)
            {
                drop.GetComponent<Drop>().ExitShadowImage();
            }
        }
    }

    //お客さんを消去する
    public void DeleteUnit(float time = 0.0f)
    {
        this.gameObject.GetComponent<Image>().sprite = null;
        this.gameObject.GetComponent<Image>().color = Vector4.zero;
        guageflag = "generate";

        //評判の値,イベントによる効果によって次のお客が来るまでの時間を決める
        generateicon_time = 11.0f - gamemanager.Rep * 0.6f + eventplustime + time;
        guage.fillOrigin = 1;
        unittype = 0;
        deletetime = 0.0f;
        iconflag = false;
    }



    //ユニット編成画面で編成したユニットを生成候補にセットする
    public void SetUnit()
    {
        int i, j;

        string checkname;
        string[] unitnamedata = new string[24];
        int length = unitdatabase.GetComponent<UnitDataBase>().unitname.Length;

        //先にデータベースからユニット名前を取ってくる
        for (j = 0; j < length; j++)
        {
            unitnamedata[j] = unitdatabase.unitname[j];
        }

        for (i = 0; i < 8; i++)//選択したユニットの数
        {
            checkname = PlayerPrefs.GetString("Unit" + (i + 1));
            for (j = 0; j < 24; j++)//データベースにあるユニットの数
            {
                //名前が一致するオブジェクトを生成候補に追加
                if (checkname == unitnamedata[j])
                {
                    unitobject[i] = unitdatabase.unitobject[j];
                    unitname[i] = unitdatabase.unitname[j];
                }
            }
        }
    }

    //生成されるお客さんを選ぶ
    private int Choose(Dictionary<int, float> dic)
    {
        int i = 0;
        float total = 0;

        foreach (KeyValuePair<int, float> elem in dic)
        {
            total += elem.Value;
            //Debug.Log(elem.Value);
        }

        float randomPoint = UnityEngine.Random.value * total;

        foreach (KeyValuePair<int, float> elem in dic)
        {
            if (randomPoint < elem.Value)
            {
                for (i = 0; i < unitrate.Length; i++)
                {
                    //今回選ばれたユニットは選ばれにくくする
                    if (i == elem.Key)
                    {
                        unitrate[i] -= 7;
                        if (unitrate[i] < 0)
                        {
                            unitrate[i] = 0;
                        }
                    }
                    else
                    {
                        unitrate[i] += 1;
                    }

                }
                return elem.Key;
            }
            else
            {
                randomPoint -= elem.Value;
            }
        }
        return 0;
    }


    public override void OnBeginDrag(PointerEventData pointerEventData)
    {
        //ユニットの画像が表示されているときだけオブジェクトを生成
        if (guageflag == "delete")
        {
            CreateDragObject();
            dragTransform = draggingObject.GetComponent<RectTransform>();
            Vector2 localPosition = GetLocalPosition(pointerEventData.position);
            dragTransform.localPosition = localPosition;
            animeflag = true;
        }
    }

    public override void OnDrag(PointerEventData pointerEventData)
    {
        //ユニットの画像が表示されているときだけオブジェクトを生成
        if (guageflag == "delete")
        {
            Vector2 localPosition = GetLocalPosition(pointerEventData.position);
            dragTransform.localPosition = localPosition;
        }
    }

    public override void OnEndDrag(PointerEventData pointerEventData)
    {
        gameObject.GetComponent<Image>().color = Vector4.one;
        dragTransform = null;
        Destroy(draggingObject);
        animeflag = false;
    }

    // ドラッグオブジェクト作成
    public override void CreateDragObject()
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
            case 3:
                draggingObject.tag = "dragingobject_party";
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

    public override bool SetAnimation()
    {
        if (animeflag)
            return true;
        else
            return false;
    }

    public override Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPosition, Camera.main, out result);

        return result;
    }
}