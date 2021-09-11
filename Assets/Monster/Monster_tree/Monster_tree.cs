using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_tree : Monster_normal
{
    ISlime_info slime_info;
    /// <summary>
    /// 出生時間
    /// </summary>
    float birth_time;
    bool time_1, time_2;
    void Start()
    {
        birth_time = Time.time;
        time_1 = true;
        time_2 = true;
        base.Start();
    }

    void Update()
    {
        base.Update();
        if(Time.time- birth_time >=10 && time_1)
        {
            grow(1);
            time_1 = false;
        }
        else if(Time.time - birth_time >= 20 && time_2)
        {
            grow(2);
            time_2 = false;
        }
    }

    void grow(int _time)
    {
        if (_time == 1)
        {
            Health += 100;
            health += 100;
            attack_damage += 5;
        }
        else
        {
            Health += 200;
            health += 200;
            attack_damage += 10;
        }
    }
}
