using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTask :ITutorialTask
{
    public string GetTitle()
    {
        return "食事席";
    }


    public string GetText()
    {
        return "青い枠で囲まれているのが食事席です。\n"
               + "待ち席のお客を食事席に配置することが出来ます。\n"
               + "食事席の大きさによって、配置できるお客が変わります。\n"
               + "テーブル席には4人以下、ペア席には2人以下、一人席は1人の客を座わらせることが出来ます。\n";
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

        GameObject menu = GameObject.Find("MenuManager");
        menu.GetComponent<Menu_Tutorial>().dropflag = true;

        GameObject parent = GameObject.Find("Frame");
        parent.transform.GetChild(1).gameObject.SetActive(true);
        parent.transform.GetChild(2).gameObject.SetActive(true);
        parent.transform.GetChild(0).gameObject.SetActive(false);
    }

    public bool CheckTask()
    {
        return false;
    }

    public void NextTask()
    {
        GameObject parent = GameObject.Find("Frame");
        parent.transform.GetChild(1).gameObject.SetActive(false);
        parent.transform.GetChild(2).gameObject.SetActive(false);
    }


    public float GetTransitionTime()
    {
        return 0.0f;
    }
}
