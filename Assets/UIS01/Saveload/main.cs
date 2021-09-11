using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class main : MonoBehaviour
{
    playerState myPlayer;
    public string myPlayerName = "sammaru";
    public int level = 87;
    public Vector3 pos = new Vector3(0, 0, 500);
    Transform Player;
    UnityEngine.UI.Text PathText;
    // Use this for initialization
    void Start()
    {
        myPlayer = new playerState();
        Player = GameObject.Find("sammaru").transform;
        PathText = GameObject.Find("PathText").GetComponent<UnityEngine.UI.Text>();
    }
    public void save()
    {
        PathText.text = Application.persistentDataPath;

        //填寫jplayerState格式的資料..
        playerState myPlayer = new playerState();
        myPlayer.name = "sammaru";
        myPlayer.level = level;
        myPlayer.pos = GameObject.Find("sammaru").transform.position;

        //將myPlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(myPlayer);

        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.persistentDataPath, "myPlayer"));
        file.Write(saveString);
        file.Close();
    }

    public void load()
    {
        //讀取json檔案並轉存成文字格式
        StreamReader file = new StreamReader(System.IO.Path.Combine(Application.persistentDataPath, "myPlayer"));
        string loadJson = file.ReadToEnd();
        file.Close();

        //新增一個物件類型為playerState的變數 loadData
        playerState loadData = new playerState();

        //使用JsonUtillty的FromJson方法將存文字轉成Json
        loadData = JsonUtility.FromJson<playerState>(loadJson);

        //驗證用，將sammaru的位置變更為json內紀錄的位置
        GameObject.Find("sammaru").transform.position = loadData.pos;
        level = loadData.level;
    }

    // Update is called once per frame
    void Update()
    {
        controller();
    }

    void syncInfo()
    {
        pos = myPlayer.pos;
        myPlayerName = myPlayer.name;
        level = myPlayer.level;
    }

    void controller()
    {
        if (Input.GetKey("w"))
        {
            Player.transform.position += Vector3.forward * 1.5f;
        }
        if (Input.GetKey("s"))
        {
            Player.transform.position += Vector3.back;
        }
        if (Input.GetKey("a"))
        {
            Player.transform.position += Vector3.left;
        }
        if (Input.GetKey("d"))
        {
            Player.transform.position += Vector3.right;
        }
    }

    public void ButtonClik(string key)
    {
        if (key == "w")
        {
            Player.transform.position += Vector3.up * 15f;
            level++;
        }
        if (key == "s")
        {
            Player.transform.position += Vector3.down * 15f;
        }
    }
    public class playerState
    {
        public string name;
        public int level;
        public Vector3 pos;
        public playerState() //預設建構
        {
            name = "sam";
            level = 0;
            pos = Vector3.zero;
        }
        public playerState(string n, int l, Vector3 p)
        {
            name = n;
            level = l;
            pos = p;
        }

    }
}
