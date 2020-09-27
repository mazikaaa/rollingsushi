using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitSetManager : MonoBehaviour
{
    [SerializeField] GameObject[] drops = new GameObject[8];
    public GameObject cautionpanel;
    private string[] dropname = new string[8];
    public AudioClip drum_d, drum_dd;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCompleteButton()
    {
        int i, j;
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

        Invoke("GoSelect", 0.3f);
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

}
