using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[System.Serializable]
public class Unitdata : MonoBehaviour
{
    public float probability_like;
    public float probability_normal;
    public float waittime_like;
    public float waittime_normal;
    public string like;

    public int eatamount = 0;
    public float leavetime = 0.0f, eventtime = 0.0f;//eventによる時間の上下
    public bool setUnit = false;
    public int unittype;//1なら一人,2ならペア,4ならグループ

    public Skill skill;

    public Sprite[] Separate_image = new Sprite[1];
}