using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-----------------------------------------interface
interface ISlime_farm_info
{
    string Type
    {
        get;
    }
    string Mature
    {
        get;
    }
}
//-----------------------------------------interface
public class Slime_normal_farm : MonoBehaviour, ISlime_farm_info
{
    public string type = "normal";
    /// <summary>
    /// 日期
    /// </summary>
    public int Day;
    /// <summary>
    /// 飢餓度
    /// </summary>
    public int Hunger;
    protected float time_hunger;
    protected bool is_eating = false;
    public GameObject[] food_container = new GameObject[10];
    /// <summary>
    /// 移動方向
    /// </summary>
    protected Vector3 movement;
    protected Quaternion faceRotation;
    /// <summary>
    /// 目標
    /// </summary>
    protected Vector3 target;
    /// <summary>
    /// 開始往一個方向移動的時間
    /// </summary>
    protected float time_move;
    /// <summary>
    /// 往一個方向最多移動多久
    /// </summary>
    protected float Mextime = 5;
    /// <summary>
    /// 是否在移動
    /// </summary>
    protected bool is_move = false;
    /// <summary>
    /// 移動速度
    /// </summary>
    protected float move_speed = 3;
    /// <summary>
    /// 年齡
    /// </summary>
    public int age = 0;
    /// <summary>
    /// 成熟度
    /// </summary>
    public string mature;
    /// <summary>
    /// 是否在戀愛中
    /// </summary>
    public bool in_love = false;
    /// <summary>
    /// 有人在配對
    /// </summary>
    public bool is_maching = false;
    protected bool mach_p = false;
    /// <summary>
    /// 上次配對時間
    /// </summary>
    protected float time_mach;
    /// <summary>
    /// 配對間隔時間
    /// </summary>
    protected float mach_time = 3;
    /// <summary>
    /// 是否懷孕
    /// </summary>
    public bool pregnanted = false;
    /// <summary>
    /// 懷孕的時間
    /// </summary>
    protected float pregnanted_time;
    /// <summary>
    /// 卵子數
    /// </summary>
    public int egg_num = 3;
    /// <summary>
    /// 幼蟲
    /// </summary>
    public GameObject[] larva;
    /// <summary>
    /// 父母
    /// </summary>
    public GameObject mom, dad;
    /// <summary>
    /// 伴侶
    /// </summary>
    public GameObject better_half;
    protected bool set = false;
    /// <summary>
    /// 居住的農場
    /// </summary>
    public GameObject farm;
    /// <summary>
    /// alien
    /// </summary>
    public bool alien = false;
    protected int num;
    protected GameObject DatNightControl;
    //-----------------------------------------interface
    public string Type
    {
        get
        {
            return type;
        }
    }
    public string Mature
    {
        get
        {
            return mature;
        }
    }
    //-----------------------------------------interface
    protected void Start()
    {
        DatNightControl = GameObject.Find("DatNightControl");
        age = 0;
        if (transform.parent.name[0]=='F')
        {
            age = 3;
            mature = "teenager";
        }
        else
        {
            mom = transform.parent.gameObject;
            dad = transform.parent.gameObject.GetComponent<Slime_normal_farm>().better_half;
            transform.parent = transform.parent.transform.parent;
        }
        better_half = null;
        farm = gameObject.transform.parent.gameObject;
        this.gameObject.name = "Slime_farm_" + farm.GetComponent<Slime_farm>().slime_num;
        num = farm.GetComponent<Slime_farm>().slime_num;
        pregnanted = false;
        in_love = false;
        egg_num = 3;
        Hunger = 100;
        time_hunger = 0;
        Day = DatNightControl.GetComponent<DayAndNight>().Day;
        pregnanted_time = time_mach = time_move = Time.time;
    }

