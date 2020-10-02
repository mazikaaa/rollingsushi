using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{

    public GameObject[] Gameframe;
    public GameObject AudioPrefab;
    public AudioClip drum_d, drum_dd;
    AudioSource audioSource;
    private int music;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        music = PlayerPrefs.GetInt("MUSIC", 0);
        if (music == 0)
        {
            GameObject Audio = Instantiate(AudioPrefab);
            PlayerPrefs.SetInt("MUSIC", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrueGame(int i)
    {
      audioSource.PlayOneShot(drum_d);
      Gameframe[i-1].SetActive(true);
    }

    public void SetFalseGame(int i)
    {
      Gameframe[i - 1].SetActive(false);
    }

    public void GoGameScene(int i)
    {
        audioSource.PlayOneShot(drum_dd);
        PlayerPrefs.SetInt("MUSIC", 0);
        SceneManager.LoadScene("GameScene"+i);
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

    public void TutorialButton()
    {
        audioSource.PlayOneShot(drum_dd);
        Invoke("GoTutorial", 0.3f);
    }

    private void GoTutorial()
    {
        PlayerPrefs.SetInt("MUSIC", 0);
        SceneManager.LoadScene("TutorialScene");
    }

    public void GoUnitSetScene()
    {
        audioSource.PlayOneShot(drum_dd);
        Invoke("GoUnitSet", 0.3f);
    }

    private void GoUnitSet()
    {
        SceneManager.LoadScene("UnitSetScene");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("MUSIC", 0);
    }
}
