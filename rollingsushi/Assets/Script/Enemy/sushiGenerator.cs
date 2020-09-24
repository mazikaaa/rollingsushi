using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sushiGenerator : Base_sushiGenerate
{
  
    public float sushigeneratetime;

    private float sushitime=0.0f;
    private int sushikey;//出てくる寿司の種類を決めるキー

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sushitime += Time.deltaTime;

        if(sushitime>sushigeneratetime)
        {
            /*
            sushikey = ChooseGenerateSushi();

            switch (sushikey)
            {
                case 0:
                   // Debug.Log("cheap");
                    GenerateCheapSushi();
                    break;
                case 1:
                    //Debug.Log("normal");
                    GenerateNormalSushi();

                    break;
                case 2:
                   // Debug.Log("expensive");
                    break;
            }
            */

            ChooseSushi();

            sushitime = 0.0f;
        }
    }
}
