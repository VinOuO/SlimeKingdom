using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMonster_info
{
    string Type
    {
        get;
    }
    bool Dectected_player
    {
        get;
    }
    void Get_attack(float _hurt, float _effect_time, string _effect);
    void Get_heal(float _heal, float _effect_time, string _effect);
    float Health_show(string _s);
}

public class Monster_normal : MonoBehaviour, IMonster_info
{
    IMonster_info monster_info;
    ISlime_info slime_info;
    protected GameObject player, UI;
    protected float camRayLength = 1000;
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
    protected float attack_damage = 5;
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
    protected float move_speed = 20;
    /// <summary>
    /// 總血量
    /// </summary>
    protected float Health = 100;
    /// <summary>
    /// <summary>
    /// 目前血量
    /// </summary>
    protected float health;
    protected float health_last_time;
    /// <summary>
    /// 受傷數字GUI
    /// </summary>
    public GameObject hurt;

    /// <summary>
    /// 耐心
    /// </summary>
    public float patient = 0;
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
    /// 是否被洗腦
    /// </summary>
    public bool is_brain_washed = false;
    /// <summary>
    /// 受到傷害加成
    /// </summary>
    protected float weak = 1;
    /// <summary>
    /// 怪物屬性
    /// </summary>
    public string type = "normal";

    public GameObject[] food_drop;

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
        if (_effect == "brain_wash")//---------------------------------狀態:洗腦
        {
            is_brain_washed = true;
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
        UI = GameObject.Find("UI");
        health = Health;
        health_last_time = health;
        time_move = Time.time;
        movestate = 1;
        spawnpoint = transform.position;
        jump_settime = 1;
        if (gameObject.name[0] == 'B')
        {
            patient = Patient;
        }
    }

    protected void Update()
    {
        hurt_show();
        if (Input.GetKeyDown(KeyCode.B) && type=="mud")
        {
            if (is_brain_washed)
            {
                is_brain_washed = false;
            }
            else
            {
                is_brain_washed = true;
                patient = 10;
            }
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
            Instantiate(food_drop[Random.Range(1, 10)], transform.position, transform.rotation);
            Instantiate(food_drop[Random.Range(1, 10)], transform.position, transform.rotation);
            Instantiate(food_drop[Random.Range(1, 10)], transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 怪物移動
    /// </summary>
    /// <param name="slime_moveatate"></param>
    /// <param name="Mextime"></param>
    protected void move_state(int slime_movestate, float Mextime)
    {
        if (can_move && !is_spelling)
        {
            switch (slime_movestate)
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
                        target = new Vector3(Random.Range(spawnpoint.x - 100, spawnpoint.x + 111), transform.position.y, Random.Range(spawnpoint.z - 100, spawnpoint.z + 111));
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
                    patient_reduce();
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
        yield return new WaitForSeconds(0.1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            StartCoroutine(rooted(_start_time, _effect_time));
        }
        else
        {
            can_move = true;
            StopCoroutine(rooted(_start_time, _effect_time));

        }
    }
    /// <summary>
    /// 洗腦
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator brain_wash(float _start_time, float _effect_time)
    {
        yield return new WaitForSeconds(0.1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            StartCoroutine(brain_wash(_start_time, _effect_time));
        }
        else
        {
            is_brain_washed = false;
            StopCoroutine(brain_wash(_start_time, _effect_time));
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
        yield return new WaitForSeconds(0.1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
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
        yield return new WaitForSeconds(0.1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
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
        yield return new WaitForSeconds(0.1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            StartCoroutine(weaken(_start_time, _effect_time, _weaken));
        }
        else
        {
            weak /= 2;
            StopCoroutine(weaken(_start_time, _effect_time, _weaken));
        }
    }

    /// <summary>
    /// 耐心減少
    /// </summary>
    protected void patient_reduce()
    {
        if (gameObject.name[0] != 'B')
        {
            if (Time.time - time_pre_sec >= 1)
            {
                time_pre_sec = Time.time;
                if (patient > 0 && !is_brain_washed)
                {
                    patient--;
                }
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
            if (!is_spelling && !is_brain_washed)
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
        int i = 0;
        if (is_brain_washed)
        {
            i = 1;
            layerMask = 1 << 8;
        }
        else
        {
            layerMask = 1 << 9;
        }
        if (is_blinded)//察覺玩家距離
        {
            radius = 10;
        }
        else
        {
            radius = 2000;
        }
        hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        if(hitColliders.Length > 0)
        {
            if (!dectected_player)
            {
                dectected_player = true;
            }
            move(hitColliders[i].gameObject.transform.position, hitColliders[i].gameObject.transform.position, move_speed);
            if (Vector3.Distance(hitColliders[i].gameObject.transform.position, transform.position) <= 25f)//可攻擊玩家距離
            {
                engage(hitColliders[i].gameObject, attack_cold_down);
            }
        }
    }
    /// <summary>
    /// 鎖定目標位置
    /// </summary>
    protected Vector3 search()
    {
        layerMask = 1 << 9;
        if (is_blinded)
        {
            radius = 10;
        }
        else
        {
            radius = 200;
        }
        hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        if(hitColliders.Length > 0)
        {
            return hitColliders[0].gameObject.transform.position;
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
            else if (_target.tag == "Enemy")
            {
                monster_info = _target.GetComponent(typeof(IMonster_info)) as IMonster_info;
                monster_info.Get_attack(attack_damage, 0, "none");
            }
        }
        patient = Patient; //耐心度重置
    }

    void hurt_show()
    {
        GameObject _hurt;
        if (health != health_last_time)
        {
            _hurt = Instantiate(hurt, UI.transform);
            _hurt.GetComponent<Hurt>().hurt_num = health - health_last_time;
            _hurt.GetComponent<Hurt>().start_pos = transform.position;
            health_last_time = health;
        }
    }
}
