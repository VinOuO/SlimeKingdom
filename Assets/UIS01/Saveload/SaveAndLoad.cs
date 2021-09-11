using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class SaveAndLoad : MonoBehaviour {

    public GameObject[] slime_type;
    public GameObject slime;
    ISlime_info Slime_Info;
    public static Vector3 ShowVector = Vector3.zero;

    public int sn = 0;

   // public Slime_normal tryy;
    //public int acc;

     SlimeTypeData st;

    PlayerState SavePlayer;
    public Vector3 pos = new Vector3(0, 0, 500);
    int Scencei = 0;
    public string aaa;

    Scene scenenow;
    //UnityEngine.UI.Text PathText;
    // Use this for initialization

    // Use this for initialization
    void Start () {
       // SavePlayer = new PlayerState();
        // PathText = GameObject.Find("PathText").GetComponent<UnityEngine.UI.Text>();
        aaa = "S1";
        



    }
	
	// Update is called once per frame
	void Update () {
        scenenow = SceneManager.GetActiveScene();
        aaa = scenenow.name;
	}

    public void save()
    {
        //PathText.text = Application.persistentDataPath;
        int Snum = 0;
        foreach(GameObject slime in GameObject.FindGameObjectsWithTag("Slime"))
        {
            Snum++;
        }
        sn = Snum;
        //填寫jPlayerState格式的資料..
        PlayerState mySavePlayer = new PlayerState(Snum);

        mySavePlayer.pos = GameObject.Find("Player").transform.position;

        Main_camera CameraSave;
        CameraSave = GameObject.Find("Camera").GetComponent<Main_camera>();
       // mySavePlayer.CameraOffset = CameraSave.offset;
       // ShowVector = CameraSave.offset;
        



        mySavePlayer.snow = scenenow.name;
        mySavePlayer.CameraPos = GameObject.Find("Camera").transform.position;
        int i = 0;
        

        GameObject slime_save;
        if(Snum > 1)
        for(int j = 0; j < Snum-1; j++ )
        {
                
                slime_save = GameObject.Find("Slime_" + (j + 1));
                st = GameObject.Find("Slime_"+(j+1)).GetComponent<SlimeTypeData>();
             

                //SavePlayer.S[j].SType = st.SlimeType;
                //SavePlayer.S[j].pos = GameObject.Find("Slime_" + (j + 1)).transform.position;
                mySavePlayer.ary[j] = st.SlimeType;
               mySavePlayer.slime_pos[j] = GameObject.Find("Slime_" + (j + 1)).transform.position;

            }
       
        

        

        //將SavePlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(mySavePlayer);


        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.persistentDataPath, "SavePlayer"));
        file.Write(saveString);
        file.Close();
    }

    public void load()
    {
        
        //讀取json檔案並轉存成文字格式
        StreamReader file = new StreamReader(System.IO.Path.Combine(Application.persistentDataPath, "SavePlayer"));
        string loadJson = file.ReadToEnd();
        file.Close();

        //新增一個物件類型為playerState的變數 loadData
        PlayerState loadData = new PlayerState();

        //使用JsonUtillty的FromJson方法將存文字轉成Json
        loadData = JsonUtility.FromJson<PlayerState>(loadJson);

        int i  = loadData.Snum - 1 ;
        Debug.Log("num : "+loadData.Snum);
        int slime_type_load;
        for(int j = 0; j < i; j++)
        {
            slime_type_load = loadData.ary[j];
            slime = Instantiate(slime_type[slime_type_load], loadData.slime_pos[j], slime_type[slime_type_load].transform.rotation);
            //slime = Instantiate(slime_type[1], loadData.pos, slime_type[1].transform.rotation);
            //slime = Instantiate(slime_type[slime_type_load], loadData.pos, slime_type[slime_type_load].transform.rotation);
            Slime_Info = slime.gameObject.GetComponent(typeof(ISlime_info)) as ISlime_info;
        }

        //Instantiate(slime_type[0].transform, transform.position - 3 * Vector3.forward + 3 * Vector3.up, slime_type[0].transform.rotation);

        /*GameObject.Find("Camera").transform.position = loadData.CameraPos;
        Main_camera CameraSave;
        CameraSave = GameObject.Find("Camera").GetComponent<Main_camera>();*/
         //CameraSave.offset = loadData.CameraOffset;

        //驗證用，將sammaru的位置變更為json內紀錄的位置
        foreach (GameObject PlayerObj in GameObject.FindGameObjectsWithTag("PPAP"))
        {
            if (PlayerObj.name == "Player")
            {
               // PlayerObj.transform.position = loadData.pos;
                PlayerObj.GetComponent<NavMeshAgent>().Warp(loadData.pos);
                PlayerObj.GetComponent<Player>().slime_partied_num = i;
                /*for (int j = 0; j < i; j++)
                {
                    slime_type_load = loadData.ary[j];
                    slime = Instantiate(slime_type[slime_type_load], loadData.slime_pos[j], PlayerObj.transform.rotation);
                    //slime = Instantiate(slime_type[slime_type_load], loadData.pos, slime_type[slime_type_load].transform.rotation);
                    Slime_Info = slime.gameObject.GetComponent(typeof(ISlime_info)) as ISlime_info;
                }*/
            }
        }
        // GameObject.Find("Player").transform.position = loadData.pos;
       // CameraSave.offset = loadData.CameraOffset;

    }

   /* public class SlimeState
    {
        public string name;
        public int SType;
        public Vector3 pos;
        public float health;
        public SlimeState()
        {
            name = "fail_save";
            pos = Vector3.zero;
            health = 1;
        }
        public SlimeState(string n, Vector3 p, float h)
        {
            name = n;
            pos = p;
            health = h;
        }
    }*/

    public class PlayerState
    {
        public string name;
        public int level;
        public Vector3 pos;
        public string snow;
        public int Snum;
        public int[] ary;
        public Vector3[] slime_pos;
        public Vector3 CameraPos;
        public Vector3 CameraOffset;

        //slime資料
        
        //public SlimeState[] S;
        


       
        public PlayerState() //預設建構
        {
            name = "sam";
            level = 0;
            pos = Vector3.zero;
            snow = "1";
            CameraOffset = Vector3.zero;
           // S = new SlimeState[0];
        }
        public PlayerState(int num) //預設建構
        {
            name = "sam";
            level = 0;
            pos = Vector3.zero;
            snow = "1";
            Snum = num;
          //  S = new SlimeState[num];
            ary = new int[num];
            slime_pos = new Vector3[num];
        }
        public PlayerState(string n, int l, Vector3 p, int num)
        {
            name = n;
            level = l;
            pos = p;
            snow = "1";
            Snum = num;
           // S = new SlimeState[num];
            ary = new int[num];
            slime_pos = new Vector3[num];
        }

    }
}
