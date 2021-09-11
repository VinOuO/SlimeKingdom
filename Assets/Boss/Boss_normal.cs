using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_normal : MonoBehaviour, IMonster_info
{
    ISlime_info slime_info;
    GameObject player;
    protected float camRayLength = 1000;
    protected Vector3 cam_pos;
    public GameObject spawn_minion;
    /// <summary>
    /// 怪物狀態 1:在棲地自由移動 2:追蹤目標，攻擊
    /// </summary>
    protected int movestate;
    /// <summary>
    /// 出生地
    /// </summary>
    public Vector3 spawnpoint;
    /// <summary>
    /// 停止移動
    /// </summary>
    protected bool stop_to_move = false;
    /// <summary>
    /// 移動目的地
    /// </summary>
    public Vector3 target;
    /// <summary>
    /// 移動狀態
    /// </summary>
    protected bool is_move;
    /// <summary>
    /// 移動時間
    /// </summary>
    protected float time_move;
    /// <summary>
    /// 上次攻擊時間
    /// </summary>
    protected float time_attack;
    /// <summary>
    /// 攻速
    /// </summary>
    protected float attack_cold_down = 1;
    /// <summary>
    /// 攻擊力
    /// </summary>
    protected float attack_damage = 15;
    /// <summary>
    /// 是否察覺我方
    /// </summary>
    public bool dectected_player = false;
    /// <summary>
    /// 每秒計時
    /// </summary>
    protected float time_pre_sec = 0;
    /// <summary>
    /// 跳躍間隔
    /// </summary>
    protected float time_jump;
    /// <summary>
    /// 多久跳一次
    /// </summary>
    protected float jump_settime;
    /// <summary>
    /// 移動動量
    /// </summary>
    protected Vector3 movement;
    /// <summary>
    /// 面向方向
    /// </summary>
    protected Quaternion faceRotation;
    /// <summary>
    /// 移動速度
    /// </summary>
    protected float move_speed = 5;
    /// <summary>
    /// 總血量
    /// </summary>
    protected float Health = 100;
    /// <summary>
    /// <summary>
    /// 目前血量
    /// </summary>
    protected float health = 100;
    /// <summary>
    /// 耐心
    /// </summary>
    protected float patient = 0;
    /// <summary>
    /// 最大耐心度
    /// </summary>
    protected float Patient = 15;
    /// <summary>
    /// 可以移動
    /// </summary>
    public bool can_move = true;
    /// <summary>
    /// 可以攻擊
    /// </summary>
    protected bool can_attack = true;
    /// <summary>
    /// 技能充能
    /// </summary>
    public int skill_charge = 0;
    protected float time_skill = 0;
    /// <summary>
    /// 是否在施法
    /// </summary>
    public bool is_spelling = false;
    float p_time = 0;

    protected int layerMask = 1 << 9;
    protected int radius = 20;
    protected Collider[] hitColliders;
    /// <summary>
    /// 控制效果
    /// </summary>
    protected string effected = "none";
    /// <summary>
    /// 硬控時間
    /// </summary>
    protected float time_hard_effected;
    /// <summary>
    /// 是否被致盲
    /// </summary>
    public bool is_blinded = false;
    /// <summary>
    /// 受到傷害加成
    /// </summary>
    protected float weak = 1;
    /// <summary>
    /// 怪物屬性
    /// </summary>
    protected string type = "normal";

    public bool ttt = false;
    //-----------------------------------------------interface
    public string Type
    {
        get
        {
            return type;
        }
    }
    public bool Dectected_player
    {
        get
        {
            return dectected_player;
        }
    }
    /// <summary>
    /// 被攻擊
    /// </summary>
    /// <param name="_hurt"></param>
    void IMonster_info.Get_attack(float _hurt, float _effect_time, string _effect)
    {
        health -= _hurt * weak;
        patient = Patient; //耐心度重置

        if (_effect == "knock")//-----------------------------狀態:擊飛
        {
            //--------------------------------擊飛
            transform.GetComponent<Rigidbody>().AddForce(3 * Vector3.up * Time.deltaTime);
            //--------------------------------擊飛
            StartCoroutine(rooted(Time.time, _effect_time));
        }
        if (_effect == "burn")//---------------------------------狀態:灼燒
        {
            health -= _hurt / 5;
            StartCoroutine(burned(_hurt / 5, Time.time, _effect_time));
        }
        if (_effect == "root")//---------------------------------狀態:禁錮
        {
            can_move = false;
            StartCoroutine(rooted(Time.time, _effect_time));
        }
        if (_effect == "blind")//---------------------------------狀態:失明
        {
            is_blinded = true;
            StartCoroutine(blind(Time.time, _effect_time));
        }
        if (_effect == "weak")//---------------------------------狀態:虛弱
        {
            weak *= 2;
            StartCoroutine(weaken(Time.time, _effect_time, true));
        }
        if (_effect == "slow")//---------------------------------狀態:緩速
        {
            move_speed /= 2;
            StartCoroutine(slow(Time.time, _effect_time));
        }
    }
    /// <summary>
    /// 治癒
    /// </summary>
    /// <param name="_heal"></param>
    void IMonster_info.Get_heal(float _heal, float _effect_time, string _effect)
    {
        health += _heal;
        if (health >= Health)
        {
            health = Health;
        }
    }
    /// <summary>
    /// 顯示血量
    /// </summary>
    /// <param name="_s"></param>
    /// <returns></returns>
    float IMonster_info.Health_show(string _s)
    {
        if (_s == "Max")
        {
            return Health;
        }
        else
        {
            return health;
        }
    }
    //-----------------------------------------------interface
    protected void Start()
    {
        player = GameObject.Find("Player");
        time_move = Time.time;
        movestate = 1;
        spawnpoint = transform.position;
        jump_settime = 1;
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Boss_skill_1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Boss_skill_2();
        }
        if (patient > 0)
        {
            move_state(2, 3);
        }
        else
        {
            if (dectected_player)
            {
                dectected_player = false;
            }
            move_state(1, 3);
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 怪物移動
    /// </summary>
    /// <param name="slime_moveatate"></param>
    /// <param name="Mextime"></param>
    protected void move_state(int slime_moveatate, float Mextime)
    {
        if (can_move && !is_spelling)
        {
            switch (slime_moveatate)
            {
                case 1:
                    //----------------------------------------------------------------------------------------走路
                    if (Vector3.Distance(transform.position, target) < 1f || Time.time - time_move >= Mextime)
                    {
                        time_move = Time.time;
                        is_move = false;
                    }
                    //如果移動狀態為否則設定下一個目的地
                    if (!is_move)
                    {
                        target = new Vector3(Random.Range(spawnpoint.x - 100, spawnpoint.x + 101), 0, Random.Range(spawnpoint.z - 100, spawnpoint.z + 101));
                        is_move = true;
                    }
                    move(target, target, move_speed);
                    //----------------------------------------------------------------------------------------走路
                    break;
                case 2:
                    //----------------------------------------------------------------------------------------追蹤
                    try
                    {
                        track_attack_target();
                    }
                    catch
                    {
                        move_state(1, 3);
                        if (dectected_player)
                        {
                            dectected_player = false;
                        }
                    }
                    skill_charging();
                    //----------------------------------------------------------------------------------------追蹤
                    break;
            }
        }
    }

    /// <summary>
    /// 移動到目的地
    /// </summary>
    protected void move(Vector3 _target, Vector3 _face, float _speed)
    {
        Vector3 ahead;
        if (Vector3.Distance(_target, transform.position) >= 3f)
        {
            movement.x += (_target.x - transform.position.x);
            movement.z += (_target.z - transform.position.z);

            //normalized將向量長度調整到1, 代表移動的方向
            movement = movement.normalized * _speed * Time.deltaTime;
            //利用Rigidbody的MovePosition()平順的移動物件
            this.gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + movement);

            //slime到交點的向量
            ahead = _face - transform.position;
            //slime只在x與z方向移動，故y=0
            ahead.y = 0;
            //LookRotation會讓slime從目前看的方向轉到玩家的方向
            faceRotation = Quaternion.LookRotation(ahead);
            this.gameObject.transform.GetComponent<Rigidbody>().MoveRotation(faceRotation);
        }
    }

    /// <summary>
    /// 灼燒
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator burned(float _damage, float _start_time, float _effect_time)
    {
        yield return new WaitForSeconds(1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            health -= _damage;
            StartCoroutine(burned(_damage, _start_time, _effect_time));
        }
        else
        {
            StopCoroutine(burned(_damage, _start_time, _effect_time));
        }
    }
    /// <summary>
    /// 禁錮
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator rooted(float _start_time, float _effect_time)
    {
        yield return new WaitForSeconds(1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            can_move = false;
            StartCoroutine(rooted(_start_time, _effect_time));
        }
        else
        {
            can_move = true;
            StopCoroutine(rooted(_start_time, _effect_time));

        }
    }
    /// <summary>
    /// 失明
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator blind(float _start_time, float _effect_time)
    {
        yield return new WaitForSeconds(1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            is_blinded = true;
            StartCoroutine(blind(_start_time, _effect_time));
        }
        else
        {
            is_blinded = false;
            StopCoroutine(blind(_start_time, _effect_time));
        }
    }

    /// <summary>
    /// 緩速
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator slow(float _start_time, float _effect_time)
    {
        yield return new WaitForSeconds(1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            move_speed /= 2;
            StartCoroutine(slow(_start_time, _effect_time));
        }
        else
        {
            move_speed *= 2;
            StopCoroutine(slow(_start_time, _effect_time));
        }
    }

    /// <summary>
    /// 虛弱
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator weaken(float _start_time, float _effect_time, bool _weaken)
    {
        yield return new WaitForSeconds(1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            StartCoroutine(weaken(_start_time, _effect_time, _weaken));
        }
        else
        {
            StopCoroutine(weaken(_start_time, _effect_time, _weaken));
        }
    }

    /// <summary>
    /// 耐心減少
    /// </summary>
    protected void patient_reduce()
    {
        if (Time.time - time_pre_sec >= 1)
        {
            time_pre_sec = Time.time;
            if (patient > 0)
            {
                patient--;
            }
        }
    }

    /// <summary>
    /// 技能充能
    /// </summary>
    protected void skill_charging()
    {
        if (Time.time - time_skill >= 1)
        {
            time_skill = Time.time;
            if (!is_spelling)
            {
                skill_charge++;
            }
        }
    }

    /// <summary>
    /// 追蹤玩家並攻擊
    /// </summary>
    protected void track_attack_target()
    {
        layerMask = 1 << 9;
        int i = 0;
        if (is_blinded)//察覺玩家距離
        {
            radius = 10;
        }
        else
        {
            radius = 100;
        }
        hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        while (i < hitColliders.Length)
        {
            if (!dectected_player)
            {
                dectected_player = true;
            }
            if (hitColliders[i].gameObject.name == "Player")
            {
                move(hitColliders[i].gameObject.transform.position, hitColliders[i].gameObject.transform.position, move_speed);
                break;
            }
            i++;
        }
        if (Vector3.Distance(hitColliders[0].gameObject.transform.position, transform.position) <= 4f)//可攻擊玩家距離
        {
            engage(hitColliders[0].gameObject, attack_cold_down);
        }
    }
    /// <summary>
    /// 鎖定目標位置
    /// </summary>
    protected Vector3 search()
    {
        layerMask = 1 << 9;
        int i = 0;
        if (is_blinded)
        {
            radius = 5;
        }
        else
        {
            radius = 20;
        }
        hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        while (i < hitColliders.Length)
        {
            if (player.GetComponent<Player>().is_hided)
            {
                if (hitColliders[i].gameObject.name == "Slime_throwed")
                {
                    return hitColliders[i].gameObject.transform.position;
                }
            }
            else
            {
                return hitColliders[0].gameObject.transform.position;
            }
            i++;
        }
        return new Vector3(0, 1000, 0);
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    protected void engage(GameObject _target, float _cold_down)
    {
        if (Time.time - time_attack >= _cold_down)
        {
            time_attack = Time.time;
            if (_target.tag == "PPAP")
            {
                _target.GetComponent<Player>().get_attack(attack_damage, 0, transform.position, "none");
            }
            else if (_target.tag == "Slime")
            {
                slime_info = _target.GetComponent(typeof(ISlime_info)) as ISlime_info;
                slime_info.Get_attack(attack_damage, 0, transform.position, "none");
            }
        }
        patient = Patient; //耐心度重置
    }
    /// <summary>
    /// 地震
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator earthquake(float _start_time, float _effect_time, int _times)
    {
        yield return new WaitForSeconds(0.05f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            if (player.GetComponent<Player>().move_way == "FPS")
            {
                Debug.Log("HI");
                if (_times % 4 == 0)
                {
                    Camera.main.transform.position += new Vector3(3, 0, 0);
                }
                else if (_times % 4 == 1)
                {
                    Camera.main.transform.position += new Vector3(-3, 0, 0);
                }
                else if (_times % 4 == 2)
                {
                    Camera.main.transform.position += new Vector3(0, 3, 0);
                }
                else
                {
                    Camera.main.transform.position += new Vector3(0, -3, 0);
                }
            }
            else
            {
                if (_times % 4 == 0)
                {
                    Camera.main.transform.position += new Vector3(3, 0, 0);
                }
                else if (_times % 4 == 1)
                {
                    Camera.main.transform.position += new Vector3(-3, 0, 0);
                }
                else if (_times % 4 == 2)
                {
                    Camera.main.transform.position += new Vector3(0, 0, 3);
                }
                else
                {
                    Camera.main.transform.position += new Vector3(0, 0, -3);
                }
            }
            StartCoroutine(earthquake(_start_time, _effect_time, _times + 1));
        }
        else
        {
            Camera.main.transform.position = player.transform.position + cam_pos;
            StopCoroutine(earthquake(_start_time, _effect_time, _times + 1));
        }
    }

    protected void Boss_skill_1()
    {
        cam_pos = Camera.main.transform.position - player.transform.position;
        StartCoroutine(earthquake(Time.time, 0.4f * 5, 0));
        //----------------------------------------------------------------暈眩
        layerMask = 1 << 9;
        int i = 0;
        radius = 200;
        hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.name == "Player")
            {
                hitColliders[i].gameObject.GetComponent<Player>().get_attack(5, 0.4f * 5 + 1, transform.position, "stun");
            }
            else
            {
                slime_info = hitColliders[i].gameObject.GetComponent(typeof(ISlime_info)) as ISlime_info;
                slime_info.Get_attack(5, 0.4f * 5 + 1, transform.position, "stun");
            }
            i++;
        }
        //----------------------------------------------------------------暈眩
    }

    protected void Boss_skill_2()
    {
        GameObject _minion;
        _minion = Instantiate(spawn_minion, transform.GetChild(0).transform.position, transform.rotation);
        _minion.name = "Boss"+ _minion.name;
        _minion = Instantiate(spawn_minion, transform.GetChild(1).transform.position, transform.rotation);
        _minion.name = "Boss" + _minion.name;
        _minion = Instantiate(spawn_minion, transform.GetChild(2).transform.position, transform.rotation);
        _minion.name = "Boss" + _minion.name;
        /*
        _minion = Instantiate(spawn_minion, transform.GetChild(3).transform.position, transform.rotation);
        _minion.name = "Boss" + _minion.name;
        _minion = Instantiate(spawn_minion, transform.GetChild(4).transform.position, transform.rotation);
        _minion.name = "Boss" + _minion.name;
        _minion = Instantiate(spawn_minion, transform.GetChild(5).transform.position, transform.rotation);
        _minion.name = "Boss" + _minion.name;
        _minion = Instantiate(spawn_minion, transform.GetChild(6).transform.position, transform.rotation);
        _minion.name = "Boss" + _minion.name;
        */
    }

}
