using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSpeedUp : Event
{
    List<sushiGenerator> sushigenerators = new List<sushiGenerator>();
    int i;

    public override void InitEvent()
    {
        foreach (GameObject sushigene in GameObject.FindGameObjectsWithTag("sushigenerator"))
        {
            sushigenerators.Add(sushigene.GetComponent<sushiGenerator>());
        }
    }
    public override void ActionEvent()
    {
        foreach (sushiGenerator sushigenerator in sushigenerators)
        {
            sushigenerator.sushigeneratetime -= 0.8f;
        }
    }

    public override void ExitEvent()
    {
        foreach (sushiGenerator sushigenerator in sushigenerators)
        {
            sushigenerator.sushigeneratetime += 0.8f;
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
