using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {
    ISlime_info slime_info;
    float start_time = 0;
    int dry_times = 0;
    bool is_dryed = false;

    void Start()
    {
        start_time = Time.time;
        StartCoroutine(dryed());
    }

    /// <summary>
    /// 變乾
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator dryed()
    {
        yield return new WaitForSeconds(0.01f);
        if (dry_times < 1250 && dry_times >= 300)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.03f, transform.localScale.y, transform.localScale.z - 0.03f);
        }
        if (dry_times >= 1250)
        {
            is_dryed = true;
        }
        if (dry_times >= 1290)
        {
            Destroy(this.gameObject);
        }
        dry_times++;
        StartCoroutine(dryed());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PPAP")
        {
            other.gameObject.GetComponent<Player>().get_attack(0, 0, transform.position, "slip_in");
        }
        else if (other.tag == "Slime")
        {
            slime_info = other.gameObject.GetComponent(typeof(ISlime_info)) as ISlime_info;
            slime_info.Get_attack(0, 0, transform.position, "slip_in");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (is_dryed)
        {
            if (other.tag == "PPAP")
            {
                other.gameObject.GetComponent<Player>().get_attack(0, 0, transform.position, "slip_out");
            }
            else if (other.tag == "Slime")
            {
                slime_info = other.gameObject.GetComponent(typeof(ISlime_info)) as ISlime_info;
                slime_info.Get_attack(0, 0, transform.position, "slip_out");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PPAP")
        {
            other.gameObject.GetComponent<Player>().get_attack(0, 0, transform.position, "slip_out");
        }
        else if (other.tag == "Slime")
        {
            slime_info = other.gameObject.GetComponent(typeof(ISlime_info)) as ISlime_info;
            slime_info.Get_attack(0, 0.01f, transform.position, "slip_out");
        }
    }
}
