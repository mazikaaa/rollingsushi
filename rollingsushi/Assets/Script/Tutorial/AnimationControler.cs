using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //チュートリアルで流すアニメーションを制御する
public class AnimationControler : MonoBehaviour
{
    Animator frame_animator;
    GameObject frame, Drag;
    public bool dd1_flag , dd2_flag;

    // Start is called before the first frame update
    void Start()
    {
        frame = GameObject.Find("Frame");
        frame_animator = frame.GetComponent<Animator>();
        frame_animator.SetBool("DD1", true);

        Drag = GameObject.Find("Drag1");
        dd1_flag = false;
        dd2_flag = false;
    }

    // Update is called once per frame
    void Update()
    {

        //フラグが立っているなら、指定のアニメーションを起動する。
        dd2_flag = !(Drag.GetComponentInChildren<Drag>().SetAnimation());

        dd1_flag = Drag.GetComponentInChildren<Drag>().SetAnimation();

        if (dd2_flag)
        {
            frame_animator.SetBool("DD1", true);
            frame_animator.SetBool("DD2", false);
            dd1_flag = false;
        }
        else if (dd1_flag)
        {
            frame_animator.SetBool("DD2", true);
            frame_animator.SetBool("DD1", false);
            dd2_flag = false;
        }
    }
}
