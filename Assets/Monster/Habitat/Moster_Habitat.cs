using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moster_Habitat : MonoBehaviour {

    /// <summary>
    /// 怪物種類
    /// </summary>
    public GameObject[] monster_type;
    public int i;
    /// <summary>
    /// 史萊姆出生間隔
    /// </summary>
    public float time_wait;
    /// <summary>
    /// 上次生出史萊姆時間
    /// </summary>
    float time_last_spawn;
    /// <summary>
    /// 是否繼續生史萊姆
    /// </summary>
    bool stop;

    void Start()
    {
        stop = false;
        time_last_spawn = Time.time;
    }

    void Update()
    {
        spawn(time_wait);
    }

    void spawn(float time_to_spown)
    {
        if (transform.childCount < 5)
        {
            if (Time.time - time_last_spawn >= time_to_spown)
            {
                time_last_spawn = Time.time;
                Instantiate(monster_type[i], transform.position, monster_type[i].transform.rotation, transform);
            }
        }
    }
}
