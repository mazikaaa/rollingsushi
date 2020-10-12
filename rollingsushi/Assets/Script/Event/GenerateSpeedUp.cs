using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSpeedUp : Event
{
    GameObject sushigenerator;
    float vx, vy;

    public override void InitEvent()
    {
        sushigenerator = GameObject.Find("SushiGenerator");
        sushigenerator.GetComponent<sushiGenerator>().sushigeneratetime-=0.8f;

    }

    public override void ExitEvent()
    {
        sushigenerator.GetComponent<sushiGenerator>().sushigeneratetime += 0.8f;
    }

    public override string GetTitle()
    {
        return "寿司大量生成";
    }

    public override string GetText()
    {
        return "寿司が出てくるテンポが上がります\n";
    }
}
