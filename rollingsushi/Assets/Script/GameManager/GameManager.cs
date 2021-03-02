using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : GameSystemBase
{
    [SerializeField] int gameover_disposal=2;//ゲームオーバーになる廃棄数
    [SerializeField] int gameclear_profit=500;//ゲームクリアになる売り上げ

    private float BGM_volume;
    private bool gameoverflag=false,gameclearflag=false;
    private GameObject menumanager,audio_BGM;
    private AudioSource audiosource,BGM;

    public AudioClip GameOver_SE, GameClear_SE,drum_d,drum_dd;

    // Start is called before the first frame update
     new void Start()
    {
        base.Start();
       menumanager = GameObject.Find("MenuManager");

        audio_BGM = GameObject.Find("GameMusic");
        BGM = audio_BGM.GetComponent<AudioSource>();
        BGM_volume = PlayerPrefs.GetFloat("BGM", 1.0f);
        BGM.volume *= BGM_volume;
    }

    // Update is called once per frame
    void Update()
    {
         //廃棄数が上限を超えたらゲームオーバー
        if (Disposal >= gameover_disposal&&!gameoverflag)
        {
            GameOver();
            gameoverflag = true;
        }
        
        //利益が上限を超えたらゲームクリア
        if (Profit > gameclear_profit && !gameclearflag)
        {
            GameClear();
            gameclearflag = true;
        }
    }


    //待機席にいるお客さんを入れ替える
    public void RefreshUnitButton() { 
     
        GameObject[] drags = GameObject.FindGameObjectsWithTag("drag");

        foreach(GameObject drag in drags)
        {
            //次のお客さんが来るまでの時間を少し短くする
            drag.GetComponentInChildren<Drag>().DeleteUnit(-5.0f);
        }
        LowerRep();
    }

    //ステージを最初からやり直す
    public void ReplayButtton(int i)
    {
        BGM.PlayOneShot(drum_d);
        SceneManager.LoadScene("GameScene"+i);
    }

    //ステージセレクト画面に戻る
    public void  StageSelectButton()
    {
        BGM.PlayOneShot(drum_dd);
        SceneManager.LoadScene("SelectScene");
    }

    //ゲームクリア処理
    private void GameClear()
    {
        Gameclear.gameObject.SetActive(true);
        menumanager.SetActive(false);
        BGM.Stop();
        Audio.PlayOneShot(GameClear_SE);
        AllObjectFalse();
    }

    //ゲームオーバー処理
    private void GameOver()
    {
        Gameover.gameObject.SetActive(true);
        menumanager.SetActive(false);
        BGM.Stop();
        Audio.PlayOneShot(GameOver_SE);
        AllObjectFalse();
    }

}


