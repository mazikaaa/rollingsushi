﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : GameSystemBase
{
    [SerializeField] int gameover_disposal=2;//ゲームオーバーになる廃棄数
    [SerializeField] int gameclear_profit=500;//ゲームクリアになる売り上げ

    private float MainTime;
    private bool timeflag=true,eventflag=false;
    private GameObject menumanager;

  

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        menumanager = GameObject.Find("MenuManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Disposal >= gameover_disposal)
        {
            GameOver();
        }

        if (Profit > gameclear_profit)
        {
            GameClear();
        }
        if (timeflag)
        {
            MainTime += Time.deltaTime;
        }
       // time_text.GetComponent<Text>().text = MainTime.ToString();
    }
    private void GameClear()
    {
        Gameclear.gameObject.SetActive(true);
        menumanager.SetActive(false);
        MainTime = 0;
        timeflag = false;
        AllObjectFalse();
    }

    private void GameOver()
    {
        Gameover.gameObject.SetActive(true);
        MainTime = 0;
        timeflag = false;
        menumanager.SetActive(false);
        AllObjectFalse();
    }

    public void RefreshUnitButton() { 
     
        GameObject[] drags = GameObject.FindGameObjectsWithTag("drag");

        foreach(GameObject drag in drags)
        {
            drag.GetComponentInChildren<Drag>().DeleteUnit(-5.0f);
        }

        LowerRep();
    }

    public void ReplayButtton(int i)
    {
        SceneManager.LoadScene("GameScene"+i);
    }

    public void  StageSelectButton()
    {
        SceneManager.LoadScene("SelectScene");
    }


}


