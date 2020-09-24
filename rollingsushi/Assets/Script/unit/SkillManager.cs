using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool SkillCheck(int No,int price)
    {
        switch (No)
        {
            case 0:
                return true;
            case 1:
                return NotEatExpensive(price);
            default:
                return true;
        }
    }

    public bool NotEatExpensive(int price)
    {
        if (price >= 200)
        {
            return false;
        }
        return true;
    }
}
