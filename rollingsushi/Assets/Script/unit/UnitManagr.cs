using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManagr : unitBase
{
 
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        if (setUnit == true)
        {
            leave += Time.deltaTime;
            clockhand.localRotation= Quaternion.Euler(0,0,(leave/leavetime)*-360);
        }

        if (amount > eatamount)
        {
            Leave();
        }

        if (leave > leavetime)
        {
            Leave();
        }
    }
}
