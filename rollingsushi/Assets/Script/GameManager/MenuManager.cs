using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : GameSystemBase
{
    [SerializeField] bool menuflag = false;
    public GameObject menu;
    GameObject gamemanager;

    // Start is called before the first frame update
    new void Start()
    {
        gamemanager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Pause))
        {
            setMenu();
        }
    }
    public void setMenu()
    {
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
        menu.SetActive(false);
        menuflag = false;

        gamemanager.SetActive(true);
        AllObjectTrue();
    }

    public void GoSelectButton()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void GameEndButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}