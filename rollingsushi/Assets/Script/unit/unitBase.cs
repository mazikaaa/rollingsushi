﻿using System.Collections;
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
    public float leavetime=0.0f,eventtime=0.0f;//eventによる時間の上下
    public bool setUnit=false;
    public int unittype;//1なら一人,2ならペア,4ならグループ

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
    protected float eat_flag;//寿司を食べるかどうかの乱数を生成
    public bool poisonflag = false;
    
    protected RectTransform clockhand;//時計の針の角度を決める

    //ユニットのスキル
    protected Skill nowskill;
    protected List<Skill> skillList;

    protected GameObject gamemanager;
    protected AudioSource audiosource;

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
        volume = PlayerPrefs.GetFloat("SE", 1.0f);
        audiosource.volume *= volume;
        clockhand = clock_image.GetComponent<RectTransform>();
    }

    [System.Obsolete]
    public void Eat(GameObject sushi)
    {
        GameObject sushiobj = sushi.transform.Find("shshi_info").gameObject;
        sushidata sushi_data = sushiobj.GetComponent<sushidata>();

        eat_flag = Random.Range(0.0f, 10.0f);
        sushi_like = like;
        sushi_dislike = dislike;

        //寿司のデータを所得
        string name = sushi_data.sushi_name;
        string type = sushi_data.sushi_type;
        int price = sushi_data.price;

        //寿司の値段が高いなら食べる確率を少し減らす
        if (price >= 200)
        {
            eat_flag += 1.5f;
        }
        else if (price >= 180)
        {
            eat_flag += 1.0f;
        }
        else if (price >= 150)
        {
            eat_flag += 0.5f;
        }

        //スキルによって食べない判定をする
        skillflag = nowskill.BeforeEat(price);
        if (!skillflag)
            return;

        if (name == sushi_like || type == sushi_like)
        {
            if (eat_flag < probability_like && eattime>waittime_base)
            {
                Destroy(sushi);
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
                gamemanager.GetComponent<GameManager>().GainProfit(price);
            }
        }
        else if (name == sushi_dislike || type == sushi_dislike)
        {

        }
        else
        {
            if (eat_flag < probability_normal&& eattime > waittime_base)
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
                Destroy(sushi);
                float per = (float)amount / (float)eatamount;
                amount_guage.fillAmount = per;
                waittime_base = waittime_normal;
                eattime = 0;
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

    public void SetTime(float time)
    {
        leavetime = time - eventtime;
    }
}
