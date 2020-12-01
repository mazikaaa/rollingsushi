using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiParty : Event
{
    GameObject[] sushigenerators;
    float vx, vy;

    public override void InitEvent()
    {
        sushigenerators = GameObject.FindGameObjectsWithTag("sushigenerator");

        foreach (GameObject sushigenerator in sushigenerators)
        {
            sushigenerator.GetComponent<sushiGenerator>().sushigeneratetime -= 0.5f;

            vx = sushigenerator.GetComponent<sushiGenerator>().speed_x;
            vy = sushigenerator.GetComponent<sushiGenerator>().speed_y;

            sushigenerator.GetComponent<sushiGenerator>().speed_x = vx + 0.7f;
            sushigenerator.GetComponent<sushiGenerator>().speed_y = vy + 0.7f;
        }

        GameObject[] sushis = GameObject.FindGameObjectsWithTag("sushi");
        foreach (GameObject sushi in sushis)
        {
            sushi.GetComponent<sushi>().SpeedUpdate(0.7f, 0.7f);
        }

    }

    public override void ExitEvent()
    {
        foreach (GameObject sushigenerator in sushigenerators)
        {
            sushigenerator.GetComponent<sushiGenerator>().sushigeneratetime += 0.5f;

            sushigenerator.GetComponent<sushiGenerator>().speed_x = vx;
            sushigenerator.GetComponent<sushiGenerator>().speed_y = vy;
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
