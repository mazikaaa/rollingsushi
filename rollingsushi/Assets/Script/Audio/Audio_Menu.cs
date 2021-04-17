using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio_Menu : MonoBehaviour
{
    protected List<string> scenename;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        scenename = new List<string>()
        {
            "StartScene",
            "SelectScene",
            "UnitSetScene",
            "SushiLibraryScene"
        };
    }

    // Update is called once per frame
    void Update()
    {
        //特定のシーンでのみ、オーディオオブジェクトを継続させる
        foreach (string name in scenename)
        {
            if (SceneManager.GetActiveScene().name == name)
                return;
        }
            Destroy(this.gameObject);
    }
}
