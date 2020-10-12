using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event:MonoBehaviour
{
    //ゲーム中に起きるイベント
    public virtual void InitEvent()
    {

    }

    public virtual void ExitEvent()
    {

    }

    public virtual string GetTitle()
    {
        return null;
    }

    public virtual string GetText()
    {
        return null;
    }
}
