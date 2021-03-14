using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
   [SerializeField] int eventNo=0;
    
    private Event nowEvent;

    [SerializeField] List<Event> eventList=new List<Event>();
    public GameObject eventBox; //発生するイベントを格納するオブジェクト

    private float volume;
  //  private bool  eventflag = false;

 　//掛け軸関連
    Animator kakeziku_anime;
    new AudioSource audio;

    public float eventspan,eventTime;

    //イベント用のUI
    public Text event_text, event_title, event_title2;
    public AudioClip kakeziku_SE;


    // Start is called before the first frame update
    void Start()
    {
        //掛け軸の初期化
        GameObject kakeziku = GameObject.Find("kakeziku");
        kakeziku_anime = kakeziku.GetComponent<Animator>();

        //サウンドの初期化
        audio = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("SE", 1.0f);
        audio.volume *= volume;

        //イベントの初期化
        Event[] events = eventBox.GetComponents<Event>();
        foreach (Event eve in events)
        {
            eventList.Add(eve);
            eve.InitEvent();
        }
        nowEvent = eventList[0];

    }

    // Update is called once per frame
    void Update()
    {
        eventTime += Time.deltaTime;

        //何もイベントが発生していないとき
        if (eventNo == 0)
        {
            if (eventTime > eventspan/2.0f)
            {
                nowEvent.ExitEvent();//前のイベントの効果を消す

                eventNo = Random.Range(1, eventList.Count);
                nowEvent = eventList[eventNo];//新しいイベントをセット
                SetCurrentEvent(nowEvent);//新しいイベントを発生させる
                eventTime = 0.0f;
            }

        }
        else //イベントが発生している時
        {
            if (eventTime > eventspan)
            {
                nowEvent.ExitEvent();//前のイベントの効果を消す

                eventNo = 0;
                nowEvent = eventList[eventNo];//新しいイベントをセット
                SetCurrentEvent(nowEvent);//新しいイベントを発生させる
                eventTime = 0.0f;
            }
        }
    }

    //イベントを実際に発生する関数
    protected void SetCurrentEvent(Event task)
    {
        task.ActionEvent();

        event_title2.text = task.GetTitle();
        event_text.text = task.GetText();

        if (eventNo != 0)
        { 
        kakeziku_anime.SetTrigger("Kakeziku");
        Invoke("PlaySE", 0.6f);
        }

        event_title.text = task.GetTitle();
    }

    private void PlaySE()
    {
        audio.PlayOneShot(kakeziku_SE);
    }

}
