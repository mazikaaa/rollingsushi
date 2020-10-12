using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
   [SerializeField] int eventNo=0;
   // public Event[] events=new Event[5];//発生するイベント格納する
    
    private Event nowEvent=null;
    private List<Event> eventList;

    private float eventTime;
    private bool  eventflag = false;
    private int eventnum=0;//イベントの数

    Animator kakeziku_anime;
    AudioSource audio;

    //イベント用のUI
    public Text event_text,event_title,event_title2;
    public AudioClip kakeziku_SE;


    // Start is called before the first frame update
    void Start()
    {
        GameObject kakeziku = GameObject.Find("kakeziku");
        kakeziku_anime = kakeziku.GetComponent<Animator>();

        audio = GetComponent<AudioSource>();

        eventList = new List<Event>
        {
            new NoneEvent(),
            new GenerateSpeedUp(),
            new SushiSpeedUp(),
            new ProfitDown(),
            new Claim(),
        };
       

        nowEvent = null;
    }

    // Update is called once per frame
    void Update()
    {
        eventTime += Time.deltaTime;

        if (eventTime > 20.0f)
        {
            if(nowEvent!=null)
            nowEvent.ExitEvent();//前のイベントの効果を消す

            if ( eventNo== 0)
                eventNo = Random.Range(0,3);
            else
                eventNo = 0;

            nowEvent = eventList[eventNo];//新しいイベントをセット
            SetCurrentEvent(nowEvent);//新しいイベントを発生させる
            eventTime = 0.0f;
        }
    }

    protected void SetCurrentEvent(Event task)
    {
        task.InitEvent();

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
