using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSpeedUp : Event
{
    GameObject[] sushigenerators;

    public override void InitEvent()
    {
        sushigenerators = GameObject.FindGameObjectsWithTag("sushigenerator");

        foreach (GameObject sushigenerator in sushigenerators)
        {
            sushigenerator.GetComponent<sushiGenerator>().sushigeneratetime -= 0.8f;
        }

    }

    public override void ExitEvent()
    {
        foreach (GameObject sushigenerator in sushigenerators)
        {
            sushigenerator.GetComponent<sushiGenerator>().sushigeneratetime += 0.8f;
        }
    }

    public override string GetTitle()
    {
        return "たくさんご提供";
    }

    public override string GetText()
    {
        return "寿司が出てくるテンポが上がります\n";
    }
}
