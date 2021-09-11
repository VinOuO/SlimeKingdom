using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partical_fallow_path : MonoBehaviour {
    public string path_name;
    public float time, time_tick;
	void Start () {
        time_tick = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - time_tick >= time)
        {
            time_tick = Time.time;
            iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(path_name), "easetype", iTween.EaseType.easeInOutSine, "time", time));
        }
    }
}
