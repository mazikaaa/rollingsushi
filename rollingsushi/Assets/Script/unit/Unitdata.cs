using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

//食事席にいるお客さんに必要な情報
[System.Serializable]
public class Unitdata : MonoBehaviour
{
    public float probability_like;//好きな寿司を食べる確率
    public float probability_normal;//普通の寿司を食べる確率
    public float waittime_like;//好きな寿司を食べた後、次の寿司を食べるまでの時間
    public float waittime_normal;//普通の寿司を食べた後、次の寿司を食べるまでの時間
    public string like;//好きな寿司の名前

    public int eatamount = 0;//寿司を食べる量
    public float leavetime = 0.0f, eventtime = 0.0f;//eventによる時間の上下
    public int unittype;//1なら一人,2ならペア,4ならグループ

    public Skill skill;//お客さんの特殊能力

    public Sprite[] Separate_image = new Sprite[1];//お客さんの画像
}