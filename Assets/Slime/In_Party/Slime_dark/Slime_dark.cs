using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_dark : Slime_normal
{
    IMonster_info monster_info;
    ISlime_info slime_info;
    /// <summary>
    /// 開始治癒時間
    /// </summary>
    float time_buff_attackspeed;
    /// <summary>
    /// 每秒治癒一次
    /// </summary>
    float cold_down;
    void Start()
    {
        base.Start();
        type = "dark";
    }

    void Update()
    {
        base.Update();
        base.Update();
        if (is_charge)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                charge_state[5] = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && is_charge)
        {
            monster_info = other.gameObject.GetComponent(typeof(IMonster_info)) as IMonster_info;
            monster_info.Get_attack(charge_damage * combo_damage, 0, "none");
            if (charge_state[0])
            {
                monster_info.Get_attack(10, 3, "knock");
            }
            if (charge_state[1])
            {
                monster_info.Get_attack(5, 5, "burn");
            }
            if (charge_state[4])
            {

            }
            Debug.Log("Combo!");
            combo.GetComponent<Combo>().num_add();
        }
    }
}
