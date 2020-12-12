using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiTask2 :ITutorialTask
{
    public string GetTitle()
    {
        return "寿司(2/2)";
    }


    public string GetText()
    {
        return "食事席にいる客の前に寿司が来ると、客は一定の確率で寿司を食べます。"
               + "寿司を食べると、売上が上がります。\n"
               +"食事席の客は満腹になるか、時間が経つと食事席を離れます。\n"
               + "多くのステージでは売上を一定以上稼ぐことが出来ればゲームクリアになります。";
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
        parent.transform.GetChild(6).gameObject.SetActive(false);
        parent.transform.GetChild(7).gameObject.SetActive(true);
        parent.transform.GetChild(8).gameObject.SetActive(false);
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
