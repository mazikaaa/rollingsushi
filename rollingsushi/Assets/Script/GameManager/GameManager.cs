using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : GameSystemBase
{
    [SerializeField] int gameover_disposal;//ゲームオーバーになる廃棄数
    [SerializeField] int gameclear_profit;//ゲームクリアになる売り上げ

    // Start is called before the first frame update
    void Start()
    {
        rep_text.GetComponent<Text>().text = Rep.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Disposal > gameover_disposal)
        {
            GameOver();
        }

        if (Profit > gameclear_profit)
        {
            GameClear();
        }
    }


    private void GameClear()
    {
        Gameclear.gameObject.SetActive(true);

        GameObject dragingobject = GameObject.FindGameObjectWithTag("dragingobject");
        if (dragingobject)
        {
            dragingobject.SetActive(false);
        }

        GameObject generator = GameObject.Find("SushiGenerator");
        generator.SetActive(false);

        foreach (GameObject sushi in GameObject.FindGameObjectsWithTag("sushi"))
        {
            sushi.GetComponent<sushi>().enabled = false;
        }

        foreach (GameObject drag in GameObject.FindGameObjectsWithTag("drag"))
        {
            drag.transform.GetChild(0).GetComponent<Drag>().enabled = false;
        }

        foreach (GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
        {
            drop.GetComponent<UnitManagr>().enabled = false;
        }


    }

    private void GameOver()
    {
        Gameover.gameObject.SetActive(true);

        GameObject generator = GameObject.Find("SushiGenerator");
        generator.SetActive(false);

        GameObject dragingobject = GameObject.FindGameObjectWithTag("dragingobject");
        if (dragingobject)
        {
            dragingobject.SetActive(false);
        }

        foreach (GameObject sushi in GameObject.FindGameObjectsWithTag("sushi"))
        {
            sushi.GetComponent<sushi>().enabled = false;
        }

        foreach (GameObject drag in GameObject.FindGameObjectsWithTag("drag"))
        {
            drag.transform.GetChild(0).GetComponent<Drag>().enabled = false;
        }

        foreach (GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
        {
            drop.GetComponent<UnitManagr>().enabled = false;
        }

    }
}


