using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTask : ITutorialTask
{
    public string GetTitle()
    {
        return "待ち席";
    }


    public string GetText()
    {
        return "赤い枠で囲まれているのが待ち席です\n"
               + "待ち席の客は食事席に配置されるか,一定時間が経つことでいなくなり,待ち席は空きの状態になります\n"
               + "空の待ち席には,一定時間が経つと新しい客がやって来ます";
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
        GameObject menu = GameObject.Find("MenuManager");

        parent.transform.GetChild(0).gameObject.SetActive(true);

        menu.GetComponent<Menu_Tutorial>().dragflag = true;

        GameObject[] drags = GameObject.FindGameObjectsWithTag("drag");
        foreach(GameObject drag in drags)
        {
            drag.transform.GetComponentInChildren<Drag>().enabled = true;
        }
    }

    public bool CheckTask()
    {
        return false;
    }

    public void NextTask()
    {
        GameObject parent = GameObject.Find("Frame");
        parent.transform.GetChild(0).gameObject.SetActive(false);
    }


    public float GetTransitionTime()
    {
        return 0.0f;
    }
}
