using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_anim : MonoBehaviour {
    //-----------------------------------------------animator
    protected Animator anim;
    public int slime_anim_state = 1;
    public float time_is_move;
    protected Vector3 move_b, move_a;
    public float shoot_time = -10;
    //-----------------------------------------------animator
    void Start () {
        //-----------------------------------------------animator
        move_b = transform.position;
        time_is_move = Time.time;
        anim = transform.GetChild(3).GetChild(2).GetComponent<Animator>();
        //-----------------------------------------------animator
    }

    void Update () {
        Is_moving();
        anim_state();
        Debug.Log(slime_anim_state);
    }
    //-----------------------------------------------animator
    /// <summary>
    /// 史萊姆是否在移動
    /// </summary>
    /// <returns></returns>
    protected void Is_moving()
    {

        move_a = transform.position;
        if (Time.time - shoot_time >= 1f)
        {
            if (Time.time - time_is_move >= 0.0001f)
            {
                time_is_move = Time.time;
                if (move_a == move_b)
                {
                    slime_anim_state = 0;
                }
                else
                {
                    slime_anim_state = 1;
                }
                move_b = transform.position;
            }
        }
        else
        {
            slime_anim_state = 2;
        }
    }
    /// <summary>
    /// 動畫
    /// </summary>
    protected void anim_state()
    {
        anim.SetInteger("Slime_anim_comtral", slime_anim_state);
    }
    //-----------------------------------------------animator
}
