using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPauseDown : Event
{
    List<Drag> drags = new List<Drag>();

    public override void InitEvent()
    {
        foreach (GameObject drag in GameObject.FindGameObjectsWithTag("drag"))
        {
            drags.Add(drag.transform.GetComponentInChildren<Drag>());
        }
    }

    public override void ActionEvent()
    {
        foreach (Drag drag in drags)
        {
            drag.eventplustime = 3.0f;
        }
    }

    public override void ExitEvent()
    {
        foreach (Drag drag in drags)
        {
            drag.eventplustime = 0.0f;
        }
    }

    public override string GetTitle()
    {
        return "客足の減少";
    }

    public override string GetText()
    {
        return "一定時間，待ち席にお客がやってくるまでの時間が長くなってしまいます. ";
    }
}
