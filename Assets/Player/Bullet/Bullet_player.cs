using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_player : MonoBehaviour {
    IMonster_info monster_info;
    LayerMask a;
    float start_time;
    public Vector3 way;

	void Start () {
        a = 1 << 8;
        start_time = Time.time;
    }

	void Update () {
        if (Time.time - start_time >= 10)
        {
            Destroy(this.gameObject);
        }
        this.gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + way);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "floor"|| other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.tag == "Enemy")
            {
                monster_info = other.gameObject.GetComponent(typeof(IMonster_info)) as IMonster_info;
                monster_info.Get_attack(2, 0, "none");
            }
            Destroy(this.gameObject);
        }
    }
}
