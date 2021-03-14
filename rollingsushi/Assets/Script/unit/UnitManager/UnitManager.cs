using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//食事席に配置されたお客さんのアニメーションを制御する　＆　時間の管理をする
public class UnitManager : unitBase
{
    //演出（アニメーション）関連
    public GameObject Status;
    Text status_text;
    Animator anim;
    public AudioClip RepUp_SE,RepDown_SE,Skill_SE,Poison_SE;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        unitmanager = GetComponent<UnitManager>();

        //演出情報の初期化
        status_text = Status.GetComponent<Text>();
        anim = this.gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //席にお客さんが配置されているなら時間を計測する
        if (setUnit == true)
        {
            leave += Time.deltaTime;
            eattime += Time.deltaTime;
            clockhand.localRotation= Quaternion.Euler(0,0,(leave/leavetime)*-360);
            clock_guage.fillAmount = 1.0f * leave / leavetime;
        }

        //満腹なら
        if (amount >= eatamount && setUnit)
        {
            //評判が上がる
            RaiseAnim();
            gamemanager.RaiseRep();
            Leave();
        }

        //着席時間を超えたら
        if (leave >= leavetime && setUnit)
        {
            if ((float)amount / (float)eatamount <= 0.50f)
            {
                //評判下がる
                LowerAnim();
                gamemanager.LowerRep();
            }
            Leave();
        }
    }


    private void RaiseAnim()
    {
        status_text.text = "評判UP！！";
        status_text.color = new Vector4(255, 0, 0, 255);

        audiosource.PlayOneShot(RepUp_SE);
        anim.SetTrigger("RepUp");
    }

    private void LowerAnim()
    {
        status_text.text = "評判DOWN……";
        status_text.color = new Vector4(0, 0, 255, 255);

        audiosource.PlayOneShot(RepDown_SE);
        anim.SetTrigger("RepDown");
    }

    public void SkillAnim()
    {
        status_text.text = "スキル";
        status_text.color = new Vector4(50, 150, 0 , 255);

        audiosource.PlayOneShot(Skill_SE);
        anim.SetTrigger("Skill");
    }

    public void PoisonAnim()
    {
        status_text.text = "食あたり…";
        status_text.color = new Vector4(50, 150, 0, 255);

        audiosource.PlayOneShot(Poison_SE);
        anim.SetTrigger("Poison");
    }



}
