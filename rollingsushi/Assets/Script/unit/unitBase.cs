using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitBase : MonoBehaviour
{
    public int eat_like;
    public int eat_normal;
    public float waittime_like;
    public float waittime_normal;
    public string like;
    public string dislike;

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
            if (eat_flag < eat_like)
            {
                Destroy(sushi);
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
            if (eat_flag < eat_normal)
            {
                Destroy(sushi);
                waittime_base = waittime_normal;
            }

        }
    }


}
