using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SushiLibraryManager : MonoBehaviour
{
    public GameObject backbutton, nextbutton;
    public AudioClip drum_d, drum_dd;
    public GameObject[] page = new GameObject[5];

    private int pageNo = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdvancePage()
    {
        page[pageNo].SetActive(false);
        pageNo++;
        page[pageNo].SetActive(true);

        backbutton.SetActive(true);
        if (pageNo == 4)
           nextbutton.SetActive(false);
    }
    public void RetrunPage()
    {
        page[pageNo].SetActive(false);
        pageNo--;
        page[pageNo].SetActive(true);

        nextbutton.SetActive(true);
        if (pageNo == 0)
            backbutton.SetActive(false);
    }

    public void BackSelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("MUSIC", 0);
    }
}
