using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message_t : MonoBehaviour {

    public string[] Message;
    int i = 0;
    float Start_Time;
    bool Auto_Play = true;
	void Start ()
    {
        Start_Time = Time.time;
	}
	
	void Update ()
    {
        GetComponent<Text>().text = Message[i];

        if (Time.time - Start_Time >= 10 && i == 0)
        {
            i++;
        }
        if(Input.GetKeyDown(KeyCode.Keypad0)|| Input.GetKeyDown(KeyCode.Keypad1)|| Input.GetKeyDown(KeyCode.Keypad2)|| Input.GetKeyDown(KeyCode.Keypad3)|| Input.GetKeyDown(KeyCode.Keypad4)|| Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            if (i == 1)
            {
                i++;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (i == 2)
            {
                i++;
                Start_Time = Time.time;
            }
        }
        if (Time.time - Start_Time >= 10 && i >= 3)
        {
            i++;
            Start_Time = Time.time;
        }
    }
}
