using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mini_game_rooted : MonoBehaviour {
    GameObject player;
	void Start () {
        player = GameObject.Find("Player");
	}
	
	void Update () {
        if (player.GetComponent<Player>().is_minigame_root)
        {
            if (player.GetComponent<Player>().last_time_hit == "x")
            {
                this.GetComponent<Text>().text = "Hit Z to get ride of rooting!!! \r\n" + (20 - player.GetComponent<Player>().zx_time) + "to go!!!";
            }
            else
            {
                this.GetComponent<Text>().text = "Hit X to get ride of rooting!!! \r\n" + (20 - player.GetComponent<Player>().zx_time) + "to go!!!";
            }
        }
        else
        {
            this.GetComponent<Text>().text = "";
        }
	}
}
