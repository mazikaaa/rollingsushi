using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoneEvent : Event
{
    //イベントをリセットするため、何も起きないイベント

    GameObject test;
    public override void InitEvent()
    {
       

    }
    
    public override void ActionEvent()
    {

    }

    public override void ExitEvent()
    {

    }

    public override string GetTitle()
    {
        return "なし";
    }

    public override string GetText()
    {
        return "";
    }
}
