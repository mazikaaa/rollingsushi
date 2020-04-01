using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitBase : MonoBehaviour
{
    //ユニットごとの情報
    public int probability_like;
    public int probability_normal;
    public float waittime_like;
    public float waittime_normal;
    public string like;
    public string dislike;
    public int eatamount=0;
    public float leavetime=0.0f;

    //ユニットが変わるたびに初期化
    protected int amount;//食べた寿司の量を計測
    protected float leave;//ドロップしてから離席までの時間を計測

    protected int eat_flag;
    public string sushi_like
    {
        get
        {
            return _like;
        }
        set
        {
            _like = value;
            Debug.Log(_like);
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
        waittime_base = waittime_normal;
       
        sushi_like=like;
        sushi_dislike=dislike;
    }

    public void Eat(string name,string type ,GameObject sushi)
    {
        eat_flag = Random.Range(1, 10);
        sushi_like = like;
        sushi_dislike = dislike;
        if (name==sushi_like)
        {
            Debug.Log("好きですが何か？");
            if (eat_flag < probability_like)
            {
                Destroy(sushi);
                amount += 1;
                waittime_base = waittime_like;
            }
        }
        else if (type== sushi_dislike)
        {
            Debug.Log("嫌いですが何か？");
        }
        else
        {
            Debug.Log("普通ですが何か？");
            if (eat_flag < probability_normal)
            {
                Destroy(sushi);
                amount += 1;
                waittime_base = waittime_normal;
            }

        }
    }

    protected void Leave(){
        amount = 0;
        leave = 0.0f;
        this.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<Drop>().iconImage.sprite = null;
        this.gameObject.GetComponent<Drop>().iconImage.color= Vector4.zero;
    }
}
