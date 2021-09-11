using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_ditect : MonoBehaviour
{
    float time = 0;

    private void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().move_way == "2.5D")
        {
            transform.localScale = new Vector3(1, 0.1f, 4);
        }
        else
        {
            transform.localScale = new Vector3(4, 0.1f, 4);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "floor")
        {
            transform.parent.GetComponent<Player>().hit_terrain = true;
            transform.parent.GetComponent<Player>().time_hit_terrain = Time.time;
        }
    }
}
