using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepoTask : ITutorialTask
{
    public string GetTitle()
    {
        return "評判";
    }


    public string GetText()
    {
        return "評判の高さによって、空の待ち席に新しい客が来るまでの時間が変わります。\n"
               + "評判は客を満腹にすることや、お客の能力で上がります。\n"
               + "逆にお客の入れ替えを行うことや、食べた量が半分以下の状態で離席してしまうと評判は下がってしまいます。\n";
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
        parent.transform.GetChild(7).gameObject.SetActive(false);
        parent.transform.GetChild(8).gameObject.SetActive(true);
        parent.transform.GetChild(9).gameObject.SetActive(false);
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
