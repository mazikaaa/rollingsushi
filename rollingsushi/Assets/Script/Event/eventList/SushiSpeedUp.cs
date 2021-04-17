using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiSpeedUp : Event
{
    //流れてくる寿司のスピードが上がるイベント

    List<sushiGenerator> sushigenerators = new List<sushiGenerator>();
    float vx,vy;

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
            vx = sushigenerator.speed_x;
            vy = sushigenerator.speed_y;

            sushigenerator.speed_x = vx + 1.0f;
            sushigenerator.speed_y = vy + 1.0f;
        }

        GameObject[] sushis = GameObject.FindGameObjectsWithTag("sushi");
        foreach(GameObject sushi in sushis)
        {
            sushi.GetComponent<sushi>().SpeedUpdate(1.0f, 1.0f);
        }
    }

    public override void ExitEvent()
    {
        foreach (sushiGenerator sushigenerator in sushigenerators)
        {
            sushigenerator.speed_x = vx;
            sushigenerator.speed_y = vy;
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
