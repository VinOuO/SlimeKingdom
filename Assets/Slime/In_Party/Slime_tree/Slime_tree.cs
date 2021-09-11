using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_tree : Slime_normal
{
    IMonster_info monster_info;
    ISlime_info slime_info;
    bool cast = false;
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
        type = "tree";
        base.Start();
    }

    void Update()
    {
        base.Update();
        if (is_charge)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int layerMask = 1 << 8;
                Collider[] hitColliders;
                float radius;
                int i;
                i = 0;
                radius = 100;
                hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
                if(hitColliders.Length > 0)
                {
                    if (hitColliders[0].gameObject.tag == "Enemy")
                    {
                        monster_info = hitColliders[0].gameObject.GetComponent(typeof(IMonster_info)) as IMonster_info;
                        monster_info.Get_attack(0, 30, "brain_wash");
                    }
                    i++;
                }
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
            Debug.Log("Combo!");
            combo.GetComponent<Combo>().num_add();
        }
    }
}
