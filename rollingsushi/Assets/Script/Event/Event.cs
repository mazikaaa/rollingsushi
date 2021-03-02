using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event:MonoBehaviour
{
    //ゲーム中に起きるイベント

    //ゲームの最初に起こす処理
    public virtual void InitEvent()
    {

    }

    //実際にイベント発生が発生した時の処理
    public virtual void ActionEvent()
    {

    }

    //イベントの終了時に起こす処理
    public virtual void ExitEvent()
    {

    }
　
    //タイトルの取得
    public virtual string GetTitle()
    {
        return null;
    }

    //テキストの取得
    public virtual string GetText()
    {
        return null;
    }
}
