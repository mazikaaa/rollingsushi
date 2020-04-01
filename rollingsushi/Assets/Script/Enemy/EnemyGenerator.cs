using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : Base_EnemyGenerate
{
    public float cheapspan;
    public float normalspan;
    public float expensivespan;

    private float[] sushitime = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sushitime[0] += Time.deltaTime;

        if(sushitime[0]>cheapspan)
        {
            GenerateCheapSushi();
            sushitime[0] = 0.0f;
        }
    }
}
