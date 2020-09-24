using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTask : ITutorialTask
{

    public string GetTitle()
    {
        return "はじめに";
    }


    public string GetText()
    {
        return "あなたは回転寿司屋の戦略を担うことになりました.苦難を乗り越えて売上を伸ばしましょう\n"+
                "まずは開店前に操作を覚えて回転寿司屋を成功させましょう!";
    }


    public bool GetNextButton()
    {
        return true;
    }
    public bool GetBackButton()
    {
        return false;
    }


    public void OnTaskSetting()
    {
        GameObject parent = GameObject.Find("Frame");
        parent.transform.GetChild(0).gameObject.SetActive(false);
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
