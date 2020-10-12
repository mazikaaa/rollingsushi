using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTask : ITutorialTask
{
    public string GetTitle()
    {
        return "イベント";
    }


    public string GetText()
    {
        return "一定の確率でイベントが発生します\n"
               + "イベントはステージ毎に異なり,様々な効果が発動します\n"
               + "一定時間が経つと,イベントの効果は消えます\n"
               + "※このステージではイベントは発生しません";
    }


    public bool GetNextButton()
    {
        return true;
    }
    public bool GetBackButton()
    {
        return true;
    }


    public void OnTaskSetting()
    {
        GameObject parent = GameObject.Find("Frame");
        parent.transform.GetChild(8).gameObject.SetActive(false);
        parent.transform.GetChild(9).gameObject.SetActive(true);
        parent.transform.GetChild(10).gameObject.SetActive(false);
    }

    public bool CheckTask()
    {
        return false;
    }

    public void NextTask()
    {

    }


    public float GetTransitionTime()
    {
        return 0.0f;
    }
}
