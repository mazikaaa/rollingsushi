using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] bool menuflag = false;
    public GameObject menu,refresh;
    GameObject gamemanager,sushigenerator,eventmanager;

    public AudioClip drum_d,drum_dd;
    private AudioSource SE;

    private float volume;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager");
        sushigenerator = GameObject.Find("SushiGenerator");
        eventmanager = GameObject.Find("EventManager");
        SE = GetComponent<AudioSource>(); volume = PlayerPrefs.GetFloat("BGM", 1.0f);
        volume = PlayerPrefs.GetFloat("BGM", 1.0f);
        SE.volume *= volume;


    }

    // Update is called once per frame
    void Update()
    {
    }
    public void setMenu()
    {
        SE.PlayOneShot(drum_d);
        if (menuflag)
        {
            menu.SetActive(false);
            menuflag = false;

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

    public void ContinueButton()
    {
        SE.PlayOneShot(drum_d);
        menu.SetActive(false);
        menuflag = false;

        gamemanager.SetActive(true);
        AllObjectTrue();
    }

    public void ReplayTutorial()
    {
        SE.PlayOneShot(drum_d);
        SceneManager.LoadScene("TutorialScene");
    }

    public void GoSelectButton()
    {
        SE.PlayOneShot(drum_dd);
        SceneManager.LoadScene("SelectScene");
    }

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
            drag.transform.GetChild(0).GetComponent<Drag>().enabled = false;
        }

        foreach (GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
        {
            drop.GetComponent<UnitManagr>().enabled = false;
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
            drag.transform.GetChild(0).GetComponent<Drag>().enabled = true;
        }

        foreach (GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
        {
            drop.GetComponent<UnitManagr>().enabled = true;
        }

        refresh.SetActive(true);
    }
}