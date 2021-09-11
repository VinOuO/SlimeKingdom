using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    ISlime_info slime_info;
    float start_time = 0;
    int dry_times = 0;
    bool is_dryed = false;
    float ef_time;

    void Start()
    {
        start_time = Time.time;
        ef_time = 0;
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
        if (dry_times >= 300)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y, transform.localScale.z - 0.1f);
        }
        if (transform.localScale.x<= 30)
        {
            is_dryed = true;
        }
        if (transform.localScale.x <= 5)
        {
            Destroy(this.gameObject);
        }
        dry_times++;
        StartCoroutine(dryed());
    }

    private void OnTriggerEnter(Collider other)
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (is_dryed)
        {
            if (other.tag == "PPAP")
            {
                other.gameObject.GetComponent<Player>().get_attack(0, 180, transform.position, "root");
                other.gameObject.GetComponent<Player>().is_minigame_root = true;
                other.gameObject.GetComponent<Player>().mini_game_rooted_start();
            }
            else if (other.tag == "Slime")
            {
                slime_info = other.gameObject.GetComponent(typeof(ISlime_info)) as ISlime_info;
                slime_info.Get_attack(0, 5, transform.position, "root");
            }
        }
    }
}
