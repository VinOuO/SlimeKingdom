using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
    ISlime_info slime_info;
    float start_time = 0;
    int erupt_times = 0;
    float ef_time, ef_time_2;

    void Start () {
        start_time = Time.time;
        StartCoroutine(erupted());
        ef_time = 0;
        ef_time_2 = 0;
    }
	
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (Time.time - ef_time >= 0.1f)
        {
            ef_time = Time.time;
            if (other.tag == "PPAP")
            {
                other.gameObject.GetComponent<Player>().get_attack(0, 0.1f, transform.position, "slow");
            }
            else if (other.tag == "Slime")
            {
                slime_info = other.gameObject.GetComponent(typeof(ISlime_info)) as ISlime_info;
                slime_info.Get_attack(0, 0.1f, transform.position, "slow");
            }
        }
        if (Time.time - ef_time_2 >= 0.5f)
        {
            ef_time_2 = Time.time;
            if (other.tag == "PPAP")
            {
                other.gameObject.GetComponent<Player>().get_attack(1, 0, transform.position, "none");
            }
            else if (other.tag == "Slime")
            {
                slime_info = other.gameObject.GetComponent(typeof(ISlime_info)) as ISlime_info;
                slime_info.Get_attack(1, 0, transform.position, "none");
            }
        }

    }

    /// <summary>
    /// 火山爆發
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator erupted()
    {
        yield return new WaitForSeconds(0.01f);
        if (erupt_times < 1250 && erupt_times >= 600)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.01f, transform.localScale.y, transform.localScale.z + 0.01f);
        }
        erupt_times++;
        StartCoroutine(erupted());
    }
}
