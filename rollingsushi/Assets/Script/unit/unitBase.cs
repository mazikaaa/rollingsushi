using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unitBase : MonoBehaviour
{
    //ユニットごとの情報
    protected float probability_like;
    protected float probability_normal;
    protected float waittime_like;
    protected float waittime_normal;
    protected string like;
    protected string dislike;
    protected int cost;
    protected Skill skill;
    public int eatamount=0;
    public float leavetime=0.0f,eventtime=0.0f;//eventによる時間の上下
    protected bool setUnit=false;
    protected int unittype;//1なら一人,2ならペア,4ならグループ

    public GameObject clock_image; 
    public Image amount_guage,clock_guage;

    //効果音
    public AudioClip eat_SE;

    //ユニットが変わるたびに初期化
    protected int amount;//食べた寿司の量を計測
    protected float leave;//ドロップしてから離席までの時間を計測
    protected float eattime;//寿司を食べてからの時間を計測
    protected float volume;

    protected bool skillflag = true;//スキルによる判定
    public bool poisonflag = false;
    
    protected RectTransform clockhand;//時計の針の角度を決める

    //ユニットのスキル
    protected Skill nowskill;
    protected List<Skill> skillList;

    protected GameManager gamemanager;
    protected UnitManager unitmanager;
    protected AudioSource audiosource;

    public float Eat_Flag
    {
        get
        {
            return eat_flag;
        }
        set
        {
            eat_flag = value;
        }
    }
    protected float eat_flag;


    public float waittime_base
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

    public float Rate_Like
    {
        get
        {
            return rate_like;
        }
        set
        {
            rate_like = value;
        }
    }
    protected float rate_like;

    public float Rate_Normal
    {
        get
        {
            return rate_normal;
        }
        set
        {
            rate_normal = value;
        }
    }
    protected float rate_normal;

    protected void Start()
    {
        waittime_base = 0;

        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audiosource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("SE", 1.0f);
        audiosource.volume *= volume;
        clockhand = clock_image.GetComponent<RectTransform>();
    }

    [System.Obsolete]
    public bool Eat(sushidata sushidata)
    {

        Eat_Flag = Random.Range(0.0f, 10.0f);

        //寿司のデータを所得
        string name = sushidata.sushi_name;
        string type = sushidata.sushi_type;
        int price = sushidata.price;

        //寿司の値段が高いなら食べる確率を少し減らす
        if (price >= 200)
        {
            Eat_Flag += 1.5f;
        }
        else if (price >= 180)
        {
            Eat_Flag += 1.0f;
        }
        else if (price >= 150)
        {
            Eat_Flag += 0.5f;
        }

        //スキルによって食べない判定をする
        skillflag = skill.BeforeEat(price,unitmanager);
        if (!skillflag)
            return false;

        if (CheckLike(name,type,price))
        {
            if (Eat_Flag < Rate_Like && eattime>waittime_base)
            {
                if (poisonflag)
                {
                    int poison = Random.Range(0, 20);
                    if (poison == 0)
                    {
                        GetPoison();
                    }
                }

                amount += 1;
                float per = (float)amount / (float)eatamount;
                amount_guage.fillAmount = per;
                amount_guage.fillAmount = per;
                waittime_base = waittime_like;
                eattime = 0;
                audiosource.PlayOneShot(eat_SE);
                skill.AfterEat(price,gamemanager,unitmanager,true);
                gamemanager.GainProfit(price);
                return true;
            }
            return false;
        }
        else
        {
            if (Eat_Flag < Rate_Normal && eattime > waittime_base)
            {
                if (poisonflag)
                {
                    int poison = Random.Range(0, 20);
                    if (poison == 0)
                    {
                        GetPoison();
                    }
                }
                amount += 1;
                float per = (float)amount / (float)eatamount;
                amount_guage.fillAmount = per;
                waittime_base = waittime_normal;
                eattime = 0;
                audiosource.PlayOneShot(eat_SE);
                skill.AfterEat(price,gamemanager,unitmanager,false);
                gamemanager.GainProfit(price);
                return true;
            }
            return false;
        }
    }
    public void Leave()
    {
        skill.LeaveSkill(amount,gamemanager,unitmanager);
        amount = 0;
        leave = 0.0f;
        this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<Drop>().ExitImage();
        this.gameObject.GetComponent<UnitManager>().setUnit = false;

        amount_guage.fillAmount = 0.0f;
        clock_guage.fillAmount = 0.0f;
        clockhand.localRotation = Quaternion.Euler(0, 0, 0);
    }

    //食あたりを発生させる。
    protected void GetPoison()
    {
        gamemanager.GetComponent<GameManager>().LowerRep();
        Leave();
    }

    //食べる寿司が好きかどうかを判定
    protected bool CheckLike(string name,string type,int price)
    {
        int i;

        string[] names = name.Split(',');

        switch (like)
        {
            case"cheap":
                if (price < 150)
                    return true;
                return false;
            case "rich":
                if (price >= 180)
                    return true;
                return false;
            default:
                for (i = 0; i < names.Length; i++)
                {
                    if (names[i] == like || type == like)
                        return true;
                }
                return false;
        }
    }

    //ユニットをセットした時に、各データを入れ込む
    public void SetUnit(Unitdata unitdata)
    {
        Rate_Like = unitdata.probability_like;
        Rate_Normal = unitdata.probability_normal;
        waittime_like = unitdata.waittime_like;
        waittime_normal = unitdata.waittime_normal;
        like = unitdata.like;
        skill = unitdata.skill;
        eatamount = unitdata.eatamount;
        setUnit = true;
        SetTime(unitdata.leavetime);

    }

    //着席時間をセットする
    public void SetTime(float time)
    {
        leavetime = time - eventtime;
    }
}
