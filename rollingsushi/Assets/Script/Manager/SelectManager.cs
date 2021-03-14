using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    //サウンド関連
    public GameObject[] Gameframe;
    public GameObject AudioPrefab;
    public AudioClip drum_d, drum_dd;
    AudioSource audioSource;
    private int music;
    private float volume;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("SE", 1.0f);
        audioSource.volume *= volume;

        music = PlayerPrefs.GetInt("MUSIC", 0);
        if (music == 0)
        {
            GameObject Audio = Instantiate(AudioPrefab);
            PlayerPrefs.SetInt("MUSIC", 1);
        }
    }

    public void SetTrueGame(int i)
    {
      audioSource.PlayOneShot(drum_d);
      Gameframe[i-1].SetActive(true);
    }

    public void SetFalseGame(GameObject frame)
    {
        audioSource.PlayOneShot(drum_d);
        frame.SetActive(false);
    }

    //指定したステージに移る
    public void GoGameScene(int i)
    {
        audioSource.PlayOneShot(drum_dd);
        PlayerPrefs.SetInt("MUSIC", 0);
        SceneManager.LoadScene("GameScene"+i);
    }
    //ゲームを終了する
    public void GameEndButton()
    {
        #if UNITY_EDITOR
        PlayerPrefs.SetInt("MUSIC", 0);
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        PlayerPrefs.SetInt("MUSIC", 0);
        UnityEngine.Application.Quit();
#endif

    }

    //チュートリアル画面に移る
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

    //ユニット編成画面に移る
    public void UnitSetSceneButton()
    {
        audioSource.PlayOneShot(drum_dd);
        Invoke("GoUnitSetScene", 0.3f);
    }

    public void GoUnitSetScene()
    {
        SceneManager.LoadScene("UnitSetScene");
    }

    //お品書き画面へ移る
    public void SushiLibralySceneButton()
    {
        audioSource.PlayOneShot(drum_dd);
        Invoke("GoSushiLibraly", 0.3f);
    }

    public void BackStartSceneButton()
    {
        audioSource.PlayOneShot(drum_dd);
        Invoke("BackStartScene", 0.3f);
    }

    public void BackStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    private void GoSushiLibraly()
    {
        SceneManager.LoadScene("SushiLibraryScene");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("MUSIC", 0);
    }
}
