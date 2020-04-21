using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int Disposal
    {
        set
        {
            disposal += value;
        }
        get
        {
            return disposal;
        }
    }
    private int disposal=0;

    public int Profit {
        set
        {
            profit+= value;
            Debug.Log(profit);
        }

        get
        {
            return profit;
        }

    }
    private int profit=0;

    public GameObject profit_text;
    public GameObject disposal_text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainProfit(int price)
    {
        Profit = price;
        profit_text.GetComponent<Text>().text = Profit.ToString();
    }

    public void Discard()
    {
        Disposal = 1;
        disposal_text.GetComponent<Text>().text = Disposal.ToString();
    }
}
