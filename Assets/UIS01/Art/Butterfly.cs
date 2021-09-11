using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Butterfly : MonoBehaviour
{
    public GameObject[] pos = new GameObject[6];
    Rect screenRect;
    Vector2 Width_Height;
    Vector3 movement;
    public GameObject cc;
    float angle = 0;
    float time, time2 = -5, time2_2 = 3, time3 = 0;
    void Start()
    {
        movement.Set(Random.Range(-100, 101), Random.Range(-100, 101), 0);
        movement = movement.normalized;
        this.GetComponent<RectTransform>().localPosition += movement;
        Debug.Log(Screen.width + "|" + Screen.height);
    }

    void Update()
    {
        screenRect.Set(0f, 0f, Screen.width, Screen.height);
        if (!screenRect.Contains(transform.position))
        {
            this.GetComponent<RectTransform>().localPosition =  pos[Random.Range(0, 5)].GetComponent<RectTransform>().localPosition;
        }


        Width_Height.Set(Screen.height * 0.1f, Screen.height * 0.1f);
        this.GetComponent<RectTransform>().sizeDelta = Width_Height;
        if (Time.time - time >= 0.03f)
        {
            time = Time.time;
            movement = Shoot_movement(angle);
            this.GetComponent<RectTransform>().localPosition += movement * 6f;
        }
        if (Time.time - time2 >= time2_2)
        {
            time2 = Time.time;
            time2_2 = Random.Range(1, 6) / 2;
            angle = Random.Range(-10, 11);
        }
        if (movement.magnitude != 0)
        {
            rotat(Vector3.up, movement);
        }
        /*
        if (pos.x >= 1 || pos.x <= 0)
        {
            this.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-Screen.width, Screen.width), Random.Range(-Screen.height, Screen.height), 0);
        }
        if (pos.y >= 1 || pos.y <= 0)
        {
            this.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-Screen.width, Screen.width), Random.Range(-Screen.height, Screen.height), 0);
        }
        */
        if (Time.time - time3 >= 0.1f)
        {
            time3 = Time.time;
            if (gameObject.name != "Bee")
            {
                Instantiate(cc, transform.position, transform.rotation, transform.parent);
            }
            else
            {
                Instantiate(cc, transform.position, Quaternion.Euler(this.GetComponent<RectTransform>().eulerAngles - Vector3.forward * 90), transform.parent);
            }
        }
    }

    void rotat(Vector3 _vector_a, Vector3 _vector_b)
    {
        float _angle = 0;

        _angle = Mathf.Acos((_vector_a.x * _vector_b.x + _vector_a.y * _vector_b.y + _vector_a.z * _vector_b.z) / (Vector3.Distance(Vector3.zero, _vector_a) * Vector3.Distance(Vector3.zero, _vector_b))) * 180 / Mathf.PI;
        if (_vector_a.x < _vector_b.x)
        {
            _angle *= -1;
        }
        if (gameObject.name != "Bee")
        {
            this.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, _angle);
        }
        else
        {
            this.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, _angle + 90);
        }
    }
    public Vector3 Shoot_movement(float _angle)
    {
        //Debug.Log(_angle);
        _angle *= -1;
        Vector3 _shoot_movement;
        _shoot_movement.x = movement.x * Mathf.Cos(_angle / 180 * Mathf.PI) - movement.y * Mathf.Sin(_angle / 180 * Mathf.PI);
        _shoot_movement.y = movement.x * Mathf.Sin(_angle / 180 * Mathf.PI) + movement.y * Mathf.Cos(_angle / 180 * Mathf.PI);
        _shoot_movement.z = 0;
        return _shoot_movement;
    }
}
