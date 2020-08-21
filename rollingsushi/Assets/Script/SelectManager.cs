using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoGameScene(int i)
    {
        SceneManager.LoadScene("GameScene1");
    }
    public void GameEndButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

    }

    public void GoUnitScene() {
        SceneManager.LoadScene("UnitSetScene");
    }
}
