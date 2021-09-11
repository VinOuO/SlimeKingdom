using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_lava_skill_test : MonoBehaviour {

    public GameObject fire_ball, fire_ball_lava_boss;
    Vector3 fire_ball_fall_pos;
	void Start () {
		
	}
	
	void Update () {

	}

    public void fire_ball_sum()
    {
        fire_ball_lava_boss = Instantiate(fire_ball, this.transform);
        fire_ball_fall_pos.Set(Random.Range(-Screen.width / 2 - 1, Screen.width / 2), Random.Range(Screen.height / 2 + 1, -Screen.height / 2), 0);
        fire_ball_lava_boss.GetComponent<Fire_ball>().fall_pos = fire_ball_fall_pos;
    }
}
