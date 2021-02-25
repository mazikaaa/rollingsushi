using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : unitBase
{
    GameManager gameManager;
    public GameObject Status;
    Text status_text;
    Animator anim;
    public AudioClip RepUp_SE,RepDown_SE,Skill_SE;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        gameManager = gamemanager.GetComponent<GameManager>();
        unitmanager = GetComponent<UnitManager>();

        status_text = Status.GetComponent<Text>();

        anim = this.gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
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
            gameManager.RaiseRep();
            Leave();
        }

        //着席時間を超えたら
        if (leave >= leavetime && setUnit)
        {
            if ((float)amount / (float)eatamount <= 0.50f)
            {
                //評判下がる
                LowerAnim();
                gameManager.LowerRep();
            }
            Leave();
        }
    }

    private void RaiseAnim()
    {
        status_text.text = "評価UP！！";
        status_text.color = new Vector4(255, 0, 0, 255);

        audiosource.PlayOneShot(RepUp_SE);
        anim.SetTrigger("RepUp");
    }

    private void LowerAnim()
    {
        status_text.text = "評価DOWN……";
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

}
