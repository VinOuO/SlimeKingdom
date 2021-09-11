using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_grass : Slime_normal {
    IMonster_info monster_info;
    void Start()
    {
        type = "grass";
        base.Start();
    }

    void Update()
    {
        base.Update();

        Is_moving();

        anim_state();
    }

    private void OnCollisionEnter(Collision collision)
    {
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
                    monster_info.Get_attack(5, 5, "root");
                }
                i++;
            }
            hit = true;
        }

    }

}