    protected void Update()
    {
        aged();
        move_state(in_love, mature);
        //每隔一段時間男生會去找女生配對
        if (!in_love && num % 2 == 1 && Time.time - time_mach >= DatNightControl.GetComponent<DayAndNight>().Day_long / 3 && mature == "teenager")
        {
            mach();
            time_mach = Time.time;
        }
        if(Time.time - time_hunger >= DatNightControl.GetComponent<DayAndNight>().Day_long / 10)
        {
            time_hunger = Time.time;
            Hunger -= 50;
        }
        if(Hunger <= 60 && Vector3.Distance(transform.parent.GetChild(0).transform.position,transform.position) <= 20 && !transform.parent.GetChild(0).transform.GetChild(0).transform.GetChild(transform.parent.gameObject.GetComponent<Slime_farm>().type_change(type)).GetComponent<Food_container>().is_cooling && transform.parent.GetChild(0).transform.GetChild(0).transform.GetChild(transform.parent.gameObject.GetComponent<Slime_farm>().type_change(type)).GetComponent<Food_container>().food_amount > 0 && !transform.parent.GetChild(0).transform.GetChild(0).transform.GetChild(transform.parent.gameObject.GetComponent<Slime_farm>().type_change(type)).GetComponent<Food_container>().is_giving_food)
        {
            transform.parent.GetChild(0).transform.GetChild(0).transform.GetChild(transform.parent.gameObject.GetComponent<Slime_farm>().type_change(type)).GetComponent<Food_container>().is_giving_food = true;
            is_eating = true;
            StartCoroutine(eating(Time.time, 20));
        }
    }

    /// <summary>
    /// 吃
    /// </summary>
    protected IEnumerator eating(float _start_time, float _effect_time)
    {
        yield return new WaitForSeconds(0.5f);
        if (Time.time - _start_time - _effect_time <= 0 && transform.parent.GetChild(0).transform.GetChild(0).transform.GetChild(transform.parent.gameObject.GetComponent<Slime_farm>().type_change(type)).GetComponent<Food_container>().food_amount > 0)
        {
            transform.parent.GetChild(0).transform.GetChild(0).transform.GetChild(transform.parent.gameObject.GetComponent<Slime_farm>().type_change(type)).GetComponent<Food_container>().food_amount--;
            Hunger++;
            StartCoroutine(eating(_start_time, _effect_time));
        }
        else
        {
            is_eating = false;
            transform.parent.GetChild(0).transform.GetChild(0).transform.GetChild(transform.parent.gameObject.GetComponent<Slime_farm>().type_change(type)).GetComponent<Food_container>().is_giving_food = false;
            StopCoroutine(eating(_start_time, _effect_time));
        }
    }

