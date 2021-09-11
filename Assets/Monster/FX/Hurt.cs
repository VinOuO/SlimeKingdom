using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hurt : MonoBehaviour {
    public Vector3 start_pos;
    Vector2 Width_Height;
    Vector3 movement;
    Vector3 face_cam, adj_pos;
    public float hurt_num = 0;
    float up_time = 0, shake_time = 0, shake = 0;
    Rect screenRect;
    void Start () {
        adj_pos.Set(Screen.width / 2, Screen.height / 2, 0);
        this.GetComponent<RectTransform>().localPosition = Camera.main.WorldToScreenPoint(start_pos);
        this.GetComponent<RectTransform>().localPosition -= adj_pos;
        face_cam.Set(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        transform.GetChild(0).GetComponent<Text>().text = "" + hurt_num;
        this.GetComponent<RectTransform>().eulerAngles = Vector3.zero;
    }
	
	void Update () {
        //transform.rotation = Quaternion.LookRotation(face_cam);
        Width_Height.Set(Screen.height * 0.1f * 4, Screen.height * 0.1f);
        this.GetComponent<RectTransform>().sizeDelta = Width_Height;

        screenRect.Set(0f, 0f, Screen.width, Screen.height);
        if (!screenRect.Contains(transform.position))
        {
            Destroy(this.gameObject);
        }

        if (Time.time - up_time >= 0.03f)
        {
            up_time = Time.time;
            movement = up_movement(5f);
            this.GetComponent<RectTransform>().localPosition += movement * 1f;
        }
        /*
        if (Time.time - shake_time >= 0.1f)
        {
            shake_time = Time.time;
            shake = Random.Range(-Screen.width / 100, Screen.width / 100 + 1);
        }
        */
    }

    public Vector3 up_movement(float _up_speed)
    {
        Vector3 _shoot_movement;
        _shoot_movement.x = shake;
        _shoot_movement.y = _up_speed;
        _shoot_movement.z = 0;
        return _shoot_movement;
    }
}
