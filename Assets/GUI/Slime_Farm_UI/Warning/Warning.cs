using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour {

    float warning_time = 0;
    bool set = false;
    GameObject farm_1;
	void Start () {
        farm_1 = GameObject.Find("Slime_farm_world").transform.GetChild(0).gameObject;

    }

	void Update () {
        if (farm_1.GetComponent<Slime_farm>().warning != "87" && !set)
        {
            warning_time = Time.time;
            set = true;
        }
        if (Time.time - warning_time >= 1)
        {
            this.GetComponent<Text>().text = farm_1.GetComponent<Slime_farm>().warning = "87";
            this.GetComponent<Text>().text = "";
            set = false;
        }
        else
        {
            this.GetComponent<Text>().text = farm_1.GetComponent<Slime_farm>().warning;
        }
    }
}
