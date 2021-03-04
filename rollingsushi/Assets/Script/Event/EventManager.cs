using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
   [SerializeField] int eventNo=0;
   // public Event[] events=new Event[5];//発生するイベント格納する
    
    private Event nowEvent;
    [SerializeField] List<Event> eventList=new List<Event>();

    private float volume;
  //  private bool  eventflag = false;

    Animator kakeziku_anime;
    new AudioSource audio;

    public GameObject eventBox;
    public float eventspan,eventTime;

    //イベント用のUI
    public Text event_text, event_title, event_title2;
    public AudioClip kakeziku_SE;


    // Start is called before the first frame update
    void Start()
    {
        GameObject kakeziku = GameObject.Find("kakeziku");
        kakeziku_anime = kakeziku.GetComponent<Animator>();

        audio = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("SE", 1.0f);
        audio.volume *= volume;

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
        else
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
