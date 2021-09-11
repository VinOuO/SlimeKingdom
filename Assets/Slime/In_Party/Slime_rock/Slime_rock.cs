using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_rock : Slime_normal
{
    IMonster_info monster_info;
    public bool a;

    void Start()
    {
        type = "rock";
        base.Start();
    }

    void Update()
    {
        base.Update();
        if (is_charge)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                broadcast.GetComponent<Broadcast>().message_update(nickname + " 勢不可擋");
                charge_state[0] = true;
                charge_state[2] = true;
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
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && is_charge)
        {
            monster_info = collision.gameObject.GetComponent(typeof(IMonster_info)) as IMonster_info;
            monster_info.Get_attack(10, 3, "knock");
        }
        if (form == 4 && !hit)
        {
            int layerMask = 1 << 8;
            Collider[] hitColliders;
            float radius;
            int i;
            i = 0;
            radius = 100;
            hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.tag == "Enemy")
                {
                    monster_info = hitColliders[i].gameObject.GetComponent(typeof(IMonster_info)) as IMonster_info;
                    monster_info.Get_attack(10, 3, "knock");
                }
                i++;
            }
            hit = true;
        }
    }
    */
}
