using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManagr : unitBase
{
    GameManager gameManager;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        gameManager = gamemanager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (setUnit == true)
        {
            leave += Time.deltaTime;
            eattime += Time.deltaTime;
            clockhand.localRotation= Quaternion.Euler(0,0,(leave/leavetime)*-360);
        }

        //満腹なら
        if (amount > eatamount)
        {
            //評判が上がる
            gameManager.RaiseRep();
            Leave();
        }

        //着席時間を超えたら
        if (leave > leavetime)
        {
            if ((float)amount / (float)eatamount <= 0.50f)
            {
                //評判下がる
                gameManager.LowerRep();
            }
            Leave();
        }
    }
}
