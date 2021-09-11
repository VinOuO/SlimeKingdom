using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

public class Shadow : MonoBehaviour
{
    public bool[] charge_state = new bool[10];//0.勢不可擋 1.天火燎原 2.御風而行 3.治癒 4.加攻速
    public bool is_charge = false;
    public float charge_power, charge_time;
    /// <summary>
    /// Nav
    /// </summary>
    NavMeshAgent slime_nav;
    //-----------------------------------------------animator
    protected Animator anim;
    public int slime_anim_state = 1;
    public float time_is_move;
    protected Vector3 move_b, move_a;
    //-----------------------------------------------animator
    /// <summary>
    /// 玩家
    /// </summary>
    GameObject player;
    //-----------------------------------------------interface
    public bool this[int index]
    {
        get
        {
            return charge_state[index];
        }
        set
        {
            charge_state[index] = value;
        }
    }
    //-----------------------------------------------interface
    protected void Start()
    {
        //---------------------------------
        slime_nav = GetComponent<NavMeshAgent>();
        //---------------------------------
        player = GameObject.Find("Player");
        //-----------------------------------------------animator
        move_b = transform.position;
        time_is_move = Time.time;
        anim = transform.GetChild(2).GetChild(0).GetComponent<Animator>();
        //-----------------------------------------------animator
        //StartCoroutine(throwed(Time.time, 10, 5, 5, Vector3.left));
    }

    protected void Update()
    {
        //-----------------------------------------------animator
        Is_moving();
        anim_state();
        //-----------------------------------------------animator
    }
    //-----------------------------------------------animator
    /// <summary>
    /// 史萊姆是否在移動
    /// </summary>
    /// <returns></returns>
    protected void Is_moving()
    {

        move_a = transform.position;
        if (Time.time - time_is_move >= 0.1f)
        {
            time_is_move = Time.time;
            if (Vector3.Distance(move_a, move_b) != 0)
            {
                slime_anim_state = 2;
            }
            else
            {
                slime_anim_state = 0;
            }
            move_b = transform.position;
        }
    }
    /// <summary>
    /// 動畫
    /// </summary>
    protected void anim_state()
    {
        anim.SetFloat("Slime_anim_state", slime_anim_state);
    }
    //-----------------------------------------------animator

    Vector3 bounce(Vector3 _vector3_in, Vector3 _normal)
    {
        //Debug.DrawRay(hit.position, hit.normal * 10, Color.red);
        Vector3 _vector3_out;
        float _in_length, _normal_length;
        float _angle;
        _in_length = Vector3.Distance(Vector3.zero, _vector3_in);
        _normal_length = Vector3.Distance(Vector3.zero, _normal);
        _angle = Mathf.Acos((_vector3_in.x * _normal.x + _vector3_in.y * _normal.y + _vector3_in.z * _normal.z) / (_in_length * _normal_length)) * 180 / Mathf.PI;
        _angle *= 2;
        _angle *= -1;
        _vector3_out.x = _vector3_in.x * Mathf.Cos(_angle / 180 * Mathf.PI) - _vector3_in.z * Mathf.Sin(_angle / 180 * Mathf.PI);
        _vector3_out.y = 0;
        _vector3_out.z = _vector3_in.x * Mathf.Sin(_angle / 180 * Mathf.PI) + _vector3_in.z * Mathf.Cos(_angle / 180 * Mathf.PI);
        return _vector3_out.normalized * _in_length;
    }

    //---------------------------------------------------------------------------------buff
    /// <summary>
    /// 被丟出去
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    public IEnumerator throwed(float _start_time, float _effect_time, float _Power, float _power, Vector3 _throwement)
    {
        yield return new WaitForSeconds(0.01f);
        Vector3 _pos_b;
        NavMeshHit _hit;
        //charge_time = _effect_time;
        //charge_power = _power;
        if (NavMesh.FindClosestEdge(transform.position, out _hit, NavMesh.AllAreas))
        {
            Debug.DrawRay(_hit.position, _hit.normal * 10, Color.red);
            if (_hit.distance <= 0.1f)
            {
                _throwement = bounce(_throwement, _hit.normal);
            }
        }
        if (charge_state[1])
        {
            Debug.Log("天火燎原");
        }
        if (charge_state[2])
        {
            Debug.Log("御風而行");
            charge_state[2] = false;
            _effect_time += 5;
        }
        if (Time.time - _start_time - _effect_time <= 0)
        {
            _pos_b = transform.position;
            _power = _Power * (_effect_time - (Time.time - _start_time)) / _effect_time;
            slime_nav.Move(_throwement.normalized * _power * 0.5f);
            StartCoroutine(throwed(_start_time, _effect_time, _Power, _power, _throwement));
        }
        else
        {
            for (int i = 0; i <= 9; i++)
            {
                //charge_state[i] = false;
            }
            is_charge = false;
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            StopCoroutine(throwed(_start_time, _effect_time, _Power, _power, _throwement));
            Destroy(this.gameObject);
        }
    }
    //--------------------------------------------------------------------------------buff
}
