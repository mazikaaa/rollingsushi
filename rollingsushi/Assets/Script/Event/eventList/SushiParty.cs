using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiParty : Event
{
    List<sushiGenerator> sushigenerators=new List<sushiGenerator>();
    float vx, vy;

    public override void InitEvent()
    {

        foreach(GameObject sushigene in GameObject.FindGameObjectsWithTag("sushigenerator"))
        {
            sushigenerators.Add(sushigene.GetComponent<sushiGenerator>());
        }
    }

    public override void ActionEvent()
    {
        foreach (sushiGenerator sushigenerator in sushigenerators)
        {
            sushigenerator.sushigeneratetime -= 0.5f;

            vx = sushigenerator.speed_x;
            vy = sushigenerator.speed_y;

            sushigenerator.speed_x = vx + 0.7f;
            sushigenerator.speed_y = vy + 0.7f;
        }

        GameObject[] sushis = GameObject.FindGameObjectsWithTag("sushi");
        foreach (GameObject sushi in sushis)
        {
            sushi.GetComponent<sushi>().SpeedUpdate(0.7f, 0.7f);
        }

    }

    public override void ExitEvent()
    {
   
        foreach (sushiGenerator sushigenerator in sushigenerators)
        {
            sushigenerator.sushigeneratetime += 0.5f;
            sushigenerator.speed_x = vx;
            sushigenerator.speed_y = vy;
        }

        GameObject[] sushis = GameObject.FindGameObjectsWithTag("sushi");
        foreach (GameObject sushi in sushis)
        {
            sushi.GetComponent<sushi>().SpeedUpdate(-0.7f, -0.7f);
        }
    }

    public override string GetTitle()
    {
        return "すしざんまい";
    }

    public override string GetText()
    {
        return "寿司が出てくるテンポ、寿司が流れてくるが両方とも上がります\n";
    }
}
