using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiSpeedUp : Event
{
    GameObject[] sushigenerators;
    float vx,vy;

   public override void InitEvent()
    {
        sushigenerators = GameObject.FindGameObjectsWithTag("sushigenerator");

        foreach (GameObject sushigenerator in sushigenerators)
        {
            vx = sushigenerator.GetComponent<sushiGenerator>().speed_x;
            vy = sushigenerator.GetComponent<sushiGenerator>().speed_y;

            sushigenerator.GetComponent<sushiGenerator>().speed_x = vx + 1.0f;
            sushigenerator.GetComponent<sushiGenerator>().speed_y = vy + 1.0f;
        }

        GameObject[] sushis = GameObject.FindGameObjectsWithTag("sushi");
        foreach(GameObject sushi in sushis)
        {
            sushi.GetComponent<sushi>().SpeedUpdate(1.0f, 1.0f);
        }
    }

    public override void ExitEvent()
    {
        foreach (GameObject sushigenerator in sushigenerators)
        {
            sushigenerator.GetComponent<sushiGenerator>().speed_x = vx;
            sushigenerator.GetComponent<sushiGenerator>().speed_y = vy;
        }

        GameObject[] sushis = GameObject.FindGameObjectsWithTag("sushi");
        foreach (GameObject sushi in sushis)
        {
            sushi.GetComponent<sushi>().SpeedUpdate(-1.0f, -1.0f);
        }
    }

    public override string GetTitle()
    {
        return "高速レーン";
    }

    public override string GetText()
    {
        return "回転レーンの速度が上がり,\n" +
               "寿司が流れてくるスピードが速くなります\n";
    }
}
