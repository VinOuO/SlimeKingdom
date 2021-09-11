using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_lava : Monster_normal
{
    ISlime_info slime_info;
    public GameObject volcano;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
        if (skill_charge >= 25)
        {
            summon_tornado();
            skill_charge = 0;
        }
    }
    void summon_tornado()
    {
        Instantiate(volcano, transform.position + new Vector3(Random.Range(0, 20), -transform.position.y - 4, Random.Range(0, 20)), volcano.transform.rotation);
    }
}
