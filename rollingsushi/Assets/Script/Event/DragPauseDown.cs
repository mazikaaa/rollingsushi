using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPauseDown : Event
{
    GameObject[] drags;
    public override void InitEvent()
    {
        drags = GameObject.FindGameObjectsWithTag("drag");
        foreach (GameObject drag in drags)
        {
            drag.transform.GetComponentInChildren<Drag>().eventplustime = 3.0f;
        }
    }

    public override void ExitEvent()
    {
        drags = GameObject.FindGameObjectsWithTag("drag");
        foreach (GameObject drag in drags)
        {
            drag.transform.GetComponentInChildren<Drag>().eventplustime = 0.0f;
        }
    }

    public override string GetTitle()
    {
        return "客足の減少";
    }

    public override string GetText()
    {
        return "一定時間，待ち席に客がやってくるまでの時間が\n" +
            "長くなってしまいます. ";
    }
}
