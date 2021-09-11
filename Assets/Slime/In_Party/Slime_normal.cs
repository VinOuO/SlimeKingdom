using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

interface ISlime_info
{
    float Time_throwed
    {
        get;
        set;
    }
    string Type
    {
        get;
    }
    float Step_pit_time
    {
        get;
    }
    int Num_in_party
    {
        get;
        set;
    }
    int Form
    {
        get;
        set;
    }
    bool Hit
    {
        get;
        set;
    }
    bool Can_move
    {
        get;
        set;
    }
    Vector3 Throwment
    {
        get;
        set;
    }
    bool Is_charge
    {
        get;
        set;
    }
    bool this[int index]
    {
        get;
        set;
    }
    void Get_attack(float _hurt, float _effect_time,Vector3 _enemy_position, string _effect);
    void Get_heal(float _heal, float _effect_time, string _effect);
    void Throwed(Vector3 _throwement, float _effect_time, float _power);
    float Health_show(string _s);
}

public class Slime_normal : MonoBehaviour, ISlime_info
{
    /// <summary>
    /// 遊戲資訊
    /// </summary>
    protected GameObject combo;
    protected GameObject broadcast;
    public string nickname;
    public bool[] charge_state = new bool[10];//0.勢不可擋 1.天火燎原 2.御風而行 3.治癒 4.神聖詠唱 5.影子分身 6.死亡之徑
    /// <summary>
    /// 是否在衝撞
    /// </summary>
    public bool is_charge = false;
    public float charge_power, charge_time;
    public bool spelled = true;
    protected float combo_damage = 1;
    protected float charge_damage = 10;
    public GameObject shadow, dark_slime_shadow;
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
    public bool aa;
    public Vector3 throwment;
    /// <summary>
    /// 子彈
    /// </summary>
    public GameObject bow;
    /// <summary>
    /// 上次射擊時間
    /// </summary>
    protected float time_shoot;
    /// 射擊冷卻(計算用)
    /// </summary>
    protected float attack_cold_down = 2;
    /// <summary>
    /// 跟隨對象
    /// </summary>
    protected Transform slime_ahead;
    /// <summary>
    /// 移動速度
    /// </summary>
    protected float speed = 45;
    public bool is_moving = false;
    /// <summary>
    /// 在隊伍中的位置
    /// </summary>
    public int num_in_party;
    public string type = "normal";
    /// <summary>
    /// 衛星隊形中的切線方向
    /// </summary>
    protected Quaternion slime_circle;
    /// <summary>
    /// 衛星隊形中的軌道半徑
    /// </summary>
    public float r_circle = 6;
    /// <summary>
    /// 衛星隊形中屬於哪一圈
    /// </summary>
    protected int c;
    /// <summary>
    /// 衛星隊形每圈史萊姆數
    /// </summary>
    protected int c1 = 6, c2 = 15, c3 = 30;
    protected float rr_circle = 6;
    //是否要讓路或超車
    protected bool r_circle_change = false;
    /// <summary>
    /// 在對的位置的時間
    /// </summary>
    protected float circle_time;
    /// <summary>
    /// 在對的位置的次數
    /// </summary>
    protected int right_circle_num = 0;
    /// <summary>
    /// 是否繞道正確的位置
    /// </summary>
    public bool right_circle_position = false;
    /// <summary>
    /// 衛星隊形中的速度
    /// </summary>
    protected int speed_circle = 70;
    /// <summary>
    /// 脫隊時間
    /// </summary>
    protected float outOfline_time = 0;
    /// <summary>
    /// 是否開始計算脫隊
    /// </summary>
    protected bool start_timer = false;
    /// <summary>
    /// 目前的狀態
    /// </summary>
    public int form = 1;
    /// <summary>
    /// 動量
    /// </summary>
    public Vector3 movement;
    /// <summary>
    /// 動量(keep distance)
    /// </summary>
    protected Vector3 movement_keep_distance;
    /// <summary>
    /// 史萊姆面對方向
    /// </summary>
    protected Quaternion faceRotation;
    /// <summary>
    /// 在隊伍中的史萊姆的layerMask
    /// </summary>
    protected int layerMask = 1 << 9;
    /// <summary>
    /// 與自己太近的Teammate
    /// </summary>
    protected Collider[] hitColliders;
    /// <summary>
    /// 總血量
    /// </summary>
    protected float Health = 100;
    /// <summary>
    /// 目前血量
    /// </summary>
    public float health = 100;
    //int i = 0;
    /// <summary>
    /// 玩家把史萊姆丟出去時用來判斷是否落地
    /// </summary>
    public bool hit = false;
    /// <summary>
    /// 史萊姆被玩家丟出去時的計時
    /// </summary>
    protected float time_throwed = 0;
    /// <summary>
    /// 是否加過攻速了
    /// </summary>
    protected bool is_attackspeeded = false;
    /// <summary>
    /// 是否被硬控
    /// </summary>
    public bool can_move = true;
    int can_move_count = 0;
    /// <summary>
    /// 被位移型控制的種類
    /// </summary>
    protected string position_control_type = "none";
    /// <summary>
    /// 被位移方向
    /// </summary>
    protected Vector3 position_control_movement;
    /// <summary>
    /// 是否被致盲
    /// </summary>
    protected bool is_blinded = false;
    /// <summary>
    /// 踩到泥坑的時間
    /// </summary>
    public float step_pit_time = 0;
    /// <summary>
    /// 是否偽裝了
    /// </summary>
    protected bool is_hided = false;
    /// <summary>
    /// 是否滑倒
    /// </summary>
    public bool is_sliped = false;
    /// <summary>
    /// 是否被暈眩
    /// </summary>
    public bool is_stuned = false;
    /// <summary>
    /// 踏了幾次水
    /// </summary>
    public int step_pool = 0;
    /// <summary>
    /// 玩家
    /// </summary>
    protected GameObject player;
    //-----------------------------------------------interface
    public float Time_throwed
    {
        get
        {
            return time_throwed;
        }
        set
        {
            time_throwed = value;
        }
    }
    public string Type
    {
        get
        {
            return type;
        }
    }
    public float Step_pit_time
    {
        get
        {
            return Step_pit_time;
        }
        set
        {
            step_pit_time = value;
        }
    }
    public bool Hit
    {
        get
        {
            return hit;
        }
        set
        {
            hit = value;
        }
    }
    public int Num_in_party
    {
        get
        {
            return num_in_party;
        }
        set
        {
            num_in_party = value;
        }
    }
    public int Form
    {
        get
        {
            return form;
        }
        set
        {
            form = value;
        }
    }
    public bool Can_move
    {
        get
        {
            return can_move;
        }
        set
        {
            can_move = value;
        }
    }
    public Vector3 Throwment
    {
        get
        {
            return throwment;
        }
        set
        {
            throwment = value;
        }
    }
    public bool Is_charge
    {
        get
        {
            return is_charge;
        }
        set
        {
            is_charge = value;
        }
    }
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
    /// <summary>
    /// 被攻擊
    /// </summary>
    /// <param name="_hurt"></param>
    void ISlime_info.Get_attack(float _hurt, float _effect_time,Vector3 _enemy_position, string _effect)
    {
        health -= _hurt;

        if (_effect == "knock")//-----------------------------狀態:撞擊
        {
            can_move_count --;
            position_control_type = "knock";
            position_control_movement = this.transform.position - _enemy_position;
            position_control_movement.y = 0;
            position_control_movement = position_control_movement.normalized;
            movement.Set(0, 0, 0);
            StartCoroutine(knocked(Time.time, _effect_time));
            StartCoroutine(rooted(Time.time, _effect_time + 1));
        }
        if (_effect == "slip_in")//-----------------------------狀態:滑倒_in
        {
            step_pool++;
            if (step_pool == 1)
            {
                can_move_count--;
                is_sliped = true;
                position_control_type = "slip";
                position_control_movement = movement;
            }
        }
        if (_effect == "slip_out")//-----------------------------狀態:滑倒_out
        {
            step_pool--;
            if (step_pool == 0)
            {
                can_move_count++;
                is_sliped = false;
            }
        }
        if (_effect == "burn")//---------------------------------狀態:灼燒
        {
            health -= _hurt / 5;
            StartCoroutine(burned(_hurt / 5, Time.time, _effect_time));
        }
        if (_effect == "root")//---------------------------------狀態:禁錮
        {
            can_move_count--;
            StartCoroutine(rooted(Time.time, _effect_time));
        }
        if (_effect == "blind")//---------------------------------狀態:失明
        {
            is_blinded = true;
            StartCoroutine(blind(Time.time, _effect_time));
        }
        if (_effect == "slow")//---------------------------------狀態:緩速
        {
            speed /= 2;
            StartCoroutine(slow(Time.time, _effect_time));
        }
        if (_effect == "stun")//---------------------------------狀態:暈眩
        {
            is_stuned = true;
            can_move_count--;
            StartCoroutine(stuned(Time.time, _effect_time));
        }
    }
    /// <summary>
    /// 治癒
    /// </summary>
    /// <param name="_heal"></param>
    void ISlime_info.Get_heal(float _heal, float _effect_time, string _effect)
    {
        health += _heal;
        if (health >= Health)
        {
            health = Health;
        }

        if (_effect == "buff_attackspeed")//---------------------------------狀態:加攻速
        {
            attack_cold_down /= 2;
            StartCoroutine(buff_attackspeed(Time.time, _effect_time, true));
        }
    }
    /// <summary>
    /// 顯示血量
    /// </summary>
    /// <param name="_s"></param>
    /// <returns></returns>
    float ISlime_info.Health_show(string _s)
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
    /// <summary>
    /// 被丟出去
    /// </summary>
    void ISlime_info.Throwed(Vector3 _throwement,float _effect_time,float _power)
    {
        /*
        GetComponent<NavMeshAgent>().updatePosition = false;
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        GetComponent<NavMeshAgent>().nextPosition = transform.position;
        GetComponent<NavMeshAgent>().updatePosition = true;
        */
        GetComponent<NavMeshAgent>().Warp(player.transform.position);
        spelled = false;
        combo_damage = 1 + 0.1f * combo.GetComponent<Combo>().num_last;
        StartCoroutine(throwed(Time.time, _effect_time, _power, _power, _throwement));
    }
    //-----------------------------------------------interface
    protected void Start()
    {
        //---------------------------------
        slime_nav = GetComponent<NavMeshAgent>();
        //---------------------------------
        combo = GameObject.Find("Combo");
        broadcast = GameObject.Find("Broadcast");
        player = GameObject.Find("Player");
        num_in_party = player.GetComponent<Player>().slime_partied_num;
        get_Line();
        time_shoot = Time.time;
        DontDestroyOnLoad(this.gameObject);
        form = player.GetComponent<Player>().form_type;
        //-----------------------------------------------animator
        move_b = transform.position;
        time_is_move = Time.time;
        if (type != "mud")
            anim = transform.GetChild(2).GetChild(0).GetComponent<Animator>();
        //-----------------------------------------------animator
        nickname = type + "Slime";
    }

