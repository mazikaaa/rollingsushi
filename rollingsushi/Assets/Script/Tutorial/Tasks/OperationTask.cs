using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationTask : ITutorialTask
{
    Animator frame_animator;
    GameObject animecon,Drag,Drop,frame;
    public bool checkflag = false;
    public string GetTitle()
    {
        return "待ち席から食事席への配置";
    }


    public string GetText()
    {
        return "それでは待ち席から食事席に配置してみましょう。\n"
               + "待ち席の客を食事席にドラック＆ドロップしてみて下さい。\n"
               +"※一人席に二人以上を座らせることは出来ないので注意してください。";
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
        animecon = GameObject.Find("GameManager");
        animecon.GetComponent<AnimationControler>().enabled = true;
        Drag = GameObject.Find("Drag1");

        frame = GameObject.Find("Frame");
        frame.transform.GetChild(1).gameObject.SetActive(false);
        frame.transform.GetChild(2).gameObject.SetActive(false);

        Drop = GameObject.Find("Drop1");
        checkflag = false;

        
    }

    public bool CheckTask()
    {
        if (Drop.GetComponent<Drop>().setUnit == true && !checkflag)
        {
            checkflag = true;
            return true;
        }
        else
            return false;
    }

    public void NextTask()
    {
        animecon.GetComponent<AnimationControler>().enabled = false;

        Animator frame_animator = frame.GetComponent<Animator>();
        frame_animator.SetBool("DD1", false);
        frame_animator.SetBool("DD2", false);
    }


    public float GetTransitionTime()
    {
        return 0.0f;
    }
}
