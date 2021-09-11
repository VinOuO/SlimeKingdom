using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Slime_Circle_point : MonoBehaviour
{
    public int num_in_party;
    public float speed_circle = 60;
    public float circle_time;
    public int right_circle_num;
    public double aaa;
    public double aa;
    public int c1, c2, c3;
    public bool right_circle_position = false;
    public float r_circle = 6;
    public int c;
    float c_real;
    Vector3 ny;
    Vector2 a1, b1;
    public float P_to_s;
    //繞的圈數要看的方向
    public bool ab = false;
    void Start()
    {
        num_in_party = GameObject.Find("Player").GetComponent<Player>().slime_partied_num;
        c1 = 6;
        c2 = 13;
        c3 = 30;

    }


    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().form_type == 2 || GameObject.Find("Player").GetComponent<Player>().form_type == 5)
        {
            while (GameObject.Find("Point_" + (num_in_party - 1)) == null && num_in_party > 1)
            {
                num_in_party--;
                this.gameObject.name = "Point_" + num_in_party;
            }
            this.gameObject.name = "Point_" + num_in_party;
            if (num_in_party <= c1)
            {
                c = 1;
                P_to_s = 2.5f;
            }
            else if (num_in_party <= c2)
            {
                c = 2;
                P_to_s = 20f;
                ab = true;
            }
            else
            {
                c = 3;
                P_to_s = 7.2f;
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                right_circle_position = false;
            }
            ny.Set(0, -transform.position.y, 0);
            transform.position += ny;
            if (num_in_party != 1 && num_in_party != c1 + 1 && num_in_party != c2 + 1)
            {
                if (GameObject.Find("Point_" + (num_in_party - 1)).GetComponent<Slime_Circle_point>().right_circle_position)
                {
                    get_Circle();
                }
            }
            else
            {
                get_Circle();
            }
        }

    }

    void get_CircleSpeed(float _r_circle, int slime_pre_circle, int pn)
    {
        GameObject player, slime_ahead;
        Vector3 a, b;
        double angle_to_be, angle_now;

        if (num_in_party != 1 && num_in_party != c1 + 1 && num_in_party != c2 + 1)
        {
            player = GameObject.Find("Player");
            slime_ahead = GameObject.Find("Point_" + (num_in_party - 1));
            a = this.transform.position - player.transform.position;
            a.y = 0;

            b = slime_ahead.transform.position - player.transform.position;
            b.y = 0;

            a.Normalize();
            b.Normalize();
            double theta = Math.Acos(Vector3.Dot(a, b));
            angle_now = theta * 180 / Math.PI;

            angle_to_be = 360 / slime_pre_circle;

            aaa = angle_now;
            aa = angle_to_be;

            if (angle_now - angle_to_be > 1.5f)
            {
                speed_circle++;
            }
            else if (angle_now - angle_to_be < -1.5f)
            {
                speed_circle--;
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
            right_circle_position = true;
        }

    }
    void get_Circle()
    {
        c_real = Vector3.Distance(GameObject.Find("Player").transform.position, this.gameObject.transform.position);
        //GetComponent<Rigidbody>().MovePosition(transform.position + 2 * GameObject.Find("Player").GetComponent<Player>().movement);
        if (right_circle_num >= 50)
        {
            right_circle_position = true;
        }

        if (c == 1)//第一圈
        {
            r_circle = 6;
            if (GameObject.Find("Player").GetComponent<Player>().slime_partied_num < c1)
            {
                get_CircleSpeed(r_circle, GameObject.Find("Player").GetComponent<Player>().slime_partied_num, 1);
            }
            else
            {
                get_CircleSpeed(r_circle, c1, 1);
            }

            GameObject.Find("Player").GetComponent<Player>().player_to_slime1 = this.gameObject.transform.position - GameObject.Find("Player").transform.position;
            //繞在規定的軌道
            if (Vector3.Distance(GameObject.Find("Player").transform.position, this.gameObject.transform.position) <= r_circle)
            {
                transform.position += GameObject.Find("Player").GetComponent<Player>().player_to_slime1 * Time.deltaTime;
            }
            else if (Vector3.Distance(GameObject.Find("Player").transform.position, this.gameObject.transform.position) >= r_circle + 0.001f)
            {
                transform.position -= GameObject.Find("Player").GetComponent<Player>().player_to_slime1 * Time.deltaTime;
            }
            transform.RotateAround(GameObject.Find("Player").transform.position, Vector3.up, speed_circle * Time.deltaTime);
        }
        else if (c == 2)//第二圈
        {
            r_circle = 10;
            if (GameObject.Find("Player").GetComponent<Player>().slime_partied_num < c2)
            {
                get_CircleSpeed(r_circle, GameObject.Find("Player").GetComponent<Player>().slime_partied_num - c1, -1);
            }
            else
            {
                get_CircleSpeed(r_circle, c2 - c1, -1);
            }

            GameObject.Find("Player").GetComponent<Player>().player_to_slime1 = this.gameObject.transform.position - GameObject.Find("Player").transform.position;
            //繞在規定的軌道
            if (Vector3.Distance(GameObject.Find("Player").transform.position, this.gameObject.transform.position) <= r_circle)
            {
                transform.position += GameObject.Find("Player").GetComponent<Player>().player_to_slime1 * Time.deltaTime;
            }
            else if (Vector3.Distance(GameObject.Find("Player").transform.position, this.gameObject.transform.position) >= r_circle + 0.001f)
            {
                transform.position -= GameObject.Find("Player").GetComponent<Player>().player_to_slime1 * Time.deltaTime;
            }
            transform.RotateAround(GameObject.Find("Player").transform.position, -Vector3.up, speed_circle * Time.deltaTime);
        }
        else if (c == 3)//第二圈
        {
            r_circle = 15;
            if (GameObject.Find("Player").GetComponent<Player>().slime_partied_num < c3)
            {
                get_CircleSpeed(r_circle, GameObject.Find("Player").GetComponent<Player>().slime_partied_num - c2, 1);
            }
            else
            {
                get_CircleSpeed(r_circle, c3 - c2, 1);
            }

            GameObject.Find("Player").GetComponent<Player>().player_to_slime1 = this.gameObject.transform.position - GameObject.Find("Player").transform.position;
            //繞在規定的軌道
            if (Vector3.Distance(GameObject.Find("Player").transform.position, this.gameObject.transform.position) <= r_circle)
            {
                transform.position += GameObject.Find("Player").GetComponent<Player>().player_to_slime1 * Time.deltaTime;
            }
            else if (Vector3.Distance(GameObject.Find("Player").transform.position, this.gameObject.transform.position) >= r_circle + 0.001f)
            {
                transform.position -= GameObject.Find("Player").GetComponent<Player>().player_to_slime1 * Time.deltaTime;
            }
            transform.RotateAround(GameObject.Find("Player").transform.position, Vector3.up, speed_circle * Time.deltaTime);
        }
    }
}