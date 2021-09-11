using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partical_water_2 : MonoBehaviour
{
    Vector3 change = new Vector3(0, 0, 0);
    public float dis_obj;
    float time;
    public Vector3 move_target;

    void Start()
    {
        dis_obj = 4;
        time = 0;
        move_target = target();
    }

    void Update()
    {
        if (Time.time - time >= 0.3f)
        {
            time = Time.time;
            move_target = target();
        }
        transform.position = Vector3.MoveTowards(transform.position, move_target, 10f * Time.deltaTime);
    }

    Vector3 target()
    {
        Vector3 _target = new Vector3(0,0,0);
        _target.Set(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
        _target = _target.normalized * dis_obj;
        _target += this.transform.parent.transform.position;
        while (_target.y <= this.transform.parent.transform.position.y - 1f || Vector3.Distance(transform.position, _target) <= 4f)
        {
            _target.Set(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
            _target = _target.normalized * dis_obj;
            _target += this.transform.parent.transform.position;
        }
        return _target;
    }

    Vector3 adj_move()
    {
        Vector3 adj_move = new Vector3(0, 0, 0);
        if (Vector3.Distance(this.transform.position, this.transform.parent.transform.position) > dis_obj + 0.1f)
        {
            adj_move = this.transform.parent.transform.position - this.transform.position;
        }
        else if (Vector3.Distance(this.transform.position, this.transform.parent.transform.position) < dis_obj - 0.1f)
        {
            adj_move = this.transform.position - this.transform.parent.transform.position;
        }
        adj_move = adj_move.normalized;
        return adj_move;
    }
}
