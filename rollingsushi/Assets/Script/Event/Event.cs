using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Event
{
    //ゲーム中に起きるイベント
    void InitEvent();

    void ExitEvent();

    string GetText();
}
