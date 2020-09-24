using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleTask : ITutorialTask
{
    public string GetTitle()
    {
        return "入れ替え";
    }


    public string GetText()
    {
        return "待ち席に来る客は編成した8人の中から,ランダムで決まります\n"
               + "ですので,状況に合わないお客が来る可能性があります\n"
               + "入替のボタンを押せば,待ち席の客をリセットできます\n"
               +"ただし,評判が下がってしまうので注意して下さい\n";
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
