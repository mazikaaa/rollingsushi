using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragbase : MonoBehaviour
{
    int i;
    public GameObject[] unit = new GameObject[10];
    protected float deleteicon,generateicon=5.0f;
    protected string iconname;
    public bool iconflag = false;
    public Sprite test;
    protected void GenerateUnit()
    {
        i = Random.Range(0, 2);
        iconname = unit[i].name;
        deleteicon=unit[i].GetComponentInChildren<Generatedata>().deletespan;
        this.gameObject.GetComponent<Image>().sprite = unit[i].GetComponentInChildren<Generatedata>().Generateicon;
        iconflag = true;
    }

    protected void DeleteUnit()
    {
        this.gameObject.GetComponent<Image>().sprite = null;
        iconflag = false;
    }
   
}
