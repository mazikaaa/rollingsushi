using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unitBase : MonoBehaviour
{
    //ユニットごとの情報
    public float probability_like;
    public float probability_normal;
    public float waittime_like;
    public float waittime_normal;
    public string like;
    public string dislike;
    public int cost;
    public int skillNo = 0;
    public int eatamount=0;
    public float leavetime=0.0f;
    public bool setUnit=false;
    public int unittype;//1なら一人,2ならペア,4ならグループ

    public GameObject clock_image; 
    public Image amount_image;

    //効果音
    public AudioClip eat_SE;

    //ユニットが変わるたびに初期化
    protected int amount;//食べた寿司の量を計測
    protected float leave;//ドロップしてから離席までの時間を計測

    protected bool skillflag = true;//スキルによる判定
    protected float eat_flag;//寿司を食べるかどうかの乱数を生成
    protected RectTransform clockhand;//時計の針の角度を決める

    //ユニットのスキル
    protected Skill nowskill;
    protected List<Skill> skillList;

    protected GameObject gamemanager;
    AudioSource audiosource;

    public string sushi_like
    {
        get
        {
            return _like;
        }
        set
        {
            _like = value;
           // Debug.Log(_like);
        }
    }
    protected string _like;
    public string sushi_dislike
    {
        get
        {
            return _dislike;
        }
        set
        {
            _dislike = value;
        }
    }
    protected string _dislike;
    protected float waittime_base
    {
        get
        {
            return _waittime;
        }
        set
        {
            _waittime = value;
        }
    }
    protected float _waittime;



    protected void Start()
    {
        waittime_base = 0;
       
        sushi_like=like;
        sushi_dislike=dislike;

        gamemanager = GameObject.Find("GameManager");
        audiosource = GetComponent<AudioSource>();
        clockhand = clock_image.GetComponent<RectTransform>();
    }
    public void Eat(GameObject sushi)
    {
        eat_flag = Random.Range(0.0f, 10.0f);
        sushi_like = like;
        sushi_dislike = dislike;

        //寿司のデータを所得
        string name = sushi.transform.GetChild(4).gameObject.GetComponent<sushidata>().sushi_name;
        string type = sushi.transform.GetChild(4).gameObject.GetComponent<sushidata>().sushi_type;
        int price = sushi.transform.GetChild(4).gameObject.GetComponent<sushidata>().price;

        //スキルによって食べない判定をする
        skillflag = nowskill.BeforeEat(price);
        if (!skillflag)
            return;

        if (name == sushi_like || type == sushi_like)
        {
            if (eat_flag < probability_like)
            {
                Destroy(sushi);
                amount += 1;
                float per = (float)amount / (float)eatamount;
                amount_image.fillAmount = per;
                waittime_base = waittime_like;
                audiosource.PlayOneShot(eat_SE);
                gamemanager.GetComponent<GameManager>().GainProfit(price);
            }
        }
        else if (name == sushi_dislike || type == sushi_dislike)
        {

        }
        else
        {
            if (eat_flag < probability_normal)
            {
                amount += 1;
                Destroy(sushi);
                float per = (float)amount / (float)eatamount;
                amount_image.fillAmount = per;
                waittime_base = waittime_normal;
                audiosource.PlayOneShot(eat_SE);
                gamemanager.GetComponent<GameManager>().GainProfit(price);
            }
        }
    }

    protected void Leave()
    {
        amount = 0;
        leave = 0.0f;
        this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<Drop>().ExitImage();
        this.gameObject.GetComponent<UnitManagr>().setUnit = false;

        amount_image.fillAmount = 0.0f;
    }

    public void SetSkill(int No)
    {
        skillList = new List<Skill>()
        {
            new NoneSkill(),
            new NotEatExpensive(),
        };

        nowskill = skillList[No];
        //Debug.Log(nowskill);
    }
}
