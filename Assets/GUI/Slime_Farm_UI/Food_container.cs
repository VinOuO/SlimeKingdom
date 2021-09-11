using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_container : MonoBehaviour {

    /// <summary>
    /// 飼料的種類
    /// </summary>
    public int type;
    /// <summary>
    /// 飼料的數量
    /// </summary>
    public int food_amount = 50;
    /// <summary>
    /// 是否有史萊姆在吃飼料
    /// </summary>
    public bool is_giving_food = false;
    public bool is_cooling = false;
    float time_fill = 0;

    GameObject player;

    Vector2 size;

	void Start () {
        player = GameObject.Find("Player");
	}
	
	void Update () {
        size.Set(100, food_amount);
        this.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = size;
        if (Time.time - time_fill <= 10)
        {
            is_cooling = true;
        }
        else
        {
            is_cooling = false;
        }
    }

    public void OnBut_foodDecrease()
    {
        if (!is_giving_food)
        {
            if (food_amount >= 1)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (food_amount >= 5)
                    {
                        player.GetComponent<Player>().food_num[type] += 5;
                        food_amount -= 5;
                    }
                    else
                    {
                        player.GetComponent<Player>().food_num[type] += food_amount;
                        food_amount -= food_amount;
                    }
                }
                else
                {
                    player.GetComponent<Player>().food_num[type] += 1;
                    food_amount -= 1;
                }
                time_fill = Time.time;
            }
        }
    }

    public void OnBut_foodIncrease()
    {
        if (!is_giving_food)
        {
            if (food_amount <= 99 && player.GetComponent<Player>().food_num[type] >= 1)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (food_amount <= 95)
                    {
                        if (player.GetComponent<Player>().food_num[type] >= 5)
                        {
                            player.GetComponent<Player>().food_num[type] -= 5;
                            food_amount += 5;
                        }
                        else
                        {
                            food_amount += player.GetComponent<Player>().food_num[type];
                            player.GetComponent<Player>().food_num[type] -= player.GetComponent<Player>().food_num[type];
                        }
                    }
                    else
                    {
                        if (player.GetComponent<Player>().food_num[type] >= (100 - food_amount))
                        {
                            player.GetComponent<Player>().food_num[type] -= (100 - food_amount);
                            food_amount += (100 - food_amount);
                        }
                        else
                        {
                            food_amount += player.GetComponent<Player>().food_num[type];
                            player.GetComponent<Player>().food_num[type] -= player.GetComponent<Player>().food_num[type];
                        }
                    }
                }
                else
                {
                    player.GetComponent<Player>().food_num[type] -= 1;
                    food_amount += 1;
                }
                time_fill = Time.time;
            }
        }
    }
}
