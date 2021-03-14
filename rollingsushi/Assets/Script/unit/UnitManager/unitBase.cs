using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//食事席に配置されたお客さんのシステム部分を制御する
public class unitBase : MonoBehaviour
{
    //ユニットごとの情報
    protected float probability_like;//好きな寿司を食べる確率
    protected float probability_normal;//普通の寿司を食べる確率
    protected float waittime_like;//好きな寿司を食べた後、次の寿司を食べるまでの時間
    protected float waittime_normal;//普通の寿司を食べた後、次の寿司を食べるまでの時間
    protected string like;//好きな寿司の名前
    public int eatamount = 0;//寿司を食べる量
    protected float leavetime = 0.0f;//食事席を離れるまでの時間
    public float eventtime = 0.0f;//eventによる時間の上下
    protected int unittype;//1なら一人,2ならペア,4ならグループ
    protected bool setUnit = false;//お客さんが配置されているかどうか
    protected Skill skill;//お客さんの特殊能力

    //UI部分
    public GameObject clock_image; 
    public Image amount_guage,clock_guage;

    //効果音
    public AudioClip eat_SE;


    protected int amount;//食べた寿司の量を計測
    protected float leave;//ドロップしてから離席までの時間を計測    
    protected float eattime;//寿司を食べてからの時間を計測

    //サウンド関連
    protected float volume;

    protected bool skillflag = true;//スキルによる判定
    public bool poisonflag = false;//食当たりの判定
    
    protected RectTransform clockhand;//時計の針の角度を決める

    //ユニットのスキル
    protected Skill nowskill;
    protected List<Skill> skillList;

    protected GameManager gamemanager;
    protected UnitManager unitmanager;
    protected AudioSource audiosource;

    //寿司を食べるかどうかを決める際に必要
    public float Eat_Rate
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

    //次の寿司を食べるまで時間
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

    //好きな寿司を食べる確率
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

    //普通の寿司を食べる確率
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

    //寿司を食べる処理
    public bool Eat(sushidata sushidata)
    {

        Eat_Rate = Random.Range(0.0f, 10.0f);

        //寿司のデータを所得
        string name = sushidata.sushi_name;
        string type = sushidata.sushi_type;
        int price = sushidata.price;

        //寿司の値段が高いなら食べる確率を少し減らす
        if (price >= 200)
        {
            Eat_Rate += 1.5f;
        }
        else if (price >= 180)
        {
            Eat_Rate += 1.0f;
        }
        else if (price >= 150)
        {
            Eat_Rate += 0.5f;
        }

        //スキルによって食べない判定をする
        skillflag = skill.BeforeEat(price,unitmanager);
        if (!skillflag)
            return false;

        if (CheckLike(name,type,price))//好きな寿司かどうかを判定する
        {
            //好きな寿司の場合
            if (Eat_Rate < Rate_Like && eattime>waittime_base)
            {
                if (poisonflag)
                {
                    int poison = Random.Range(0, 20);
                    if (poison == 0)
                    {
                        GetPoison();//食当たりの発動
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
            //普通の寿司の場合
            if (Eat_Rate < Rate_Normal && eattime > waittime_base)
            {
                if (poisonflag)
                {
                    int poison = Random.Range(0, 20);
                    if (poison == 0)
                    {
                        GetPoison();//食当たりの発動
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

    //席を離れる時の処理
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
        unitmanager.PoisonAnim();
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
