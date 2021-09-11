using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Broadcast : MonoBehaviour {

    string[] message=new string[6];
    float update_time;

	void Start () {
        clear_broad();
	}
	
	void Update () {
        if (Time.time - update_time >= 3)
        {
            clear_broad();
        }
	}

    void clear_broad()
    {
        for(int i = 0; i < 6; i++)
        {
            message[i] = "";
        }
        print_message();
    }
    public void message_update(string _new_message)
    {
        update_time = Time.time;
        for (int i = 5; i > 0; i--)
        {
            message[i] = message[i - 1];
        }
        message[0] = _new_message;
        print_message();
    }
    void print_message()
    {
        for (int i = 0; i < 6; i++)
        {
            transform.GetChild(i).GetComponent<Text>().text = message[i];
        }
    }
}
