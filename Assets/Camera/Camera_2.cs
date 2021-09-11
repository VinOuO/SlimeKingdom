using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class Camera_2 : MonoBehaviour
{

    public Camera firstPersonCamera, overheadCamera;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 0.1f;
    public float sensitivityY = 0.1f;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationX = 0F;
    float rotationY = 0F;

    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0F;

    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0F;

    public float frameCounter = 20;

    Quaternion originalRotation, stadic;

    GameObject player;
    Quaternion xQuaternion;

    public Vector3 aaa;
    Vector3 dis;
    void Start()
    {
        player = GameObject.Find("Player");
        dis = transform.position - player.transform.position;

        firstPersonCamera = gameObject.GetComponent<Camera>();
        overheadCamera = Camera.main;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.freezeRotation = true;
        }
        originalRotation = transform.localRotation;
        stadic = transform.localRotation;
    }

    void Update()
    {
        try
        {
            if (player.GetComponent<Player>().move_way == "2.5D")
            {
                ShowOverheadView();
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                ShowFirstPersonView();
                transform.localRotation = stadic;
                originalRotation = transform.localRotation;
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        catch
        {
            firstPersonCamera = gameObject.GetComponent<Camera>();
            overheadCamera = Camera.main;
        }
        //--------------------------------------------------------------------FPS
        transform.position = player.transform.position;
        if (player.GetComponent<Player>().move_way == "FPS" && player.GetComponent<Player>().can_move)
        {
            aaa = transform.localEulerAngles;
            aaa.x = Input.GetAxis("Mouse X");
            if (Input.GetKey(KeyCode.Q)|| Input.GetKey(KeyCode.E))//把false去掉
            {
                /*
                rotAverageY = 0f;
                rotAverageX = 0f;


                //rotationY += Input.GetAxis("Mouse Y") * 2;
                //rotationY += 1 * 2;
                if (rotationX <= 1 || Input.GetAxis("Mouse X") < 0)
                {
                   // rotationX += Input.GetAxis("Mouse X") * 0.3f;
                    //rotationX += 1 * 0.3f;
                }
                else if (rotationX >= -1 || Input.GetAxis("Mouse X") > 0)
                {
                    //rotationX += Input.GetAxis("Mouse X") * 0.3f;
                    //rotationX += 1 * 0.3f;
                }

                rotArrayY.Add(rotationY);
                rotArrayX.Add(rotationX);

                if (rotArrayY.Count >= frameCounter)
                {
                    rotArrayY.RemoveAt(0);
                }
                if (rotArrayX.Count >= frameCounter)
                {
                    rotArrayX.RemoveAt(0);
                }

                for (int j = 0; j < rotArrayY.Count; j++)
                {
                    rotAverageY += rotArrayY[j];
                }
                for (int i = 0; i < rotArrayX.Count; i++)
                {
                    rotAverageX += rotArrayX[i];
                }

                rotAverageY /= rotArrayY.Count;
                rotAverageX /= rotArrayX.Count;

                rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
                rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

                Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
                */
                if(Input.GetKey(KeyCode.Q))
                    xQuaternion = Quaternion.AngleAxis(-1.3f, Vector3.up);
                if (Input.GetKey(KeyCode.E))
                    xQuaternion = Quaternion.AngleAxis(1.3f, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                rotationY = 0;
                rotationX = 0;
            }
            if (Input.GetKey(KeyCode.Mouse1) && false)//把true去掉
            {
                rotAverageX = 0f;

                rotationX += Input.GetAxis("Mouse X") * sensitivityX;

                rotArrayX.Add(rotationX);

                if (rotArrayX.Count >= frameCounter)
                {
                    rotArrayX.RemoveAt(0);
                }
                for (int i = 0; i < rotArrayX.Count; i++)
                {
                    rotAverageX += rotArrayX[i];
                }
                rotAverageX /= rotArrayX.Count;

                rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

                Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
                //transform.parent.transform.localRotation = originalRotation * xQuaternion;
            }
            if (Input.GetKey(KeyCode.E) && false)
            {
                rotAverageX = 0f;

                rotationX += Input.GetAxis("Mouse X") * sensitivityX;

                rotArrayX.Add(rotationX);

                if (rotArrayX.Count >= frameCounter)
                {
                    rotArrayX.RemoveAt(0);
                }
                for (int i = 0; i < rotArrayX.Count; i++)
                {
                    rotAverageX += rotArrayX[i];
                }
                rotAverageX /= rotArrayX.Count;

                rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

                Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
                //transform.parent.transform.localRotation = originalRotation * xQuaternion;
            }
            else if (false)//變else
            {
                rotAverageY = 0f;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

                rotArrayY.Add(rotationY);

                if (rotArrayY.Count >= frameCounter)
                {
                    rotArrayY.RemoveAt(0);
                }
                for (int j = 0; j < rotArrayY.Count; j++)
                {
                    rotAverageY += rotArrayY[j];
                }
                rotAverageY /= rotArrayY.Count;

                rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);

                Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
                transform.parent.transform.localRotation = originalRotation * yQuaternion;
            }
        }
        //--------------------------------------------------------------------FPS
    }


    public void ShowOverheadView()
    {
        firstPersonCamera.tag = "Untagged";
        overheadCamera.tag = "MainCamera";
        firstPersonCamera.enabled = false;
        overheadCamera.enabled = true;
    }

    public void ShowFirstPersonView()
    {
        firstPersonCamera.tag = "MainCamera";
        overheadCamera.tag = "Untagged";
        firstPersonCamera.enabled = true;
        overheadCamera.enabled = false;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
