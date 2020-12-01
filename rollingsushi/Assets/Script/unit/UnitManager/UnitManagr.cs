using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManagr : unitBase
{
    GameManager gameManager;
    public GameObject Status;
    Text status_text;
    Animator anim;
    public AudioClip RepUp_SE,RepDown_SE;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        gameManager = gamemanager.GetComponent<GameManager>();

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
        }

        //満腹なら
        if (amount >= eatamount && setUnit)
        {
            //評判が上がる
            RaiseAnim();
            audiosource.PlayOneShot(RepUp_SE);
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
                audiosource.PlayOneShot(RepDown_SE);
                gameManager.LowerRep();
            }
            Leave();
        }
    }

    private void RaiseAnim()
    {
        status_text.text = "評価UP！！";
        status_text.color = new Vector4(255, 0, 0, 255);

        anim.SetTrigger("RepUp");
    }

    private void LowerAnim()
    {
        status_text.text = "評価DOWN……";
        status_text.color = new Vector4(0, 0, 255, 255);

        anim.SetTrigger("RepDown");
    }

}
