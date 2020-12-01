using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SushiLibraryManager : MonoBehaviour
{
    public GameObject backbutton, nextbutton;
    public AudioClip drum_d, drum_dd;
    public GameObject[] page = new GameObject[5];
    public AudioSource audio;

    private int pageNo = 0;
    private float volume;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("SE", 1.0f);
        audio.volume *= volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdvancePage()
    {
        audio.PlayOneShot(drum_d);
        page[pageNo].SetActive(false);
        pageNo++;
        page[pageNo].SetActive(true);

        backbutton.SetActive(true);
        if (pageNo == 4)
           nextbutton.SetActive(false);
    }
    public void RetrunPage()
    {
        audio.PlayOneShot(drum_d);
        page[pageNo].SetActive(false);
        pageNo--;
        page[pageNo].SetActive(true);

        nextbutton.SetActive(true);
        if (pageNo == 0)
            backbutton.SetActive(false);
    }

    public void BackSelectScene()
    {
        audio.PlayOneShot(drum_dd);
        Invoke("GoSelectScene",0.5f);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("MUSIC", 0);
    }

    private void GoSelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }
}
