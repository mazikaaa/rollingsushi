using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitSetManager : MonoBehaviour
{
    [SerializeField] GameObject[] drops=new GameObject[8];
    private string[] dropname=new string[8];
    public AudioClip drum_dd;
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
        int i = 0;
        drops = GameObject.FindGameObjectsWithTag("drop");

        foreach(GameObject drop in drops)
        {
            dropname[i] = drop.GetComponent<DropUnitSet>().dropname;
            i++;
        }

        int j;
        for (j = 0; j < 8; j++)
        {
            PlayerPrefs.SetString("Unit" + (j + 1),dropname[j]);
        }

        audioSource.PlayOneShot(drum_dd);

        Invoke("GoSelect", 0.3f);
    }

    private void GoSelect()
    {
        SceneManager.LoadScene("SelectScene");

    }
}
