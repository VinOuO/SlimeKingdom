using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_ball : MonoBehaviour {
    ISlime_info slime_info;
    Ray ray;
    RaycastHit hit;
    protected int layerMask = 1 << 9;
    protected int radius = 20;
    protected Collider[] hitColliders;
    /// <summary>
    /// 背景長寬
    /// </summary>                        
    Vector2 Width_Height, Width_Height_2;
    /// <summary>
    /// Icon在螢幕的相對位置
    /// </summary>
    Vector3 fire_ball_position;
    //                       從落地位置回推
    public Vector3 fall_pos, back_vec;
    int fall_time = 60;
    float time = 0;
    Vector3 movement;
    void Start () {
        Width_Height.Set(Screen.height * 1 / 6, Screen.height * 1 / 6);
        this.GetComponent<RectTransform>().sizeDelta = Width_Height;

        fire_ball_position.Set(- Screen.width / 2, Screen.height / 2, 0);
        back_vec.Set(-Screen.height, Screen.height, 0);
        fire_ball_position = fall_pos + back_vec;
        this.GetComponent<RectTransform>().localPosition = fire_ball_position;

        movement.Set(Screen.height / fall_time, -Screen.height / fall_time, 0);
    }
	
	void Update () {
        Width_Height.Set(Screen.height * 1 / 6, Screen.height * 1 / 6);
        this.GetComponent<RectTransform>().sizeDelta = Width_Height;

        if (Time.time - time >= 0.01f)
        {
            fall_time--;
            time = Time.time;
            fire_ball_position += movement;
            this.GetComponent<RectTransform>().localPosition = fire_ball_position;
        }
        if (fall_time <= 0)
        {
            //camRay代表滑鼠指向螢幕內的射線
            ray = Camera.main.ScreenPointToRay(this.GetComponent<RectTransform>().localPosition+ transform.parent.GetComponent<RectTransform>().localPosition);

            //沿著camRay打出射線，並計算與地板的交點(floorHit)
            //out為C＃特有的關鍵字，通常代表回傳值，在此會改寫floorHit的內容
            if (Physics.Raycast(ray, out hit, 1000))
            {
                layerMask = 1 << 9;
                int i = 0;
                radius = 20;
                hitColliders = Physics.OverlapSphere(hit.point, radius, layerMask);
                while (i < hitColliders.Length)
                {
                    if (hitColliders[i].gameObject.name == "Player")
                    {
                        hitColliders[i].gameObject.GetComponent<Player>().get_attack(10, 0.4f * 5 + 1, transform.position, "stun");
                    }
                    else
                    {
                        slime_info = hitColliders[i].gameObject.GetComponent(typeof(ISlime_info)) as ISlime_info;
                        slime_info.Get_attack(5, 0.4f * 5 + 1, transform.position, "stun");
                    }
                    i++;
                }
            }
            //--------------------------------------------------------------------------
            
            Destroy(this.gameObject);
        }
    }
}
