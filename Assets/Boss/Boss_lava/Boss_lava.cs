using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_lava : Boss_normal {
    float time = 0;

    public GameObject fire_ball, fire_ball_lava_boss;
    Vector3 fire_ball_fall_pos;
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Boss_skill_3();
        }
        if (skill_charge >= 10)
        {
            Random_skill(Random.Range(1, 101));
            skill_charge = 0;
        }
    }

    void Random_skill(int _chance)
    {
        if (_chance <= 33)
        {
            Boss_skill_1();
        }
        else if (_chance <= 66)
        {
            Boss_skill_2();
        }
        else
        {
            Boss_skill_3();
        }
    }


    void Boss_skill_3()
    {
        StartCoroutine(fire_ball_rain(Time.time, 10));
    }

    /// <summary>
    /// 隕石雨
    /// </summary>
    protected IEnumerator fire_ball_rain(float _start_time, float _effect_time)
    {
        yield return new WaitForSeconds(0.1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            transform.GetChild(7).GetComponent<Boss_lava_skill_test>().fire_ball_sum();
            StartCoroutine(fire_ball_rain(_start_time, _effect_time));
        }
        else
        {
            StopCoroutine(fire_ball_rain(_start_time, _effect_time));
        }
    }
}
