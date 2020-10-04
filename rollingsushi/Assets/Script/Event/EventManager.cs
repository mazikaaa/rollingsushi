using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
   [SerializeField] int evnetNo=0;
   // public Event[] events=new Event[5];//発生するイベント格納する


    private Event nowEvent=null;
    private List<Event> eventList;

    private float eventTime;
    private bool  eventflag = false;

    //イベント用のUI
    public Text event_text;


    // Start is called before the first frame update
    void Start()
    {

        eventList = new List<Event>()
        {
            new NoneEvent(),
            new SushiSpeedUp()
        };

        nowEvent = null;
    }

    // Update is called once per frame
    void Update()
    {
        eventTime += Time.deltaTime;

        if (eventTime > 10.0f)
        {
            if(nowEvent!=null)
            nowEvent.ExitEvent();//前のイベントの効果を消す

            nowEvent = eventList[1];//新しいイベントをセット
            SetCurrentEvent(nowEvent);//新しいイベントを発生させる
            eventTime = 0.0f;
        }
    }

    protected void SetCurrentEvent(Event task)
    {
        task.InitEvent();

        event_text.text = task.GetText();
    }

}
