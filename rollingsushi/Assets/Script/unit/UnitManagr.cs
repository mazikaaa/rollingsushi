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
        if (amount > eatamount)
        {
            Leave();
        }
    }
}
