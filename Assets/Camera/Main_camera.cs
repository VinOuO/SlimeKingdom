using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_camera : MonoBehaviour
{

    //相機所要指向的目標
    Transform target;
    //相機跟隨目標的速度
    public float smoothing = 3f;

    Vector3[] cam_pos = new Vector3[8];
    int cam_pos_i = 4;
    float y = 1;

    //offet為相機與player的距離，在相機移動的過程中皆須保持這個距離
    Vector3 offset;
    float dis;
    //offect可伸縮次數
    int offset_p;
    //是否固定螢幕
    bool printscreen;
    Vector3 targetCamPos;

    float Ry = 0;
    Quaternion xQuaternion;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        target = GameObject.Find("Player").transform;
        offset = transform.position - target.position;
        offset.Set(16, 16, -1.5f);
        dis = Vector3.Distance(transform.position, target.position);
        offset_p = 0;
        printscreen = false;
        y = 1f;
        cam_pos[0].Set(1, y, 0);
        cam_pos[1].Set(0.5f, y, 0.5f);
        cam_pos[2].Set(0, y, 1);
        cam_pos[3].Set(-0.5f, y, 0.5f);
        cam_pos[4].Set(-1, y, 0);
        cam_pos[5].Set(-0.5f, y, -0.5f);
        cam_pos[6].Set(0, y, -1);
        cam_pos[7].Set(0.5f, y, -0.5f);

        for(int j = 0; j <= 7; j++)
        {
            cam_pos[j] = cam_pos[j].normalized * dis;
        }
    }

    void Update()
    {
        zoom_inout();

        //按Y切換螢幕移動方式
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (printscreen == true)
            {
                printscreen = false;
            }
            else
            {
                printscreen = true;
            }
        }

        if (printscreen)
        {
            //從player的位置反推回去相機的位置
            targetCamPos = target.position + offset;
            //Lerp為內插函式，意思是從目前相機的位置平順的移動到相機的目標位置
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
        else
        {
            /*
            // 當滑鼠將滑到螢幕外
            if (Input.mousePosition.x + Screen.height / 6 > Screen.height)
            {
                Ry += 10 * Time.deltaTime;
                //xQuaternion = Quaternion.AngleAxis(10, Vector3.up);
                //transform.localRotation *= xQuaternion;
            }
            else if (Input.mousePosition.x - Screen.height / 6 < 0)
            {
                Ry -= 10 * Time.deltaTime;
                //xQuaternion = Quaternion.AngleAxis(10, Vector3.down);
                //transform.localRotation *= xQuaternion;
            }
            transform.rotation = Quaternion.Euler(new Vector3(47, Ry, 0));

            if (Input.mousePosition.x + Screen.height / 6 > Screen.height)
            {

            }
            else if (Input.mousePosition.x - Screen.height / 6 < 0)
            {

            }
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            */
            // 當滑鼠將滑到螢幕外
            //if (Input.mousePosition.y + 200 > Screen.height)
            //transform.position += Vector3.forward * 0.8f;
            //else if (Input.mousePosition.y - 200 < 0)
            //transform.position -= Vector3.forward * 0.8f;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (cam_pos_i == 7)
                {
                    cam_pos_i = 0;
                }
                else
                {
                    cam_pos_i++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (cam_pos_i == 0)
                {
                    cam_pos_i = 7;
                }
                else
                {
                    cam_pos_i--;
                }
            }
            if (Vector3.Distance(transform.position, cam_pos[cam_pos_i]) >= 0.5f)
            {
                transform.position = Vector3.Lerp(transform.position, target.position + cam_pos[cam_pos_i], smoothing * Time.deltaTime);
            }

            transform.LookAt(target);

            offset = transform.position - target.position;
            offset = offset.normalized * dis;

        }
    }

    void zoom_inout()
    {
        //鏡頭遠近
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && offset_p >= -10)
        {
            offset_p -= 1;
            gameObject.transform.GetComponent<Camera>().fieldOfView /= 1.2f;
            gameObject.transform.GetComponent<Camera>().orthographicSize--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && offset_p <= 6)
        {
            offset_p += 1;
            gameObject.transform.GetComponent<Camera>().fieldOfView += 8f;
            gameObject.transform.GetComponent<Camera>().orthographicSize++;
        }
    }

}
