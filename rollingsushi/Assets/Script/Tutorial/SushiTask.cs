using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiTask : ITutorialTask
{
    
    public string GetTitle()
    {
        return "寿司(1/2)";
    }


    public string GetText()
    {
        return "上手く食事席に配置できました\n"
               + "では,寿司を出し始めます.寿司は各ステージで決まった位置から出されます\n"
               +"寿司はレーンを流れていき,レーンの端まで行くと廃棄されてしまいます。廃棄数が指定の数に達してしまうとゲームオーバーになります";
    }


    public bool GetNextButton()
    {
        return true;
    }
    public bool GetBackButton()
    {
        return　true;
    }


    public void OnTaskSetting()
    {
        GameObject sushigenerator = GameObject.Find("SushiGenerator");
        sushigenerator.GetComponent<sushiGenerator>().enabled = true;

        GameObject animecon = GameObject.Find("GameManager");
        animecon.GetComponent<AnimationControler>().enabled = false;

        GameObject parent = GameObject.Find("Frame");
        parent.transform.GetChild(6).gameObject.SetActive(true);
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
