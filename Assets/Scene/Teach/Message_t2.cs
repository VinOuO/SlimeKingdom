using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message_t2 : MonoBehaviour
{

    public string[] Message;
    int i = 0;
    float Start_Time;
    bool Auto_Play = true;
    void Start()
    {
        Start_Time = Time.time;
    }

    void Update()
    {
        GetComponent<Text>().text = Message[i];

        if (Time.time - Start_Time >= 8)
        {
            i++;
            Start_Time = Time.time;
        }
    }
}
