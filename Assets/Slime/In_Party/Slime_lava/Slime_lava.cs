using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_lava : Slime_normal
{
    IMonster_info monster_info;
    ISlime_info slime_info;
    float magma_time = 0;
    public GameObject magma, lava_slime_magma;
    Vector3 magma_pos;
    int magma_contral;
    void Start()
    {
        type = "lava";
        base.Start();
        magma_contral = player.GetComponent<Player>().magma_contral;
        player.GetComponent<Player>().magma_contral++;
        for (int i = 0; i < 20; i++)
        {
            //lava_slime_magma = Instantiate(magma, transform.position, magma.transform.rotation);
            //lava_slime_magma.name = "Magma";
        }
    }

    void Update()
    {
        base.Update();
        base.Update();
        if (is_charge)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !charge_state[6])
            {
                broadcast.GetComponent<Broadcast>().message_update(nickname + " 死亡之徑");
                charge_state[6] = true;
            }
        }
        if (charge_state[6] && Time.time - magma_time >= 0.1f)
        {
            magma_time = Time.time;
            lava_slime_magma = Instantiate(magma, transform.position, magma.transform.rotation);
            lava_slime_magma.name = "Magma" + magma_contral;
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Magma" + magma_contral)
        {
            //magma_pos.Set(transform.position.x, transform.position.y, transform.position.z);
            //lava_slime_magma = Instantiate(magma, transform.position, transform.rotation);
            //lava_slime_magma.name = "Magma" + magma_contral;
        }
    }
}
