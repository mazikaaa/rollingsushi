using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitSetManager : MonoBehaviour
{
    [SerializeField] GameObject[] drops = new GameObject[8];

    public GameObject cautionpanel,backbutton,nextbutton;
    public GameObject[] page = new GameObject[5];
    public AudioClip drum_d, drum_dd;

    private string[] dropname = new string[8];
    private int pageNo=0;
    private float volume;
    private int i, j;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volume= volume = PlayerPrefs.GetFloat("SE", 1.0f);
        audioSource.volume *= volume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //セットしたユニットに編成を確定するボタン(ステージ選択画面に戻る)
    public void SetCompleteButton()
    {
        drops = GameObject.FindGameObjectsWithTag("drop");

        i = 0;
        foreach (GameObject drop in drops)
        {
            dropname[i] = drop.GetComponent<DropUnitSet>().dropname;
            for (j = 0; j < i; j++)
            {
                if (dropname[i] == dropname[j])
                {
                    cautionpanel.SetActive(true);
                    audioSource.PlayOneShot(drum_d);
                    return;
                }
            }
            i++;
        }

        for (j = 0; j < 8; j++)
        {
            PlayerPrefs.SetString("Unit" + (j + 1), dropname[j]);
        }

        audioSource.PlayOneShot(drum_dd);

        Invoke("GoSelect", 0.4f);
    }

    //編成を最初の状態に戻す
    public void UnitResetButton()
    {
        drops = GameObject.FindGameObjectsWithTag("drop");

        foreach(GameObject drop in drops)
        {
            drop.GetComponent<DropUnitSet>().Init_SetUnit();
        }

        audioSource.PlayOneShot(drum_d);
    }

    private void GoSelect()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void BackUnitSelect()
    {
        audioSource.PlayOneShot(drum_d);
        cautionpanel.SetActive(false);
    }

    //次のページに進む
    public void AdvancePage()
    {
        audioSource.PlayOneShot(drum_d);
        page[pageNo].SetActive(false);
        pageNo++;
        page[pageNo].SetActive(true);

        backbutton.SetActive(true);
        if (pageNo == page.Length-1)
            nextbutton.SetActive(false);
    }
    //前のページに戻る
    public void RetrunPage()
    {
        audioSource.PlayOneShot(drum_d);
        page[pageNo].SetActive(false);
        pageNo--;
        page[pageNo].SetActive(true);

        nextbutton.SetActive(true);
        if (pageNo == 0)
            backbutton.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("MUSIC", 0);
    }

}
