using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_grass : Monster_normal
{
    ISlime_info slime_info;
    string size = "big";
    GameObject copy;
    public GameObject copy_prefab;

    void Start () {
        base.Start();
        if (this.gameObject.name == "Copy_1")
        {
            transform.localScale /= 2;
            Health /= 2;
            health = Health;
            size = "middle";
            patient = Patient;
        }
        else if (this.gameObject.name == "Copy_2")
        {
            transform.localScale /= 4;
            Health /= 4;
            health = Health;
            size = "small";
            patient = Patient;
        }
    }
	
	void Update () {
        base.Update();
        split();
	}
    /// <summary>
    /// 分裂
    /// </summary>
    void split()
    {
        if (health / Health <= 0.5)
        {
            if (size == "big")
            {
                copy = Instantiate(copy_prefab, this.transform.position, this.transform.rotation);
                copy.gameObject.name = "Copy_1";
                copy = Instantiate(copy_prefab, this.transform.position, this.transform.rotation);
                copy.gameObject.name = "Copy_1";
                Destroy(this.gameObject);
            }
            else if (size == "middle")
            {
                copy = Instantiate(copy_prefab, this.transform.position, this.transform.rotation);
                copy.gameObject.name = "Copy_2";
                copy = Instantiate(copy_prefab, this.transform.position, this.transform.rotation);
                copy.gameObject.name = "Copy_2";
                Destroy(this.gameObject);
            }
        }
    }
}
