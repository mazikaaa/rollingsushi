using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITutorialTask
{
    // チュートリアルのタイトルを取得する
    string GetTitle();


    // 説明文を取得する
    string GetText();

    //ボタンを所得する
    bool GetNextButton();
    bool GetBackButton();


    // チュートリアルタスクが設定された際に実行される
    void OnTaskSetting();

    // 次のタスクに進めるかを判定する
    bool CheckTask();

    //次のタスクに進むときに行う処理
    void NextTask();

    // 達成後に次のタスクへ遷移するまでの時間(秒)
    float GetTransitionTime();
}