    protected void move_state(bool _in_love, string _mature)
    {
        if (Hunger > 60)
        {
            switch (_mature)
            {
                case "baby":
                    //----------------------------------------------------------------------------------------四處移動
                    if (Vector3.Distance(transform.position, target) < 1f || Time.time - time_move >= Mextime)
                    {
                        time_move = Time.time;
                        is_move = false;
                    }
                    //如果移動狀態為否則設定下一個目的地
                    if (!is_move)
                    {
                        target = new Vector3(Random.Range(farm.transform.GetChild(4).transform.position.x + 50, farm.transform.GetChild(4).transform.position.x - 50), 0, Random.Range(farm.transform.GetChild(4).transform.position.z - 50, farm.transform.GetChild(4).transform.position.z + 50));
                        is_move = true;
                    }
                    move(target, target, move_speed);
                    //----------------------------------------------------------------------------------------四處移動
                    break;
                case "teenager":
                    if (!_in_love)
                    {
                        //----------------------------------------------------------------------------------------四處移動
                        if (Vector3.Distance(transform.position, target) < 1f || Time.time - time_move >= Mextime)
                        {
                            time_move = Time.time;
                            is_move = false;
                        }
                        //如果移動狀態為否則設定下一個目的地
                        if (!is_move)
                        {
                            target = new Vector3(Random.Range(farm.transform.GetChild(4).transform.position.x + 50, farm.transform.GetChild(4).transform.position.x - 50), 0, Random.Range(farm.transform.GetChild(4).transform.position.z - 50, farm.transform.GetChild(4).transform.position.z + 50));
                            is_move = true;
                        }
                        move(target, target, move_speed);
                        //----------------------------------------------------------------------------------------四處移動
                    }
                    else
                    {
                        if (num % 2 == 1)
                        {
                            fallow(better_half.transform.GetChild(0).transform.position, better_half.transform.GetChild(1).transform.position);
                        }
                        else
                        {
                            //----------------------------------------------------------------------------------------四處移動
                            if (Vector3.Distance(transform.position, target) < 1f || Time.time - time_move >= Mextime)
                            {
                                time_move = Time.time;
                                is_move = false;
                            }
                            //如果移動狀態為否則設定下一個目的地
                            if (!is_move)
                            {
                                target = new Vector3(Random.Range(farm.transform.GetChild(4).transform.position.x + 50, farm.transform.GetChild(4).transform.position.x - 50), 0, Random.Range(farm.transform.GetChild(4).transform.position.z - 50, farm.transform.GetChild(4).transform.position.z + 50));
                                is_move = true;
                            }
                            move(target, target, move_speed);
                            //----------------------------------------------------------------------------------------四處移動
                        }
                    }
                    break;
                case "adult":
                    //----------------------------------------------------------------------------------------四處移動
                    if (Vector3.Distance(transform.position, target) < 1f || Time.time - time_move >= Mextime)
                    {
                        time_move = Time.time;
                        is_move = false;
                    }
                    //如果移動狀態為否則設定下一個目的地
                    if (!is_move)
                    {
                        target = new Vector3(Random.Range(farm.transform.GetChild(4).transform.position.x + 50, farm.transform.GetChild(4).transform.position.x - 50), 0, Random.Range(farm.transform.GetChild(4).transform.position.z - 50, farm.transform.GetChild(4).transform.position.z + 50));
                        is_move = true;
                    }
                    move(target, target, move_speed);
                    //----------------------------------------------------------------------------------------四處移動
                    break;
            }
        }
        else
        {
            if (!is_eating)
            {
                move(transform.parent.GetChild(0).transform.position, transform.parent.GetChild(0).transform.position, move_speed);
            }
        }
    }
    protected void pregnant(bool _pregnanted,int _lava_type)
    {
        if (!_pregnanted)
        {
            if (num % 2 == 0 && Time.time - pregnanted_time >= DatNightControl.GetComponent<DayAndNight>().Day_long && egg_num >= 1)
            {
                if (Random.Range(0, 2) == 1)
                {
                    pregnanted = true;
                }
                egg_num--;
                pregnanted_time = Time.time;
            }
        }
        else
        {
            if (Time.time - pregnanted_time >= 15)
            {
                if (transform.parent.GetComponent<Slime_farm>().slime_max_num < 50)
                {
                    Instantiate(larva[_lava_type].transform, new Vector3(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z), larva[_lava_type].transform.rotation, gameObject.transform);
                    transform.parent.GetComponent<Slime_farm>().slime_num++;
                    transform.parent.GetComponent<Slime_farm>().slime_max_num++;
                }
                pregnanted = false;
                pregnanted_time = Time.time;
            }
        }
    }
    /// <summary>
    /// 配對
    /// </summary>
    protected void mach()
    {
        for (int i = 1; i < farm.GetComponent<Slime_farm>().slime_num; i += 2)
        {
            if (GameObject.Find("Slime_farm_" + i).GetComponent<Slime_normal_farm>().is_maching)
            {
                mach_p = true;
            }
        }
        if (!mach_p)
        {
            is_maching = true;
            for (int i = 2; i <= farm.GetComponent<Slime_farm>().slime_num; i += 2)
            {
                if (!GameObject.Find("Slime_farm_" + i).GetComponent<Slime_normal_farm>().in_love && GameObject.Find("Slime_farm_" + i).GetComponent<Slime_normal_farm>().mature == "teenager" && Random.Range(0, 2) == 1)
                {
                    in_love = true;
                    GameObject.Find("Slime_farm_" + i).GetComponent<Slime_normal_farm>().in_love = true;
                    GameObject.Find("Slime_farm_" + i).GetComponent<Slime_normal_farm>().better_half = this.gameObject;
                    better_half = GameObject.Find("Slime_farm_" + i);
                    break;
                }
            }
            is_maching = false;
        }
        mach_p = false;
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
    /// 跟隨
    /// </summary>
    /// <param name="anchor_position"></param>
    /// <param name="anchor_ahead"></param>
    protected void fallow(Vector3 anchor_position, Vector3 anchor_ahead)
    {
        move_speed = 350 * Time.deltaTime;

        if (Vector3.Distance(transform.position, anchor_position) > 5f)
        {
            move(anchor_position, anchor_ahead, move_speed);
        }
    }
    /// <summary>
    /// 長大
    /// </summary>
    void aged()
    {
        
        //-----------------------------每過一天age加一
        if (Input.GetKeyDown(KeyCode.G) || DatNightControl.GetComponent<DayAndNight>().Day!=Day)
        {
            age++;
            Day = DatNightControl.GetComponent<DayAndNight>().Day;
            if (age == 3)
            {
                transform.position += new Vector3(0, 3, 0);
            }
            else if (age == 5)
            {
                transform.position += new Vector3(0, 5, 0);
            }
        }
        //-----------------------------每過一天age加一
        if (age <= 2)
        {
            mature = "baby";
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (age <= 6)
        {
            mature = "teenager";
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            mature = "adult";
            transform.localScale = new Vector3(2, 2, 2);
        }

    }
}
