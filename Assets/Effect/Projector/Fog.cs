using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour {

    GameObject player;
    public Color fog_color;
    Light sun;

	void Start () {
        sun = GameObject.Find("Sun").GetComponent<Light>();
        player = GameObject.Find("Player");
        RenderSettings.fog = true;
        RenderSettings.fogColor = fog_color;
        RenderSettings.fogDensity = 0.005f;
    }

	void Update () {
        RenderSettings.fogColor = fog_color;
        if (player.GetComponent<Player>().move_way == "FPS")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
            //sun.intensity = 0.1f;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
            //sun.intensity = 0.1f;
        }
    }
}
