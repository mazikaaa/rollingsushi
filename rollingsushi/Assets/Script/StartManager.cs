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
    static private float SE_default,BGM_default;
    private float BGM_volume, SE_volume;
    public AudioClip drum_d,drum_dd;
    AudioSource audioSource,BGM_audio;
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
            BGM_audio = Audio.GetComponent<AudioSource>();

            BGM_default = BGM_audio.volume;
            SE_default = audioSource.volume;
            PlayerPrefs.SetFloat("BGM_DE",BGM_default);
            PlayerPrefs.SetFloat("SE_DE", SE_default);

            Debug.Log(BGM_default);
        }
        else
        {
            GameObject Audio = GameObject.Find("Audio_Menu(Clone)");
            BGM_audio = Audio.GetComponent<AudioSource>();
            BGM_default = PlayerPrefs.GetFloat("BGM_DE", 1.0f);
            SE_default= PlayerPrefs.GetFloat("SE_DE", 1.0f);

            Debug.Log(BGM_default);

        }

        /*
        for(int i = 0; i < 8; i++)
        {
            PlayerPrefs.SetString("Unit" + (i + 1), default_unit[i]);
        }
        */

        BGM_volume = PlayerPrefs.GetFloat("BGM", 1.0f);
        SE_volume= PlayerPrefs.GetFloat("SE", 1.0f);
        Sli_BGM.value = BGM_volume;
        Sli_SE.value = SE_volume;
        BGM_audio.volume = BGM_default * BGM_volume;
        audioSource.volume = SE_default * SE_volume;
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

        PlayerPrefs.SetFloat("BGM", volume);
    }

    public void SE_Setting()
    {
        float volume;

        volume = Sli_SE.value;
        audioSource.volume =SE_default* volume;

        PlayerPrefs.SetFloat("SE", volume);
    }
}
