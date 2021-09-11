using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_mud : Boss_normal
{
    float time = 0;
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
        if (Time.time - time >= 5)
        {
            transform.GetChild(7).GetComponent<Boss_mud_skill>().Hide();
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
        transform.GetChild(7).GetComponent<Boss_mud_skill>().Show();
        time = Time.time;
    }
}
