using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//食事席に関するシステム（ドロップの機構）
public class Drop : DropBase
{
    private GameObject dropobject;
    private string dropname,tagname;

    //音楽関係の変数
    private float volume;
    AudioSource audiosource;
    public AudioClip sit_SE;

    //ユニット関連
    public bool setUnit = false;//ユニットがすでに設置されているかを判断
    public bool shadowUnit = false;//色が薄いユニットが表示される状態にあるか。
    public int unitcapable;//許されるユニットの形態
    public GameObject[] unitobject = new GameObject[8];
    public string[] unitname = new string[8];
    protected string[] default_unit = { "DK", "JK", "OL", "salaryman", "wife", "oldman", "DKpair", "salarypair" };//初期のユニットの編成
    protected string setdropname;//席に配置されるユニットの名前を入れる
    protected int unittype;

    //ユニットの画像関連
    protected Sprite[] unitsprite = new Sprite[4];
    protected Sprite[] nowSprite = new Sprite[4];
    public Image[] iconImage = new Image[1];

    //オブジェクト関連
    private UnitDataBase unitdatabase;
    private UnitManager unitmanager;

    void Start()
    {
        //待ち席の画像を初期化
        int i = 0;
        foreach (Image icon in iconImage)
        {
            nowSprite[i] = null;
            icon.sprite = nowSprite[i];
            i++;
        }

        //効果音の初期化
        audiosource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("SE", 1.0f);
        audiosource.volume *= volume;

        //お客さんの候補をセットする
        unitdatabase = GameObject.Find("UnitDataBase").GetComponent<UnitDataBase>();
        unitmanager = GetComponent<UnitManager>();
        SetUnit();
    }


    public override void OnPointerEnter(PointerEventData pointerEventData)
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

    public override void OnPointerExit(PointerEventData pointerEventData)
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
    public override void OnDrop(PointerEventData pointerEventData)
    {
        //ドラック側でユニットを生成している時
        if (pointerEventData.pointerDrag.GetComponent<Drag>().iconflag==true && setUnit==false)
        {
            dropname = pointerEventData.pointerDrag.transform.GetChild(2).name;
            tagname = pointerEventData.pointerDrag.transform.GetChild(2).tag;
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

    //ドロップされるお客さんに見合った画像を食事席に表示する（半透明）
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
                for (i = 0; i < unittype; i++)
                {
                    iconImage[i].sprite = unitsprite[i];
                    iconImage[i].color = Vector4.one*0.6f;
                }
                break;
            case "dragingobject_party":
                FetchUnitImage();
                for (i = 0; i < unittype; i++)
                {
                    iconImage[i].sprite = unitsprite[i];
                    iconImage[i].color = Vector4.one * 0.6f;
                }
                break;
        }
        shadowUnit = true;
    }

    //ドロップされるお客さんに見合った画像を食事席に表示する
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
                for (i = 0; i < unittype; i++)
                {
                    iconImage[i].sprite = unitsprite[i];
                    nowSprite[i] = unitsprite[i];
                    iconImage[i].color = Vector4.one;
                }
                break;
            case "dragingobject_party":
                FetchUnitImage();
                for (i = 0; i <unittype ; i++)
                {
                    iconImage[i].sprite = unitsprite[i];
                    nowSprite[i] = unitsprite[i];
                    iconImage[i].color = Vector4.one;
                }

                break;
        }
    }

    //食事席に表示されているお客さんの画像を消す
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
 
    //半透明で表示されているお客さんを消す
    public void ExitShadowImage()
    {
        foreach (Image icon in iconImage)
        {
            icon.color = Vector4.zero;
        }
        shadowUnit=false;
    }

    //ユニットのタイプ（1人、2人、4人）を確認
    public bool CheckUnitType(string name)
    {
        int i;
        int num;//お客さんの人数

        for (i = 0; i < unitname.Length; i++)
        {
            if (name == unitname[i])
            {
                num = unitobject[i].GetComponent<Unitdata>().unittype;
                if (num<= unitcapable)
                {
                    setdropname = name;
                    unittype = num;
                    return true;
                }
                else
                    return false;
            }
        }
        return false;
    }

    //配置されるお客さんが2人以上の場合、画像を1人ずつ分割する
    public void FetchUnitImage()
    {
        int i, j;
        for (i = 0; i < unitname.Length; i++)
        {
            if (setdropname == unitname[i])
            {
                for (j = 0; j < unittype; j++)
                {
                    unitsprite[j] = unitobject[i].GetComponent<Unitdata>().Separate_image[j];
                }
            }
        }
    }

    //ソート済みのユニットから名前による検索
    public void UnitSearch(string name)
    {
        int i;

        for (i = 0; i < unitname.Length; i++)
        {
            if (name == unitname[i])
            {
                Unitdata unitdata = unitobject[i].GetComponent<Unitdata>();

                //ユニットの情報を書き込む
                unitmanager.SetUnit(unitdata);
                this.gameObject.GetComponentInChildren<UnitCollider>().setUnitManager(unitmanager);
                setUnit = true;
            }
        }
    }

    //お客さんの候補を予めセットしておく
    public void SetUnit()
    {
        int i, j;
        string checkname;
        string[] unitnamedata = new string[24];
        int length = unitdatabase.unitname.Length;

        //先にデータベースからユニット名前を取ってくる
        for (j = 0; j < length; j++)
        {
            unitnamedata[j] = unitdatabase.unitname[j];
        }

        for (i = 0; i < 8; i++)//選択したユニットの数
        {
            checkname = PlayerPrefs.GetString("Unit" + (i + 1), default_unit[i]);
            for (j = 0; j < 24; j++)//データベースにあるユニットの数
            {
                //名前が一致するオブジェクトを生成候補に追加
                if (checkname == unitnamedata[j])
                {
                    unitobject[i] = unitdatabase.unitobject[j];
                    unitname[i] = unitdatabase.unitname[j];
                    break;
                }
            }
        }
    }
}

