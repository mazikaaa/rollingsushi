using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject AudioPrefab;
    private int music;
    public AudioClip drum_dd;
    AudioSource audioSource;
    protected string[] default_unit = { "DK", "JK", "OL", "salaryman", "wife", "oldman", "DKpair", "salarypair" };

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

        for(int i = 0; i < 8; i++)
        {
            PlayerPrefs.SetString("Unit" + (i + 1), default_unit[i]);
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startButton()
    {
        audioSource.PlayOneShot(drum_dd);
        Invoke("Gostart", 0.3f);
    }

    private void  Gostart()
    {
        SceneManager.LoadScene("SelectScene");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("MUSIC", 0);
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
}
