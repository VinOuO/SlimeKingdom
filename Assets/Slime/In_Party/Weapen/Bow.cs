using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    IMonster_info monster_info;
    Vector3 movement;
    int layerMask = 1 << 8;
    Collider[] hitColliders;
    float radius;
    int i;
    void Start()
    {
        i = 0;
        radius = 20;
        hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        if (hitColliders.Length == 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.tag == "Enemy")
                {
                    move(hitColliders[i].gameObject.transform.position, hitColliders[i].gameObject.transform.position, 6);
                    break;
                }
                i++;
            }
        }
    }

    void Update()
    {

        try
        {
            move(hitColliders[i].gameObject.transform.position, hitColliders[i].gameObject.transform.position, 6);
        }
        catch
        {
            Destroy(this.gameObject);
        }

    }

    void move(Vector3 _target, Vector3 _face, float _speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            monster_info = hitColliders[i].gameObject.GetComponent(typeof(IMonster_info)) as IMonster_info;
            monster_info.Get_attack(1, 0,"none");
            Destroy(this.gameObject);
        }
    }
}
