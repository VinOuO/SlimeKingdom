using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_fire : Monster_normal
{
    ISlime_info slime_info;
    void Start () {
        base.Start();
	}
	
	void Update () {
        base.Update();
        if (health / Health <= 0.7f)
        {
            explosion();
        }
	}
    /// <summary>
    /// 自爆
    /// </summary>
    void explosion()
    {
        layerMask = 1 << 9;
        int i = 0;
        radius = 30;

        hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "PPAP")
            {
                hitColliders[i].gameObject.GetComponent<Player>().get_attack(30, 0, transform.position, "none");
            }
            else
            {
                slime_info = hitColliders[i].gameObject.GetComponent(typeof(ISlime_info)) as ISlime_info;
                slime_info.Get_attack(30, 0, transform.position, "none");
            }
            i++;
        }
        Destroy(this.gameObject);
    }
}
