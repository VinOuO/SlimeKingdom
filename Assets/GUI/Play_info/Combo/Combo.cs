using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour {

    bool is_showing = false;
    public Sprite[] combo_number;
    public int num = 0, num_last = 0;
    public bool num_changed = true;
    Vector3 num_pos_1, num_pos_2, num_pos_3;
    Vector2 num_pos, Width_Height;
    GameObject num1, num2, num3;
    public float jump = 0;
    float time = 0;
    void Start () {
        Hide();
        num1 = transform.GetChild(1).gameObject;
        num2 = transform.GetChild(0).gameObject;
        num3 = transform.GetChild(2).gameObject;
    }

	void Update () {
        if (jump > 0 && Time.time - time >= 0.001f)
        {
            time = Time.time;
            jump -= Screen.height / 100;
        }
        //---------------------------------------------------------------------調數字大小&位置
        Width_Height.Set(Screen.height * 0.1f, Screen.height * 0.1f);
        num1.GetComponent<RectTransform>().sizeDelta = Width_Height;
        num2.GetComponent<RectTransform>().sizeDelta = Width_Height;
        Width_Height.Set(Screen.height * 0.3f, Screen.height * 0.1f);
        num3.GetComponent<RectTransform>().sizeDelta = Width_Height * 0.7f;

        num_pos.Set(Screen.width * 0.85f, Screen.height * 0.8f);
        num_pos_1.Set(-Screen.width / 80 + num_pos.x, num_pos.y - jump, 0);
        num1.GetComponent<RectTransform>().localPosition = num_pos_1;
        num_pos_2.Set(Screen.width / 80 + num_pos.x, num_pos.y - jump, 0);
        num2.GetComponent<RectTransform>().localPosition = num_pos_2;
        num_pos_3.Set(num_pos.x, num_pos.y- Screen.height * 1 / 20, 0);
        num3.GetComponent<RectTransform>().localPosition = num_pos_3;
        //---------------------------------------------------------------------調數字大小&位置
        if (Input.GetKeyDown(KeyCode.N))
        {
            num_add();
        }
    }

    public void num_add()
    {
        if (!is_showing)
        {
            Show();
            is_showing = true;
        }
        num++;
        if (num <= 99)
        {
            num_changed = false;
            num1.GetComponent<Image>().sprite = combo_number[num / 10];
            num2.GetComponent<Image>().sprite = combo_number[num % 10];
            jump = Screen.height / 40;
        }
    }

    public void counting_down()
    {
        num_last = num;
        num = 0;
        if (is_showing)
        {
            Hide();
            is_showing = false;
        }
    }

    public void Hide()
    {
        this.GetComponent<CanvasGroup>().alpha = 0f; //this makes everything transparent
        this.GetComponent<CanvasGroup>().blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    void Show()
    {
        this.GetComponent<CanvasGroup>().alpha = 1f;
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
