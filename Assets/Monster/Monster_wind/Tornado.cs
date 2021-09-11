using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour {
    Vector3 way;
    ISlime_info slime_info;
    float start_time;
    void Start () {
        way = GameObject.Find("Player").transform.position - transform.position;
        way.y = 0;
        way = way.normalized;
        start_time = Time.time;
	}

	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + way, 1f);
        if (Time.time - start_time >= 5)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PPAP")
        {
            other.gameObject.GetComponent<Player>().get_attack(10, 5, transform.position, "root");
            other.gameObject.GetComponent<Player>().get_attack(10, 5, transform.position, "stun");
        }
        else if (other.tag == "Slime")
        {
            slime_info = other.GetComponent(typeof(ISlime_info)) as ISlime_info;
            slime_info.Get_attack(10, 5, transform.position, "root");
            slime_info.Get_attack(10, 5, transform.position, "stun");
        }
    }
}
