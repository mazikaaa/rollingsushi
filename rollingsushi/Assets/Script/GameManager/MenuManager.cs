﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] bool menuflag = false;
    public GameObject menu,refresh;
    GameObject gamemanager,eventmanager;

    public AudioClip drum_d,drum_dd;
    public Slider Sli_BGM, Sli_SE;

    private AudioSource SE,BGM;
    private GameObject audio_BGM;
    private Transform game_detail,unit_detail,setting;

    private float volume_SE,volume_BGM,SE_default,BGM_default;

    private void Awake()
    {
        audio_BGM = GameObject.Find("GameMusic");
        BGM = audio_BGM.GetComponent<AudioSource>();
        SE = GetComponent<AudioSource>();

        BGM_default = BGM.volume;
        SE_default = SE.volume;

        Sli_BGM.value = PlayerPrefs.GetFloat("BGM", 1.0f);
        Sli_SE.value = PlayerPrefs.GetFloat("SE", 1.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager");
        eventmanager = GameObject.Find("EventManager");

        //サウンドの初期化
        BGM = audio_BGM.GetComponent<AudioSource>();
        volume_BGM = PlayerPrefs.GetFloat("BGM", 1.0f);
        BGM.volume *= volume_BGM;

        SE = GetComponent<AudioSource>();
        volume_SE = PlayerPrefs.GetFloat("SE", 1.0f);
        SE.volume *= volume_SE;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //メニュー画面を開く
    public void setMenu()
    {
        game_detail = menu.transform.Find("Game");
        unit_detail = menu.transform.Find("Unit");
        setting = menu.transform.Find("Setting");

        SE.PlayOneShot(drum_d);
        if (menuflag)
        {
            menu.SetActive(false);
            menuflag = false;

            game_detail.gameObject.SetActive(true);
            unit_detail.gameObject.SetActive(false);
            setting.gameObject.SetActive(false);

            gamemanager.GetComponent<GameManager>().enabled = true;
            AllObjectTrue();
        }
        else
        {
            menu.SetActive(true);
            menuflag = true;

            gamemanager.GetComponent<GameManager>().enabled = false;
            AllObjectFalse();
        }
    }

    //もう一度続きから始める
    public void ContinueButton()
    {
        SE.PlayOneShot(drum_d);
        setMenu();

        gamemanager.SetActive(true);
        AllObjectTrue();
    }
    //チュートリアルを最初からやり直す
    public void ReplayTutorial()
    {
        SE.PlayOneShot(drum_d);
        SceneManager.LoadScene("TutorialScene");
    }

    //ステージ選択画面に戻る
    public void GoSelectButton()
    {
        SE.PlayOneShot(drum_dd);
        SceneManager.LoadScene("SelectScene");
    }

    //ステージを最初からやり直す
    public void ReplayGame(int i)
    {
        SE.PlayOneShot(drum_d);
        SceneManager.LoadScene("GameScene" + i);
    }

    public void GameEndButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        PlayerPrefs.SetInt("MUSIC", 0);
#elif UNITY_STANDALONE
        PlayerPrefs.SetInt("MUSIC", 0);
        UnityEngine.Application.Quit();
#endif
    }

    //ステージ情報画面を表示する
    public void Detail_Game()
    {
        SE.PlayOneShot(drum_d);
        game_detail.gameObject.SetActive(true);
        unit_detail.gameObject.SetActive(false);
        setting.gameObject.SetActive(false);
    }

    //ユニット情報画面を表示する
    public void Detail_Unit()
    {
        SE.PlayOneShot(drum_d);
        game_detail.gameObject.SetActive(false);
        unit_detail.gameObject.SetActive(true);
        setting.gameObject.SetActive(false);
    }

    //音量調整画面の表示
    public void Detail_Setting()
    {
        SE.PlayOneShot(drum_d);
        game_detail.gameObject.SetActive(false);
        unit_detail.gameObject.SetActive(false);
        setting.gameObject.SetActive(true);
    }

    //BGMの音量調整
    public void BGM_Setting()
    {
        float volume;

        volume = Sli_BGM.value;
        BGM.volume = BGM_default * volume;

        PlayerPrefs.SetFloat("BGM", volume);
    }

    //効果音の音量調整
    public void SE_Setting()
    {
        float volume;

        volume = Sli_SE.value;
        SE.volume = SE_default * volume;

        PlayerPrefs.SetFloat("SE", volume);
    }

    //ゲーム内のオブジェクトを一括で止める
    public void AllObjectFalse()
    {
        eventmanager.GetComponent<EventManager>().enabled = false;

        foreach (GameObject sushigene in GameObject.FindGameObjectsWithTag("sushigenerator"))
        {
            sushigene.GetComponent<sushiGenerator>().enabled = false;
        }

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

        refresh.SetActive(false);
    }

    //停止させたオブジェクトを一括で動かす
    public void AllObjectTrue()
    {
        eventmanager.GetComponent<EventManager>().enabled = true;

        foreach(GameObject sushigene in GameObject.FindGameObjectsWithTag("sushigenerator"))
        {
            sushigene.GetComponent<sushiGenerator>().enabled = true;
        }

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

        refresh.SetActive(true);
    }
}