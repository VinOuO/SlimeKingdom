using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNight_GUI : MonoBehaviour {

    Vector2 Width_Height;
    Vector3 FX_position, size, sign_pos;
    public Sprite[] Sun = new Sprite[3];
    string Ouverture_now = "Morning";
    float time = 0;
    void Start () {
        transform.GetChild(4).GetComponent<Image>().sprite = Sun[0];
    }
	
	void Update () {
        Width_Height.Set(Screen.height * 0.05f * 10, Screen.height * 0.05f);
        this.GetComponent<RectTransform>().sizeDelta = Width_Height;

        FX_position.Set(-Screen.width * 0.37f + Screen.width / 2, -Screen.height * 0.11f + Screen.height / 2, 0);
        this.GetComponent<RectTransform>().localPosition = FX_position;

        for(int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                sign_pos.Set(this.GetComponent<DayAndNight>().TimeMorning* transform.GetChild(3).GetComponent<RectTransform>().sizeDelta.x, transform.GetChild(i).GetComponent<RectTransform>().localPosition.y, transform.GetChild(i).GetComponent<RectTransform>().localPosition.z);
            }
            else if (i == 1)
            {
                sign_pos.Set(this.GetComponent<DayAndNight>().TimeAfternoon * transform.GetChild(3).GetComponent<RectTransform>().sizeDelta.x, transform.GetChild(i).GetComponent<RectTransform>().localPosition.y, transform.GetChild(i).GetComponent<RectTransform>().localPosition.z);
            }
            else
            {
                sign_pos.Set(this.GetComponent<DayAndNight>().TimeNight * transform.GetChild(3).GetComponent<RectTransform>().sizeDelta.x, transform.GetChild(i).GetComponent<RectTransform>().localPosition.y, transform.GetChild(i).GetComponent<RectTransform>().localPosition.z);
            }
            transform.GetChild(i).GetComponent<RectTransform>().localPosition = sign_pos;
        }

        if(Ouverture_now != this.GetComponent<DayAndNight>().ouverture)
        {
            Ouverture_now = this.GetComponent<DayAndNight>().ouverture;
            switch (Ouverture_now)
            {
                case "Morning":
                    transform.GetChild(4).GetComponent<Image>().sprite = Sun[0];
                    break;
                case "Afternoon":
                    transform.GetChild(4).GetComponent<Image>().sprite = Sun[1];
                    break;
                case "Night":
                    transform.GetChild(4).GetComponent<Image>().sprite = Sun[2];
                    break;
            }
        }
        if (Time.time - time >= 0.1f)
        {
            time = Time.time;
            sign_pos.Set(this.GetComponent<DayAndNight>().TimeOfToday / this.GetComponent<DayAndNight>().Day_long * transform.GetChild(3).GetComponent<RectTransform>().sizeDelta.x, transform.GetChild(4).GetComponent<RectTransform>().localPosition.y, transform.GetChild(4).GetComponent<RectTransform>().localPosition.z);
            transform.GetChild(4).GetComponent<RectTransform>().localPosition = sign_pos;
        }
        
    }

}
