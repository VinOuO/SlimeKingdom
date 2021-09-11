using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour {
    GameObject Sun;
    public int Day = 0;
    public float StartOfToday = 0, TimeOfToday = 0, Day_long = 300, TimeMorning = 0f, TimeAfternoon = 0.5f, TimeNight = 0.8f;
    public string ouverture = "Morning";
    void Start () {
        Sun = Camera.main.transform.GetChild(0).gameObject;
        StartOfToday = Time.time;

    }
	
	void Update () {
        if (Sun == null)
        {
            Sun = GameObject.Find("Sun");
        }
        if (Time.time - StartOfToday >= Day_long)
        {
            Day++;
            StartOfToday = Time.time;
        }
        TimeOfToday = Time.time - StartOfToday;

        if (TimeOfToday / Day_long <= TimeAfternoon)
        {
            ouverture = "Morning";
        }
        else if (TimeOfToday / Day_long <= TimeNight)
        {
            ouverture = "Afternoon";
        }
        else
        {
            ouverture = "Night";
        }

        switch (ouverture)
        {
            case "Morning":
                Sun.GetComponent<Light>().intensity = 1.3f;
                break;
            case "Afternoon":
                Sun.GetComponent<Light>().intensity = 1;
                break;
            case "Night":
                Sun.GetComponent<Light>().intensity = 0.3f;
                break;
        }

	}


}