    protected void Update()
    {
        //-----------------------------------------------animator
        Is_moving();
        if (type != "mud")
            anim_state();
        //-----------------------------------------------animator
        if (can_move_count >= 0)
        {
            can_move = true;
        }
        else
        {
            can_move = false;
        }
        if (can_move)
        {
            if (form != 4)
            {
                get_Line();
                movement.Set(0, 0, 0);
                movement += keep_distance_move();
                party_form(player.GetComponent<Player>().form_type);
            }
            if (player.GetComponent<Player>().engaged && !is_charge)
            {
                engage();
            }
        }
        else
        {
            position_control(position_control_movement, transform.position, 1f, position_control_type);
            //------------------------------暈眩
            if (is_stuned)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 720);
            }
            //------------------------------暈眩
        }
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
    protected void party_form(int _form_type)
    {
        switch (_form_type)
        {
            //排隊
            case 1:
                try
                {
                    if ((num_in_party - 1) == 0)
                    {
                        fallow(GameObject.FindGameObjectsWithTag("PPAP")[0].transform.GetChild(0).transform.position, GameObject.FindGameObjectsWithTag("PPAP")[0].transform.GetChild(1).transform.position);
                    }
                    else
                    {
                        fallow(GameObject.Find("Slime_" + (num_in_party - 1)).transform.GetChild(0).transform.position, GameObject.Find("Slime_" + (num_in_party - 1)).transform.GetChild(1).transform.position);
                    }

                    form = 1;
                }
                catch
                {

                }
                break;
            //衛星
            case 2:
                form_2();
                transform.LookAt(player.transform);
                if (c % 2 == 0)
                {
                    transform.Rotate(Vector3.up, 90);
                }
                else
                {
                    transform.Rotate(Vector3.up, -90);
                }
                if (player.GetComponent<Player>().is_moving && player.GetComponent<Player>().can_move)
                {
                    slime_nav.Move(player.GetComponent<Player>().movement);
                    Debug.Log(player.GetComponent<Player>().movement);
                }
                form = 2;

                break;
            case 3:
                form_3();
                form = 3;
                break;
                /*
            case 4:
                form = 4;
                break;
                */
        }
    }

    /// <summary>
    /// 跟隨目標
    /// </summary>
    protected void fallow(Vector3 anchor_position, Vector3 anchor_ahead)
    {
        if (Vector3.Distance(transform.position, anchor_position) > 9f)
        {
            move(anchor_position, anchor_ahead, speed * Vector3.Distance(transform.position, anchor_position) * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, anchor_position) < 15f)
        {
            speed = 45;
        }
    }

    /// <summary>
    /// 排隊並命名史萊姆
    /// </summary>
    protected void get_Line()
    {
        if (this.gameObject.name != "Slime_throwed")
        {
            while (GameObject.Find("Slime_" + (num_in_party - 1)) == null && num_in_party > 1)
            {
                num_in_party--;
            }
        }
        else
        {
            num_in_party++;
            while (GameObject.Find("Slime_" + num_in_party) != null)
            {
                num_in_party++;
            }
        }
        this.gameObject.name = "Slime_" + num_in_party;
    }
    /// <summary>
    /// form3
    /// </summary>
    protected void form_3()
    {
        Vector3 _target;
        _target = player.transform.position + -6 * player.transform.forward.normalized;
        _target.y = 0;
        if (Vector3.Distance(this.transform.position, _target) >= player.GetComponent<Player>().slime_partied_num * 0.7f)
        {
            move(_target, _target, speed + 0.8f * Vector3.Distance(this.transform.position, _target));
        }
    }
    /// <summary>
    /// 移動到目的地
    /// </summary>
    protected void move(Vector3 _target, Vector3 _face, float _speed)
    {
        Vector3 ahead;
        if (Vector3.Distance(_target, transform.position) >= 0.5f)
        {
            is_moving = true;
            movement.x += (_target.x - transform.position.x);
            movement.y += (_target.y - transform.position.y);
            movement.z += (_target.z - transform.position.z);

            //normalized將向量長度調整到1, 代表移動的方向
            movement = movement.normalized * _speed * Time.deltaTime;
            slime_nav.Move(movement);
            //利用Rigidbody的MovePosition()平順的移動物件
            //this.gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + movement);

            //slime到交點的向量
            ahead = _face - transform.position;
            //slime只在x與z方向移動，故y=0
            ahead.y = 0;
            //LookRotation會讓slime從目前看的方向轉到玩家的方向
            faceRotation = Quaternion.LookRotation(ahead);
            this.gameObject.transform.GetComponent<Rigidbody>().MoveRotation(faceRotation);
        }
        else
        {
            is_moving = false;
        }
    }

    /// <summary>
    /// 與其他史萊姆保持距離
    /// </summary>
    protected Vector3 keep_distance_move()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 2, layerMask);
        int i = 1;
        movement_keep_distance.Set(0, 0, 0);
        while (i < hitColliders.Length)
        {
            movement_keep_distance += (transform.position - hitColliders[i].transform.position).normalized;
            i++;
        }
        movement_keep_distance.y = 0;
        return 2 * movement_keep_distance;
        //this.gameObject.transform.GetComponent<Rigidbody>().AddForce(movement_keep_distance.normalized * Time.time * 0.01f);
    }

    /// <summary>
    /// 遠離某點
    /// </summary>
    protected void get_away(Vector3 _get_away_from, float _distance, float _speed, bool _rotate)
    {
        Vector3 ahead;
        if (Vector3.Distance(_get_away_from, transform.position) < _distance)
        {
            movement.x += (transform.position.x - _get_away_from.x);
            movement.z += (transform.position.z - _get_away_from.z);
            //normalized將向量長度調整到1, 代表移動的方向
            movement = movement.normalized * _speed * Time.deltaTime;
            slime_nav.Move(movement);
            //利用Rigidbody的MovePosition()平順的移動物件
            //this.gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + movement);
            if (_rotate)
            {
                //slime到交點的向量
                ahead = transform.position - _get_away_from;
                //slime只在x與z方向移動，故y=0
                ahead.y = 0;
                //LookRotation會讓slime從目前看的方向轉到玩家的方向
                faceRotation = Quaternion.LookRotation(ahead);
                this.gameObject.transform.GetComponent<Rigidbody>().MoveRotation(faceRotation);
            }
        }
    }
    /// <summary>
    /// 進攻狀態
    /// </summary>
    protected void engage()
    {
        if (Time.time - time_shoot > attack_cold_down)
        {
            //float radius = 2000;
            //layerMask = 1 << 8;
            //Collider[] hitColliders_enemy = Physics.OverlapSphere(transform.position, radius, layerMask);
            Instantiate(bow, transform.position + 2.5f * transform.forward.normalized, transform.rotation);

            time_shoot = Time.time;
        }
    }

    Vector3 bounce(Vector3 _vector3_in,Vector3 _normal)
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

    /// <summary>
    /// 位移型控制
    /// </summary>
    /// <param name="_movement"></param>
    /// <param name="_target"></param>
    /// <param name="_speed"></param>
    /// <param name="_control_type"></param>
    void position_control(Vector3 _movement, Vector3 _target, float _speed, string _control_type)
    {
        switch (_control_type)
        {
            case "knock":
                slime_nav.Move(_movement * _speed);
                //GetComponent<Rigidbody>().MovePosition(this.transform.position + _movement * _speed);
                break;
            case "slip":
                slime_nav.Move(_movement * _speed);
                //GetComponent<Rigidbody>().MovePosition(this.transform.position + _movement * _speed);
                break;
        }

    }
    //---------------------------------------------------------------------------------buff
    /// <summary>
    /// 被丟出去
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator throwed(float _start_time, float _effect_time, float _Power, float _power, Vector3 _throwement)
    {
        yield return new WaitForSeconds(0.01f);
        NavMeshHit _hit;
        //charge_time = _effect_time;
        //charge_power = _power;
        if (NavMesh.FindClosestEdge(transform.position, out _hit, NavMesh.AllAreas))
        {
            Debug.DrawRay(_hit.position, _hit.normal * 10, Color.red);
            if (_hit.distance <= 0.1f)
            {
                _throwement = bounce(_throwement,_hit.normal);
            }
        }
        if (charge_state[1])
        {
            broadcast.GetComponent<Broadcast>().message_update(nickname + " 天火燎原");
            charge_state[1] = false;
            Debug.Log("天火燎原");
        }
        if (charge_state[2])
        {
            broadcast.GetComponent<Broadcast>().message_update(nickname + " 御風而行");
            Debug.Log("御風而行");
            charge_state[2] = false;
            _effect_time += 5;
        }
        if (charge_state[3])
        {
            broadcast.GetComponent<Broadcast>().message_update(nickname + " 治癒");
            Debug.Log("治癒");
            health += Health * 0.1f;
            charge_state[3] = false;
        }
        if (charge_state[5])
        {
            broadcast.GetComponent<Broadcast>().message_update(nickname + " 影分身");
            Debug.Log("影分身");
            shadow = Instantiate(dark_slime_shadow, transform.position, dark_slime_shadow.transform.rotation);
            shadow.name = "Shadow";
            StartCoroutine(shadow.GetComponent<Shadow>().throwed(_start_time, _effect_time, _Power, _Power * (_effect_time - (Time.time - _start_time)) / _effect_time, new Vector3(UnityEngine.Random.Range(-100, 101), 0, UnityEngine.Random.Range(-100, 101))));
            shadow = Instantiate(dark_slime_shadow, transform.position, dark_slime_shadow.transform.rotation);
            shadow.name = "Shadow";
            StartCoroutine(shadow.GetComponent<Shadow>().throwed(_start_time, _effect_time, _Power, _Power * (_effect_time - (Time.time - _start_time)) / _effect_time, new Vector3(UnityEngine.Random.Range(-100, 101), 0, UnityEngine.Random.Range(-100, 101))));
            _throwement = new Vector3(UnityEngine.Random.Range(-100, 101), 0, UnityEngine.Random.Range(-100, 101));
            charge_state[5] = false;
        }
        if (Time.time - _start_time - _effect_time <= 0)
        {
            _power = _Power * (_effect_time- (Time.time - _start_time))/_effect_time;
            slime_nav.Move(_throwement.normalized * _power * 0.5f);
            //LookRotation會讓slime從目前看的方向轉到玩家的方向
            faceRotation = Quaternion.LookRotation(_throwement.normalized);
            this.gameObject.transform.GetComponent<Rigidbody>().MoveRotation(faceRotation);
            StartCoroutine(throwed(_start_time, _effect_time, _Power, _power, _throwement));
        }
        else
        {
            if (charge_state[4])
            {
                broadcast.GetComponent<Broadcast>().message_update(nickname + " 神聖詠唱");
                Debug.Log("神聖詠唱");
                attack_cold_down /= 2;
                StartCoroutine(buff_attackspeed(Time.time, 10, true));
                charge_state[4] = false;
            }
            for (int i = 0; i <= 9; i++)
            {
                charge_state[i] = false;
            }
            is_charge = false;
            spelled = true;
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            player.GetComponent<Player>().can_shoot = true;
            combo.GetComponent<Combo>().counting_down();
            StopCoroutine(throwed(_start_time, _effect_time, _Power, _power, _throwement));
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
            StartCoroutine(rooted(_start_time, _effect_time));
        }
        else
        {
            can_move_count++;
            StopCoroutine(rooted(_start_time, _effect_time));
        }
    }
    /// <summary>
    /// 撞擊
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator knocked(float _start_time, float _effect_time)
    {
        yield return new WaitForSeconds(0.01f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            StartCoroutine(knocked(_start_time, _effect_time));
        }
        else
        {
            position_control_movement.Set(0, 0, 0);
            StopCoroutine(knocked(_start_time, _effect_time));
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
            speed /= 2;
            StartCoroutine(slow(_start_time, _effect_time));
        }
        else
        {
            speed *= 2;
            StopCoroutine(slow(_start_time, _effect_time));
        }
    }
    /// <summary>
    /// 暈眩
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator stuned(float _start_time, float _effect_time)
    {
        yield return new WaitForSeconds(0.01f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            StartCoroutine(stuned(_start_time, _effect_time));
        }
        else
        {
            is_stuned = false;
            can_move_count++;
            StopCoroutine(stuned(_start_time, _effect_time));
        }
    }
    /// <summary>
    /// 加攻速
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_effect_time"></param>
    /// <returns></returns>
    protected IEnumerator buff_attackspeed(float _start_time, float _effect_time, bool _speeded)
    {
        yield return new WaitForSeconds(1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            StartCoroutine(buff_attackspeed(_start_time, _effect_time, _speeded));
        }
        else
        {
            attack_cold_down *= 2;
            StopCoroutine(slow(_start_time, _effect_time));
        }
    }
    //--------------------------------------------------------------------------------buff

    /// <summary>
    /// form2_取得繞圈速度
    /// </summary>
    protected void get_CircleSpeed(float _r_circle, int slime_pre_circle, int pn)
    {
        GameObject slime_ahead;
        Vector3 a, b;
        double angle_to_be, angle_now;

        if (num_in_party != 1 && num_in_party != c1 + 1 && num_in_party != c2 + 1)
        {
            slime_ahead = GameObject.Find("Slime_" + (num_in_party - 1));
            a = this.transform.position - player.transform.position;
            a.y = 0;

            b = slime_ahead.transform.position - player.transform.position;
            b.y = 0;

            a.Normalize();
            b.Normalize();
            double theta = Math.Acos(Vector3.Dot(a, b));
            angle_now = theta * 180 / Math.PI;

            angle_to_be = 360 / slime_pre_circle;

            if (slime_pre_circle != 2)
            {
                if (angle_now - angle_to_be > 1.5f)
                {
                    speed_circle++;
                }
                else if (angle_now - angle_to_be < -1.5f)
                {
                    if (slime_pre_circle != 2)
                    {
                        speed_circle--;
                    }
                }
                else
                {
                    if (Time.time - circle_time > 0.3)
                    {
                        right_circle_num = 0;
                    }
                    right_circle_num++;
                    speed_circle = 60;
                    circle_time = Time.time;
                }
            }
            else
            {
                if (angle_now - angle_to_be > 1.5f || angle_now - angle_to_be < -1.5f)
                {
                    speed_circle++;
                }
                else
                {
                    if (Time.time - circle_time > 0.3)
                    {
                        right_circle_num = 0;
                    }
                    right_circle_num++;
                    speed_circle = 30;
                    circle_time = Time.time;
                }
            }
        }
        else
        {
            right_circle_position = true;
        }

    }



    /// <summary>
    /// form_2
    /// </summary>
    protected void form_2()
    {
        if (num_in_party <= c1)
        {
            c = 1;
        }
        else if (num_in_party <= c2)
        {
            c = 2;
        }
        else
        {
            c = 3;
        }
        if (right_circle_num >= 50)
        {
            right_circle_position = true;
        }

        if (c == 1)//第一圈
        {
            r_circle = 10;
            if (player.GetComponent<Player>().slime_partied_num < c1)
            {
                get_CircleSpeed(r_circle, player.GetComponent<Player>().slime_partied_num, 1);
            }
            else
            {
                get_CircleSpeed(r_circle, c1, 1);
            }

            player.GetComponent<Player>().player_to_slime1 = this.gameObject.transform.position - player.transform.position;
            //繞在規定的軌道
            if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) <= r_circle)
            {
                transform.position += player.GetComponent<Player>().player_to_slime1 * Time.deltaTime;
            }
            else if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) >= r_circle + 1f)
            {
                transform.position -= player.GetComponent<Player>().player_to_slime1 * Time.deltaTime;
            }
            transform.RotateAround(player.transform.position, Vector3.up, speed_circle * Time.deltaTime);
        }
        else if (c == 2)//第二圈
        {
            r_circle = 20;
            if (player.GetComponent<Player>().slime_partied_num < c2)
            {
                get_CircleSpeed(r_circle, player.GetComponent<Player>().slime_partied_num - c1, -1);
            }
            else
            {
                get_CircleSpeed(r_circle, c2 - c1, -1);
            }

            player.GetComponent<Player>().player_to_slime1 = this.gameObject.transform.position - player.transform.position;
            //繞在規定的軌道
            if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) <= r_circle)
            {
                transform.position += player.GetComponent<Player>().player_to_slime1 * Time.deltaTime;
            }
            else if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) >= r_circle + 1f)
            {
                transform.position -= player.GetComponent<Player>().player_to_slime1 * Time.deltaTime;
            }
            transform.RotateAround(player.transform.position, -Vector3.up, speed_circle * Time.deltaTime);
        }
        else if (c == 3)//第二圈
        {
            r_circle = 30;
            if (player.GetComponent<Player>().slime_partied_num < c3)
            {
                get_CircleSpeed(r_circle, player.GetComponent<Player>().slime_partied_num - c2, 1);
            }
            else
            {
                get_CircleSpeed(r_circle, c3 - c2, 1);
            }

            player.GetComponent<Player>().player_to_slime1 = this.gameObject.transform.position - player.transform.position;
            //繞在規定的軌道
            if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) <= r_circle)
            {
                transform.position += player.GetComponent<Player>().player_to_slime1 * Time.deltaTime;
            }
            else if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) >= r_circle + 1f)
            {
                transform.position -= player.GetComponent<Player>().player_to_slime1 * Time.deltaTime;
            }
            transform.RotateAround(player.transform.position, Vector3.up, speed_circle * Time.deltaTime);
        }
    }

}
