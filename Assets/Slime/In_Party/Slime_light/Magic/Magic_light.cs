using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_light : MonoBehaviour {

    ISlime_info slime_info;
    float start_time;
    void Start()
    {
        start_time = Time.time;
    }

    void Update()
    {
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
                    slime_info[4] = true;
                }
            }
        }
    }
}
