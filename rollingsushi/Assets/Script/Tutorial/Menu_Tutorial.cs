using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Tutorial : MonoBehaviour
{
    [SerializeField] bool menuflag = false;
    public GameObject menu, refresh;
    GameObject gamemanager, sushigenerator, eventmanager;

    public AudioClip drum_d, drum_dd;
    public bool dropflag=false, dragflag=false, sushiflag=false;
    private AudioSource SE,BGM;
    public Slider Sli_BGM, Sli_SE;

    private GameObject audio_BGM;
    private Transform game_detail, unit_detail, setting;

    private float volume_SE, volume_BGM, SE_default, BGM_default;

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
        sushigenerator = GameObject.Find("SushiGenerator");
        eventmanager = GameObject.Find("EventManager");

        BGM = audio_BGM.GetComponent<AudioSource>();
        volume_BGM = PlayerPrefs.GetFloat("BGM", 1.0f);        

        SE = GetComponent<AudioSource>();
        volume_SE = PlayerPrefs.GetFloat("SE", 1.0f);
        SE.volume *= volume_SE;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
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
        sushigenerator.GetComponent<sushiGenerator>().enabled = false;
        eventmanager.GetComponent<EventManager>().enabled = false;

        GameObject dragingobject = GameObject.FindGameObjectWithTag("dragingobject");
        if (dragingobject)
        {
            dragingobject.SetActive(false);
        }

        if (sushiflag)
        {
            foreach (GameObject sushi in GameObject.FindGameObjectsWithTag("sushi"))
            {
                sushi.GetComponent<sushi>().enabled = false;
            }
        }

        if (dragflag)
        {
            foreach (GameObject drag in GameObject.FindGameObjectsWithTag("drag"))
            {
                drag.transform.GetChild(1).GetComponent<Drag>().enabled = false;
            }
        }

        if (dropflag)
        {
            foreach (GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
            {
                drop.GetComponent<UnitManager>().enabled = false;
            }
        }

        refresh.SetActive(false);
    }

    //停止させたオブジェクトを一括で動かす
    public void AllObjectTrue()
    {
        if (sushiflag)
        {
            sushigenerator.GetComponent<sushiGenerator>().enabled = true;
        }
        eventmanager.GetComponent<EventManager>().enabled = true;

        GameObject dragingobject = GameObject.FindGameObjectWithTag("dragingobject");
        if (dragingobject)
        {
            dragingobject.SetActive(true);
        }

        if (sushiflag)
        {
            foreach (GameObject sushi in GameObject.FindGameObjectsWithTag("sushi"))
            {
                sushi.GetComponent<sushi>().enabled = true;
            }
        }


        if (dragflag)
        {
            foreach (GameObject drag in GameObject.FindGameObjectsWithTag("drag"))
            {
                drag.transform.GetChild(0).GetComponent<Drag>().enabled = true;
            }
        }


        if (dropflag)
        {
            foreach (GameObject drop in GameObject.FindGameObjectsWithTag("drop"))
            {
                drop.GetComponent<UnitManager>().enabled = true;
            }
        }

        refresh.SetActive(true);
    }

    public void Detail_Game()
    {
        SE.PlayOneShot(drum_d);
        game_detail.gameObject.SetActive(true);
        unit_detail.gameObject.SetActive(false);
        setting.gameObject.SetActive(false);
    }

    public void Detail_Unit()
    {
        SE.PlayOneShot(drum_d);
        game_detail.gameObject.SetActive(false);
        unit_detail.gameObject.SetActive(true);
        setting.gameObject.SetActive(false);
    }

    public void Detail_Setting()
    {
        SE.PlayOneShot(drum_d);
        game_detail.gameObject.SetActive(false);
        unit_detail.gameObject.SetActive(false);
        setting.gameObject.SetActive(true);
    }

    public void BGM_Setting()
    {
        float volume;

        volume = Sli_BGM.value;
        BGM.volume = BGM_default * volume;

        PlayerPrefs.SetFloat("BGM", volume);
    }

    public void SE_Setting()
    {
        float volume;

        volume = Sli_SE.value;
        SE.volume = SE_default * volume;

        PlayerPrefs.SetFloat("SE", volume);
    }

}
