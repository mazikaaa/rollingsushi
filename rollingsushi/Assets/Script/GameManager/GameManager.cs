using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] int gameover_disposal=2;//ゲームオーバーになる廃棄数
    [SerializeField] int gameclear_profit=500;//ゲームクリアになる売り上げ

    //サウンド関係
    private float BGM_volume;
    private GameObject menumanager,audio_BGM;
    private AudioSource Audio_SE,Audio_BGM;
    public AudioClip GameOver_SE, GameClear_SE,drum_d,drum_dd;
    public AudioClip discard_SE;
    private float volume;


    private bool gameoverflag = false, gameclearflag = false;
    public GameObject Gameclear, Gameover;

    //UI関係のオブジェクト
    public GameObject profit_text, disposal_text;
    public GameObject[] star = new GameObject[7];


    public bool expensiveflag = false;//ネタの高騰(イベント)による変化を起こすフラグ
    public bool saleflag = false;//ネタの高騰(イベント)による変化を起こすフラグ

    //(廃棄数)一定以上貯まるとゲームオーバー
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
    private int disposal = 0;

    //(利益)一定以上貯まるとゲームクリアになる
    public int Profit
    {
        set
        {
            profit += value;
        }

        get
        {
            return profit;
        }

    }
    private int profit = 0;

    //(評価)ユニットの生成時間を上下させる変数。
    public int Rep
    {
        set
        {
            rep = value;
        }

        get
        {
            return rep;
        }

    }
    private int rep = 4;

    //音楽関

    // Start is called before the first frame update
    void Start()
    {
       menumanager = GameObject.Find("MenuManager");

        //効果音の初期化
        Audio_SE = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("SE", 1.0f);
        Audio_SE.volume *= volume;

        //BGMの初期化
        audio_BGM = GameObject.Find("GameMusic");
        Audio_BGM = audio_BGM.GetComponent<AudioSource>();
        BGM_volume = PlayerPrefs.GetFloat("BGM", 1.0f);
        Audio_BGM.volume *= BGM_volume;
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
        Audio_BGM.PlayOneShot(drum_d);
        SceneManager.LoadScene("GameScene"+i);
    }

    //ステージセレクト画面に戻る
    public void  StageSelectButton()
    {
        Audio_BGM.PlayOneShot(drum_dd);
        SceneManager.LoadScene("SelectScene");
    }

    //ゲームクリア処理
    private void GameClear()
    {
        Gameclear.gameObject.SetActive(true);
        menumanager.SetActive(false);
        Audio_BGM.Stop();
        Audio_SE.PlayOneShot(GameClear_SE);
        AllObjectFalse();
    }

    //ゲームオーバー処理
    private void GameOver()
    {
        Gameover.gameObject.SetActive(true);
        menumanager.SetActive(false);
        Audio_BGM.Stop();
        Audio_SE.PlayOneShot(GameOver_SE);
        AllObjectFalse();
    }

    //寿司を廃棄する
    public void Discard()
    {
        Disposal = 1;
        Audio_SE.PlayOneShot(discard_SE);
        disposal_text.GetComponent<Text>().text = Disposal.ToString();
    }

    //利益を増やす
    public void GainProfit(int price)
    {
        //イベント発生中は得られる利益が20%減る
        if (expensiveflag)
        {
            Profit = (int)(price * 0.8f);
        }
        else if (saleflag)
        {
            Profit = (int)(price * 0.7f);
        }
        else
        {
            Profit = price;
        }
        profit_text.GetComponent<Text>().text = Profit.ToString();
    }

    //評価を上げる
    public void RaiseRep()
    {
        Rep += 1;
        Rep = Mathf.Clamp(Rep, 1, 7);
        //  Debug.Log("評判が上がりました");
        star[Rep - 1].SetActive(true);
    }

    //評価を下げる
    public void LowerRep()
    {
        Rep -= 1;
        Rep = Mathf.Clamp(Rep, 1, 7);
        //Debug.Log("評判が下がりました");
        star[Rep].SetActive(false);
    }

    //ゲーム内のオブジェクトを一括で止める
    public void AllObjectFalse()
    {
        GameObject sushigenerator = GameObject.Find("SushiGenerator");
        sushigenerator.GetComponent<sushiGenerator>().enabled = false;

        GameObject eventmanager = GameObject.Find("EventManager");
        eventmanager.GetComponent<EventManager>().enabled = false;

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
            drag.transform.GetChild(1).GetComponent<Drag>().enabled = false;
        }

        foreach (GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
        {
            drop.GetComponent<UnitManager>().enabled = false;
        }
    }

    //停止させたオブジェクトを一括で動かす
    public void AllObjectTrue()
    {
        GameObject sushigenerator = GameObject.Find("SushiGenerator");
        sushigenerator.GetComponent<sushiGenerator>().enabled = true;

        GameObject eventmanager = GameObject.Find("EventManager");
        eventmanager.GetComponent<EventManager>().enabled = false;

        GameObject dragingobject = GameObject.FindGameObjectWithTag("dragingobject");
        if (dragingobject)
        {
            dragingobject.SetActive(true);
        }

        foreach (GameObject sushi in GameObject.FindGameObjectsWithTag("sushi"))
        {
            sushi.GetComponent<sushi>().enabled = true;
        }

        foreach (GameObject drag in GameObject.FindGameObjectsWithTag("drag"))
        {
            drag.transform.GetChild(1).GetComponent<Drag>().enabled = true;
        }

        foreach (GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
        {
            drop.GetComponent<UnitManager>().enabled = true;
        }
    }

}


