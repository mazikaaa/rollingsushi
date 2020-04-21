using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragbase : MonoBehaviour
{
    int i;

    public GameObject[] unit = new GameObject[10];
    public bool iconflag = false;
    public Sprite test;

    protected float deleteicon,generateicon=5.0f;//消去、生成までの時間
    protected string iconname;
    protected string guageflag = "generate";
    protected float deletetime, generatetime;

    protected void GenerateUnit()
    {
        i = Random.Range(0, 3);//寿司の追加時に変更忘れずに
        iconname = unit[i].name;
        deleteicon=unit[i].GetComponentInChildren<Generatedata>().deletespan;
        this.gameObject.GetComponent<Image>().sprite = unit[i].GetComponentInChildren<Generatedata>().Generateicon;
        gameObject.GetComponent<Image>().color = Vector4.one;
        guageflag = "delete";
        generatetime = 0.0f;
        iconflag = true;
    }

    public void DeleteUnit()
    {
        this.gameObject.GetComponent<Image>().sprite = null;
        this.gameObject.GetComponent<Image>().color = Vector4.zero;
        guageflag = "generate";
        deletetime = 0.0f;
        iconflag = false;
    }

    public void DeleteShadowUnit()
    {
        GameObject[] drops = GameObject.FindGameObjectsWithTag("drop");
        foreach (GameObject drop in drops)
        {
            if (drop.GetComponent<Drop>().shadowUnit)
            {
                drop.GetComponent<Drop>().iconImage.color = Vector4.zero;
                drop.GetComponent<Drop>().shadowUnit = false;
            }
        }
    }
   
}
