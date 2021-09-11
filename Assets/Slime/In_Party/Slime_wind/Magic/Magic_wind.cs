using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_wind : MonoBehaviour {

    ISlime_info slime_info;
    float start_time;
    void Start()
    {
        start_time = Time.time;
    }

    void Update () {
        if (Time.time - start_time >= 30)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Slime")
        {
            if (other.gameObject.name != "Shadow")
            {
                slime_info = other.gameObject.transform.GetComponent(typeof(ISlime_info)) as ISlime_info;
                if (slime_info.Is_charge)
                {
                    slime_info[2] = true;
                }
            }
            else
            {
                other.gameObject.GetComponent<Shadow>().charge_state[2] = true;
            }
        }
    }
}
