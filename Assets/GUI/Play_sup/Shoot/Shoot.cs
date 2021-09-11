using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    //GameObject player;
    /// <summary>
    /// 上個frame Tab是開(1)還是關(2)
    /// </summary>
    public int last_state = 2;
    /// <summary>
    /// 背景長寬
    /// </summary>
    Vector2 Width_Height, Width_Height_2;
    /// <summary>
    /// Icon在螢幕的相對位置
    /// </summary>
    Vector3 FX_position;
    public float shoot_power;

    Vector3 mouse_pos_a, mouse_pos_b, shoot_movement;
    


    void Start () {
        //player = GameObject.Find("Player");
        Hide();
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Width_Height.Set(Screen.height * 0.5f, Screen.height * 0.5f);
            this.GetComponent<RectTransform>().sizeDelta = Width_Height;

            FX_position.Set(Input.mousePosition.x, Input.mousePosition.y, 0);
            this.GetComponent<RectTransform>().localPosition = FX_position;

            mouse_pos_a.Set(Input.mousePosition.x, 0, Input.mousePosition.y);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (last_state == 2)
            {
                Show();
                last_state = 1;
            }
            //------------------------------------------------------
            mouse_pos_b.Set(Input.mousePosition.x, 0, Input.mousePosition.y);
            shoot_movement = mouse_pos_a - mouse_pos_b;
            //------------------------------------------------------
            //------------------------------------------------------
            if (2 * Vector3.Distance(mouse_pos_a, mouse_pos_b) <= Width_Height.x)
            {
                Width_Height_2.Set(2 * Vector3.Distance(mouse_pos_a, mouse_pos_b), 2 * Vector3.Distance(mouse_pos_a, mouse_pos_b));
                Width_Height_2.Set(Width_Height.x, 2 * Vector3.Distance(mouse_pos_a, mouse_pos_b));
                this.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = Width_Height_2;
                shoot_power = Vector3.Distance(mouse_pos_a, mouse_pos_b) / (Width_Height.x / 2);
            }
            else
            {
                Width_Height_2.Set(Width_Height.x, Width_Height.x);
                this.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = Width_Height_2;
                shoot_power = 1;
            }
            if (shoot_movement.magnitude != 0)
            {
                rotat_shoot(Vector3.forward, shoot_movement);
            }
            //------------------------------------------------------
        }
        else
        {
            if (last_state == 1)
            {
                Hide();
                last_state = 2;
            }
        }
    }

    void rotat_shoot(Vector3 _vector_a, Vector3 _vector_b)
    {
        float _angle = 0;

        _angle = Mathf.Acos((_vector_a.x * _vector_b.x + _vector_a.y * _vector_b.y + _vector_a.z * _vector_b.z) / (Vector3.Distance(Vector3.zero, _vector_a) * Vector3.Distance(Vector3.zero, _vector_b))) * 180 / Mathf.PI;
        if (mouse_pos_b.x < mouse_pos_a.x)
        {
            _angle *= -1;
        }
        Debug.Log("Angle:" + _angle);
        this.transform.GetChild(0).GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, _angle);
    }

    public Vector3 Shoot_movement(float _angle)
    {
        //Debug.Log(_angle);
        _angle *= -1;
        Vector3 _shoot_movement;
        _shoot_movement.x = shoot_movement.x * Mathf.Cos(_angle / 180 * Mathf.PI) - shoot_movement.z * Mathf.Sin(_angle / 180 * Mathf.PI);
        _shoot_movement.y = 0;
        _shoot_movement.z = shoot_movement.x * Mathf.Sin(_angle / 180 * Mathf.PI) + shoot_movement.z * Mathf.Cos(_angle / 180 * Mathf.PI);
        return _shoot_movement;
    }


    void Hide()
    {
        this.transform.parent.GetComponent<CanvasGroup>().alpha = 0f; //this makes everything transparent
        this.transform.parent.GetComponent<CanvasGroup>().blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    void Show()
    {
        this.transform.parent.GetComponent<CanvasGroup>().alpha = 1f;
        this.transform.parent.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
