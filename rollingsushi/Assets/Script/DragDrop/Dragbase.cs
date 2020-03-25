using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragbase : MonoBehaviour
{
    public GameObject[] unit = new GameObject[10];
    protected string iconname;
    public Sprite test;
    protected void GenerateUnit()
    {
        iconname = unit[0].name;
        this.gameObject.GetComponent<Image>().sprite = unit[0].GetComponentInChildren<Generatedata>().Generateicon;
    }
}
