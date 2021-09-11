using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_wind : Slime_normal
{
    IMonster_info monster_info;
    public GameObject spell;
    Vector3 spell_pos;
    //Vector3 blow_way;
    void Start()
    {
        type = "wind";
        base.Start();
    }

    void Update()
    {
        base.Update();
        if (is_charge)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !spelled)
            {
                spelled = true;
                spell_pos.Set(transform.position.x, transform.position.y + 1, transform.position.z);
                Instantiate(spell, spell_pos, spell.transform.rotation);
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
        }
    }
}
