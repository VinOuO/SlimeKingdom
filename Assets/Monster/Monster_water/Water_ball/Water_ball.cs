using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_ball : MonoBehaviour {

    public GameObject pool_prefab;
    Vector3 dir;

    void Start()
    {
        dir.Set(GameObject.Find("Player").transform.position.x - transform.position.x, 0, GameObject.Find("Player").transform.position.z - transform.position.z);
        dir = dir.normalized;
        dir.y = 1;
        dir = dir.normalized;
        GetComponent<Rigidbody>().AddForce(dir * 1000);
        Debug.Log(dir);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "floor")
        {
            Physics.IgnoreCollision(collision.collider, this.gameObject.GetComponent<Collider>());
        }
        else
        {
            Instantiate(pool_prefab, new Vector3(transform.position.x, 0, transform.position.z), pool_prefab.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
