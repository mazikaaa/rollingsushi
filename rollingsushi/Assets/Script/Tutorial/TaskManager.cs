using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TaskManager : MonoBehaviour
{
    // チュートリアル用UI
    protected RectTransform tutorialTextArea;
    protected Text TutorialTitle;
    protected Text TutorialText;
    protected GameObject NextButton;
    protected GameObject BackButton;

    // チュートリアルタスク
    protected ITutorialTask currentTask;
    protected List<ITutorialTask> tutorialTask;

    protected int Tasknum = 0;//現在のリストの番号を保持する


    //音楽系
    private new AudioSource audio;
    public AudioClip drum_d;

    void Start()
    {
        // チュートリアル表示用UIのインスタンス取得
        tutorialTextArea = GameObject.Find("TextBoard").GetComponent<RectTransform>();
        TutorialTitle = tutorialTextArea.Find("Title").GetComponent<Text>();
        TutorialText = tutorialTextArea.Find("Text").GetComponentInChildren<Text>();
        NextButton = GameObject.Find("Next");
        BackButton = GameObject.Find("Back");
        audio = GetComponent<AudioSource>();


        // チュートリアルの一覧
        tutorialTask = new List<ITutorialTask>()
        {
            new FirstTask(),
            new DragTask(),
            new DropTask(),
            new OperationTask(),
            new SushiTask(),
            new SushiTask2(),
            new RepoTask(),
            new EventTask(),
            new ShuffleTask(),
            new FinishTask(),
        };

        currentTask = tutorialTask.First();

        // 最初のチュートリアルを設定
        StartCoroutine(SetCurrentTask(currentTask));
    }

    void Update()
    {
        if (currentTask.CheckTask())
        {
            currentTask.NextTask();
            Tasknum++;
            StartCoroutine(SetCurrentTask(tutorialTask.ElementAt(Tasknum)));
        }

    }

    /// 新しいチュートリアルタスクを設定する
    protected IEnumerator SetCurrentTask(ITutorialTask task, float time = 0)
    {

        // timeが指定されている場合は待機
        yield return new WaitForSeconds(time);

        currentTask = task;

        // UIにタイトルと説明文を設定
        TutorialTitle.text = task.GetTitle();
        TutorialText.text = task.GetText();

        //ボタンの表示を設定
        NextButton.SetActive(task.GetNextButton());
        BackButton.SetActive(task.GetBackButton());

        // チュートリアルタスク設定時用の関数を実行
        task.OnTaskSetting();

    }

    public void MoveNextTask()
    {
        Tasknum++;
        audio.PlayOneShot(drum_d);
        currentTask.NextTask();
        StartCoroutine(SetCurrentTask(tutorialTask.ElementAt(Tasknum)));
    }

    public void MoveBackTask()
    {
        Tasknum--;
        audio.PlayOneShot(drum_d);
        StartCoroutine(SetCurrentTask(tutorialTask.ElementAt(Tasknum)));
    }


    public void MoveGoSelect()
    {
        SceneManager.LoadScene("SelectScene");
    }

    private void OnApplicationQuit()
    {
      
    }
}
