using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    IMonster_info monster_info;
    float start_time = 0;
    float ef_time, ef_time2;
    int num = 0;
    void Start()
    {
        start_time = Time.time;
        ef_time = 0;
        ef_time2 = 0;
    }

    private void Update()
    {
        if (Time.time - start_time >= 3)
        {
            Destroy(this.gameObject);
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - ef_time >= 0.1f)
        {
            ef_time = Time.time;
            if (other.tag == "Enemy")
            {
                monster_info = other.gameObject.GetComponent(typeof(IMonster_info)) as IMonster_info;
                monster_info.Get_attack(0, 0.1f, "slow");
            }
        }
    }
    */
    private void OnTriggerStay(Collider other)
    {
        if (Time.time - ef_time >= 0.1f)
        {
            ef_time = Time.time;
            if (other.tag == "Enemy")
            {
                monster_info = other.gameObject.GetComponent(typeof(IMonster_info)) as IMonster_info;
                monster_info.Get_attack(0, 0.1f, "slow");
            }
        }
        if (Time.time - ef_time2 >= 1f)
        {
            ef_time2 = Time.time;
            if (other.tag == "Enemy")
            {
                monster_info = other.gameObject.GetComponent(typeof(IMonster_info)) as IMonster_info;
                monster_info.Get_attack(3, 0, "none");
            }
        }
    }
}
