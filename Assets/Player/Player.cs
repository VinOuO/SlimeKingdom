using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    ISlime_info slime_info;
    //public string GameOrFarm = "Game";
    public bool Slime_farm_loaded = false;
    public Vector3 ExitMainWorld_pos;
    public int magma_contral = 0;
    //史萊姆隊形
    public int form_type = 1;
    public int form_type_p = 1;
    //產生的史萊姆種類
    public GameObject[] slime_type;
    /// <summary>
    /// 蛋的數量
    /// </summary>
    public int[] egg_num;
    /// <summary>
    /// 飼料的數量
    /// </summary>
    public int[] food_num;
    /// <summary>
    /// 子彈
    /// </summary>
    public GameObject bullet;
    /// <summary>
    /// 子彈數量
    /// </summary>
    public int bullet_num = 0;
    float time_charge_bullet = 0;
    /// <summary>
    /// zx連擊以脫離禁錮
    /// </summary>
    public bool is_minigame_root = false;
    /// <summary>
    /// ZX連擊數
    /// </summary>
    public int zx_time = 0;
    public string last_time_hit = "x";
    //在隊伍中的史萊姆數目
    public int slime_partied_num = 0;
    //隊伍中的第一隻史萊姆與玩家的向量
    public Vector3 player_to_slime1;
    //是否移動
    public bool is_moving = false;
    //移動目的地
    public Vector3 target;
    //移動速度
    public float speed = 100f;
    /// <summary>
    /// 是否被硬控
    /// </summary>
    public bool can_move = true;
    int can_move_count = 0;
    /// <summary>
    /// 被位移型控制的種類
    /// </summary>
    string position_control_type = "none";
    /// <summary>
    /// 被位移方向
    /// </summary>
    Vector3 position_control_movement;
    /// <summary>
    /// 是否被致盲
    /// </summary>
    bool is_blinded = false;
    /// <summary>
    /// 是否滑倒
    /// </summary>
    public bool is_sliped = false;
    /// <summary>
    /// 是否被暈眩
    /// </summary>
    public bool is_stuned = false;
    /// <summary>
    /// 踩到泥坑的時間
    /// </summary>
    public float step_pit_time = 0;
    //player移動量
    public Vector3 movement;
    //player的動畫資訊
    Animator anim;
    //player的Rigidbody資訊
    Rigidbody playerRigidbody;
    //玩家面向方向
    Quaternion playRotation;
    //地板(floor)的layer編號
    int floorMask;
    //射線的長度
    float camRayLength = 1000;
    //玩家2D位置&目標2D位置
    Vector2 player_pos2D, target_pos2D;
    //丟出的向量
    public Vector3 throwment;
    //投擲目的地
    Vector3 throw_target;
    /// <summary>
    /// 是否在進攻
    /// </summary>
    public bool engaged = false;
    /// <summary>
    /// 血量訊息文字位置
    /// </summary>
    Vector3 Health_info_position;
    /// <summary>
    /// 滑鼠射線
    /// </summary>
    Ray ray;
    /// <summary>
    /// XD
    /// </summary>
    RaycastHit hit;
    /// <summary>
    /// 總血量
    /// </summary>
    float Health = 100;
    /// <summary>
    /// 目前血量
    /// </summary>
    float health = 100;
    /// <summary>
    /// 是否偽裝
    /// </summary>
    public bool is_hided = false;
    float time_hide;
    /// <summary>
    /// 目前移動方式
    /// </summary>
    public string move_way = "2.5D";
    /// <summary>
    /// FPS鏡頭面對方向
    /// </summary>
    Vector3 cam_face;
    /// <summary>
    /// 撞到地形
    /// </summary>
    public bool hit_terrain = false;
    public float time_hit_terrain = 0;
    /// <summary>
    /// 踏了幾次水
    /// </summary>
    public int step_pool = 0;
    /// <summary>
    /// Nav
    /// </summary>
    public NavMeshAgent player_nav;

    /// <summary>
    /// 射擊資訊
    /// </summary>
    public GameObject Shoot_GUI;
    /// <summary>
    /// 是否可射擊史萊姆
    /// </summary>
    public bool can_shoot = true;
    /// <summary>
    /// 遊戲資訊GUI
    /// </summary>
    protected GameObject combo;

    public Vector3 aaa;
    public bool aa;
    public int a;
    void Start()
    {
        //---------------------------------
        player_nav = GetComponent<NavMeshAgent>();
        //---------------------------------
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        combo = GameObject.Find("Combo");
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + 10 * throwment);
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
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (move_way == "2.5D")
                {
                    move_way = "FPS";
                }
                else
                {
                    move_way = "2.5D";
                }
            }
            if (move_way == "FPS")
            {
                Move2();
                transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(3).gameObject.SetActive(true);
                Move();
                mouse();
                //move(target, target, speed);
            }
            hide();
            player_nav.speed = speed;
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                shoot(move_way);
            }
            bullet_charge();
            //----------------------------------------------------------temp生Slime
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                /*for (int i = 0; i < SceneManager.sceneCount; ++i)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.name == sceneName_no_extention)
                    {
                        //the scene is already loaded
                        return true;
                    }*/
                    slime_partied_num++;
                Instantiate(slime_type[0].transform, transform.position - 3 * Vector3.forward + 3 * Vector3.up, slime_type[0].transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                slime_partied_num++;
                Instantiate(slime_type[1].transform, transform.position - 3 * Vector3.forward + 3 * Vector3.up, slime_type[1].transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                slime_partied_num++;
                Instantiate(slime_type[2].transform, transform.position - 3 * Vector3.forward + 3 * Vector3.up, slime_type[2].transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                slime_partied_num++;
                Instantiate(slime_type[3].transform, transform.position - 3 * Vector3.forward + 3 * Vector3.up, slime_type[3].transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                slime_partied_num++;
                Instantiate(slime_type[4].transform, transform.position - 3 * Vector3.forward + 3 * Vector3.up, slime_type[4].transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                slime_partied_num++;
                Instantiate(slime_type[5].transform, transform.position - 3 * Vector3.forward + 3 * Vector3.up, slime_type[5].transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                slime_partied_num++;
                Instantiate(slime_type[6].transform, transform.position - 3 * Vector3.forward + 3 * Vector3.up, slime_type[6].transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                slime_partied_num++;
                Instantiate(slime_type[7].transform, transform.position - 3 * Vector3.forward + 10 * Vector3.up, slime_type[7].transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                slime_partied_num++;
                Instantiate(slime_type[8].transform, transform.position - 3 * Vector3.forward + 3 * Vector3.up, slime_type[8].transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                slime_partied_num++;
                Instantiate(slime_type[9].transform, transform.position - 3 * Vector3.forward + 3 * Vector3.up, slime_type[9].transform.rotation);
            }
            //----------------------------------------------------------temp生Slime
            //隊形選擇
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                form_type = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                form_type = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                form_type = 3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (engaged)
                {
                    engaged = false;
                }
                else
                {
                    engaged = true;
                }
            }
            //投擲史萊姆
            if (Input.GetKeyUp(KeyCode.Mouse0) && slime_partied_num > 0 && can_shoot)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    //設定目的地
                    throw_target.Set(hit.point.x, GameObject.Find("Slime_1").transform.position.y, hit.point.z);
                    throw_slime(throw_target);
                    slime_partied_num--;
                }
            }
        }
        else
        {
            if (!hit_terrain)
            {
                position_control(position_control_movement, transform.position, 1f, position_control_type);
            }
            else
            {
                playerRigidbody.MovePosition(transform.position - movement * 0.2f);
                if (Time.time - time_hit_terrain >= 0.3f)
                {
                    hit_terrain = false;
                    playerRigidbody.velocity = Vector3.zero;
                    playerRigidbody.angularVelocity = Vector3.zero;
                }
                if (position_control_type == "slip")
                {
                    position_control_movement.Set(0, 0, 0);
                }
            }
            //------------------------------ZX
            if (is_minigame_root)
            {
                mini_game_rooted();
            }
            //------------------------------ZX
            //------------------------------暈眩
            if (is_stuned)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 720);
            }
            //------------------------------暈眩
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        slime_info = other.gameObject.transform.GetComponent(typeof(ISlime_info)) as ISlime_info;

        if (other.gameObject.name == "Slime_throwed" && !slime_info.Is_charge && slime_partied_num < 30)
        {
            slime_info.Form = form_type_p;
            slime_partied_num++;
            if (slime_info.Type == "tree")
            {
                is_hided = true;
                engaged = false;
                time_hide = Time.time;
            }
        }

        if (other.gameObject.tag == "Food")
        {
            switch (other.gameObject.GetComponent<Food>().type)
            {
                case 0:
                    food_num[0] += 5;
                    break;
                case 1:
                    food_num[1] += 5;
                    break;
                case 2:
                    food_num[2] += 5;
                    break;
                case 3:
                    food_num[3] += 5;
                    break;
                case 4:
                    food_num[4] += 5;
                    break;
                case 5:
                    food_num[5] += 5;
                    break;
                case 6:
                    food_num[6] += 5;
                    break;
                case 7:
                    food_num[7] += 5;
                    break;
                case 8:
                    food_num[8] += 5;
                    break;
                case 9:
                    food_num[9] += 5;
                    break;
            }
        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        slime_info = collision.gameObject.transform.GetComponent(typeof(ISlime_info)) as ISlime_info;

        if (collision.gameObject.name == "Slime_throwed" && (Time.time - slime_info.Time_throwed) >= 10 && slime_partied_num < 30)
        {
            slime_info.Form = form_type_p;
            slime_partied_num++;
            if (slime_info.Type == "tree")
            {
                is_hided = true;
                engaged = false;
                time_hide = Time.time;
            }
        }

        if (collision.gameObject.tag == "Slime" && collision.gameObject.name != "Slime_throwed")
        {
            //Physics.IgnoreCollision(collision.collider, this.gameObject.GetComponent<Collider>());
        }

        if (collision.gameObject.tag == "Food")
        {
            switch (collision.gameObject.GetComponent<Food>().type)
            {
                case 0:
                    break;
                case 1:
                    food_num[1] += 5;
                    break;
                case 2:
                    food_num[2] += 5;
                    break;
                case 3:
                    food_num[3] += 5;
                    break;
            }
        }
    }
    */
    void mouse()
    {
        //-----------------------------------------------------------------Player右鍵移動
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKey(KeyCode.Mouse1) && Physics.Raycast(ray, out hit))
        {
            //設定目的地
            if (hit.transform.gameObject.tag == "floor" || hit.transform.gameObject.tag == "Pool")
            {
                target.Set(hit.point.x, 0, hit.point.z);
            }
        }
        //-----------------------------------------------------------------Player右鍵移動
    }
    //移動到目的地
    void move(Vector3 _target, Vector3 _face, float _speed)
    {
        _target.y = transform.position.y;
        //------------------------------------------------------------------------------Player控制
        //-----------------------------------------------------------------PlayerQ
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //speed *= (1 + 0.5f);
        }
        //-----------------------------------------------------------------PlayerQ
        //------------------------------------------------------------------------------Player控制
        if (!hit_terrain)
        {
            Vector3 ahead;
            if (Vector2.Distance(_target, transform.position) >= 1f)
            {
                is_moving = true;
                movement.Set(_target.x - transform.position.x, 0, _target.z - transform.position.z);
                //normalized將向量長度調整到1, 代表移動的方向
                movement = movement.normalized * _speed * Time.deltaTime;
                player_nav.Move(movement);
                //利用Rigidbody的MovePosition()平順的移動物件
                //this.gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + movement);

                //player到交點的向量
                ahead = _face - transform.position;
                //player只在x與z方向移動，故y=0
                ahead.y = 0;
                //方向
                playRotation = Quaternion.LookRotation(ahead);
                this.gameObject.transform.GetComponent<Rigidbody>().MoveRotation(playRotation);
            }
            else
            {
                movement = Vector3.zero;
                is_moving = false;
            }
        }
        else
        {
            playerRigidbody.MovePosition(transform.position - movement * 0.2f);
            if (Time.time - time_hit_terrain >= 0.3f)
            {
                hit_terrain = false;
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
    //移動
    void Move()
    {
        //------------------------------------------------------------------------------Player控制
        //-----------------------------------------------------------------PlayerQ
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //speed *= (1 + 0.5f);
        }
        //-----------------------------------------------------------------PlayerQ
        //------------------------------------------------------------------------------Player控制

        if (!hit_terrain)
        {
            if (Input.GetKey(KeyCode.A))
            {
                //movement -= Vector3.right.normalized;
                cam_face.Set(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
                cam_face = cam_face.normalized;
                movement -= cam_face;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //movement += Vector3.right.normalized;
                cam_face.Set(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
                cam_face = cam_face.normalized;
                movement += cam_face;
            }

            if (Input.GetKey(KeyCode.W))
            {
                //movement += Vector3.forward.normalized;
                cam_face.Set(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                cam_face = cam_face.normalized;
                movement += cam_face;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                //movement -= Vector3.forward.normalized;
                cam_face.Set(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                cam_face = cam_face.normalized;
                movement -= cam_face;
            }

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                movement.Set(0, 0, 0);
                is_moving = false;
            }
            else
            {
                is_moving = true;
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
                //normalized將向量長度調整到1, 代表移動的方向
                movement = movement.normalized * 2 * speed * Time.deltaTime;      //asdw移動
                player_nav.Move(movement);
                playRotation = Quaternion.LookRotation(movement);
                this.gameObject.transform.GetComponent<Rigidbody>().MoveRotation(playRotation);
            }

        }
        else
        {
            playerRigidbody.MovePosition(transform.position - movement * 0.2f);
            if (Time.time - time_hit_terrain >= 0.3f)
            {
                hit_terrain = false;
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
    //移動
    void Move2()
    {
        //------------------------------------------------------------------------------Player控制
        //-----------------------------------------------------------------PlayerQ
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //speed *= (1 + 0.5f);
        }
        //-----------------------------------------------------------------PlayerQ
        //------------------------------------------------------------------------------Player控制

        if (!hit_terrain)
        {
            if (Input.GetKey(KeyCode.A))
            {
                //movement -= Vector3.right.normalized;
                cam_face.Set(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
                cam_face = cam_face.normalized;
                movement -= cam_face;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //movement += Vector3.right.normalized;
                cam_face.Set(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
                cam_face = cam_face.normalized;
                movement += cam_face;
            }

            if (Input.GetKey(KeyCode.W))
            {
                //movement += Vector3.forward.normalized;
                cam_face.Set(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                cam_face = cam_face.normalized;
                movement += cam_face;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                //movement -= Vector3.forward.normalized;
                cam_face.Set(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                cam_face = cam_face.normalized;
                movement -= cam_face;
            }

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                movement.Set(0, 0, 0);
                is_moving = false;
            }
            else
            {
                is_moving = true;
            }
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
            //normalized將向量長度調整到1, 代表移動的方向
            movement = movement.normalized * 2 * speed * Time.deltaTime;      //asdw移動
            player_nav.Move(movement);
            //playerRigidbody.MovePosition(transform.position + movement);
            playRotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z));
            this.gameObject.transform.GetComponent<Rigidbody>().MoveRotation(playRotation);
        }
        else
        {
            playerRigidbody.MovePosition(transform.position - movement * 0.2f);
            if (Time.time - time_hit_terrain >= 0.3f)
            {
                hit_terrain = false;
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
    //轉向
    void Turning()
    {
        //camRay代表滑鼠指向螢幕內的射線
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //沿著camRay打出射線，並計算與地板的交點(floorHit)
        //out為C＃特有的關鍵字，通常代表回傳值，在此會改寫floorHit的內容
        if (Physics.Raycast(ray, out hit, camRayLength))
        {
            //player到交點的向量
            Vector3 playerToMouse = hit.point - transform.position;
            //player只在x與z方向移動，故y=0
            playerToMouse.y = 0;

            //LookRotation會讓player從目前看的方向轉到滑鼠地板交點的方向
            playRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(playRotation);
        }
    }

    void shoot(string _type)
    {
        GameObject _bullet;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 _way;
        if (bullet_num > 0)
        {
            if (_type == "FPS")
            {
                Debug.DrawRay(_ray.origin, _ray.direction * 1000, Color.yellow);
                _bullet = Instantiate(bullet, this.transform.position, bullet.transform.rotation);
                bullet_num--;
                _bullet.transform.GetComponent<Bullet_player>().way = _ray.direction.normalized;
            }
            else
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit _hit;
                if (Physics.Raycast(_ray, out _hit))
                {
                    _way = new Vector3(_hit.point.x - this.transform.position.x, 0, _hit.point.z - this.transform.position.z);
                    _way = _way.normalized;
                    _bullet = Instantiate(bullet, this.transform.position, bullet.transform.rotation);
                    bullet_num--;
                    _bullet.transform.GetComponent<Bullet_player>().way = _way;
                }
            }
        }
    }

    void bullet_charge()
    {
        if (Time.time - time_charge_bullet >= 5)
        {
            time_charge_bullet = Time.time;
            bullet_num = slime_partied_num + 1;
        }
    }

    //投擲
    void throw_slime(Vector3 _target)
    {
        //s=1*cos*2*sin/a+1/2*a*(2*sin/a)^2
        float S = 400, x, y, g =22500 ,angle = 0;
        Vector3 _pos_a, _pos_b;

        throwment.Set(_target.x - this.gameObject.transform.position.x, 0, _target.z - this.gameObject.transform.position.z);
        _pos_a = GameObject.Find("Slime_1").gameObject.transform.position;
        _pos_b = _target;
        _pos_b.y = _pos_a.y;
        if (Vector3.Distance(_target, this.gameObject.transform.position) <= 400)
        {
            S = Vector3.Distance(_target, this.gameObject.transform.position);
        }
        angle = Mathf.Asin(S * g * 1 / 9000000) / 2 * 180 / Mathf.PI;
        //Debug.Log(Camera.main.transform.rotation);
        throwment = Shoot_GUI.GetComponent<Shoot>().Shoot_movement(Camera.main.transform.eulerAngles.y);
        throwment.y = 0;
        throwment = throwment.normalized;
        //throwment.y = 1;


        StartCoroutine(throw_slime(Time.time, 0.5f));
        can_move_count--;
        //LookRotation會讓player從目前看的方向轉到滑鼠地板交點的方向
        playRotation = Quaternion.LookRotation(1000*throwment);
        playerRigidbody.rotation = playRotation;

        this.gameObject.GetComponent<Player_anim>().shoot_time = Time.time;
        can_shoot = false;
    }
    //-----------------------------------------------animator
    /// <summary>
    /// 射擊史萊姆
    /// </summary>
    protected IEnumerator throw_slime(float _start_time, float _effect_time)
    {
        yield return new WaitForSeconds(0.1f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            StartCoroutine(throw_slime(_start_time, _effect_time));
        }
        else
        {
            can_move_count++;
            slime_info = GameObject.Find("Slime_1").gameObject.transform.GetComponent(typeof(ISlime_info)) as ISlime_info;
            slime_info.Hit = false;//丟出去要判斷是不是有打到東西了
            slime_info.Form = 4;
            slime_info.Throwed(throwment, Shoot_GUI.GetComponent<Shoot>().shoot_power * 15, 5);
            GameObject.Find("Slime_1").gameObject.name = "Slime_throwed";
            slime_info.Is_charge = true;
            slime_info.Num_in_party = 0;
            StopCoroutine(throw_slime( _start_time, _effect_time));
        }
    }
    //-----------------------------------------------animator
    /// <summary>
    /// 偽裝
    /// </summary>
    void hide()
    {
        if (is_hided)
        {
            if (Time.time - time_hide <= 10)
            {
                this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
                is_hided = false;
            }
        }
    }
    /// <summary>
    /// 被攻擊
    /// </summary>
    public void get_attack(float _hurt, float _effect_time, Vector3 _enemy_position, string _effect)
    {
        health -= _hurt;

        if (_effect == "knock")//-----------------------------狀態:撞擊
        {
            can_move_count--;
            position_control_type = "knock";
            position_control_movement = this.transform.position - _enemy_position;
            position_control_movement.y = 0;
            position_control_movement = position_control_movement.normalized;
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
                target = transform.position;
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
                playerRigidbody.MovePosition(this.transform.position + _movement * _speed);
                break;
            case "slip":
                playerRigidbody.MovePosition(this.transform.position + _movement * _speed);
                break;
        }

    }
    //---------------------------------------------------------------------------------buff
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
        yield return new WaitForSeconds(0.01f);
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
    /// 擊飛
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
            movement.Set(0, 0, 0);
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
        yield return new WaitForSeconds(0.01f);
        if (Time.time - _start_time - _effect_time <= 0)
        {
            StartCoroutine(slow(_start_time, _effect_time));
        }
        else
        {
            speed *= 2;
            StopCoroutine(slow(_start_time, _effect_time));
        }
    }
    //--------------------------------------------------------------------------------buff
    public void mini_game_rooted_start()
    {
        last_time_hit = "x";
        zx_time = 0;
        is_minigame_root = true;
    }
    void mini_game_rooted_end()
    {
        can_move_count++;
        last_time_hit = "x";
        zx_time = 0;
        is_minigame_root = false;
    }
    void mini_game_rooted()
    {
        if (last_time_hit == "x")
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                zx_time++;
                last_time_hit = "z";
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                zx_time++;
                last_time_hit = "x";
            }
        }
        if (zx_time >= 20)
        {
            mini_game_rooted_end();
        }

    }

    /// <summary>
    /// 顯示血量
    /// </summary>
    /// <param name="_s"></param>
    /// <returns></returns>
    public float health_show(string _s)
    {
        if (_s == "Max")
        {
            return Health;
        }
        else if(_s == "Now")
        {
            return health;
        }
        else
        {
            return health / Health;
        }
    }
}
