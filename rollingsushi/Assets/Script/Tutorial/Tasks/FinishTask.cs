using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTask : ITutorialTask
{
    public string GetTitle()
    {
        return "さいごに";
    }


    public string GetText()
    {
        return "これで準備は終了です。\n"
               + "ステージを進めると出てくる寿司の種類やイベントが増えていきます。\n"
               + "戦略を生かして、上手く回転寿司屋を成功させましょう！";
    }


    public bool GetNextButton()
    {
        return false;
    }
    public bool GetBackButton()
    {
        return false;
    }


    public void OnTaskSetting()
    {
        GameObject parent = GameObject.Find("Frame");
        parent.transform.GetChild(9).gameObject.SetActive(false);

        GameObject textwindow = GameObject.Find("TextBoard");
        GameObject go = textwindow.transform.Find("Go").gameObject;
        go.gameObject.SetActive(true);
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
