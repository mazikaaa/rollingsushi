using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject AudioPrefab,setting_frame;
    public Slider Sli_BGM, Sli_SE;
    private int music;
    private float SE_default,BGM_default;
    public AudioClip drum_d,drum_dd;
    AudioSource audioSource,BGM_audio;
    protected string[] default_unit = { "DK", "JK", "OL", "salaryman", "wife", "oldman", "DKpair", "salarypair" };

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SE_default = audioSource.volume;

        music = PlayerPrefs.GetInt("MUSIC", 0);
        if (music == 0)
        {
            GameObject Audio = Instantiate(AudioPrefab);
            PlayerPrefs.SetInt("MUSIC", 1);
            BGM_audio = Audio.GetComponent<AudioSource>();
            BGM_default = BGM_audio.volume;
            Debug.Log(BGM_default);
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

    public void Setting(bool flag)
    {
        if (flag)
        {
            audioSource.PlayOneShot(drum_d);
            setting_frame.SetActive(false);
        }
        else
        {
            audioSource.PlayOneShot(drum_d);
            setting_frame.SetActive(true);
        }
    }

    public void BGM_Setting()
    {
        float volume;

        volume = Sli_BGM.value;
        BGM_audio.volume = BGM_default*volume;
        Debug.Log(volume);

        PlayerPrefs.SetFloat("BGM", volume);
    }

    public void SE_Setting()
    {
        float volume;

        volume = Sli_SE.value;
        audioSource.volume =SE_default* volume;
        Debug.Log(volume);

        PlayerPrefs.SetFloat("SE", volume);
    }
}
