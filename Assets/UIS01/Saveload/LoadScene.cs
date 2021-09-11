using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public static bool loadSceneChange = new bool();
   

    SaveAndLoad _saveload;

    private void Awake()
    {
        //_saveload = GameObject.Find("GameObject").GetComponent<SaveAndLoad>();
    }

    // Use this for initialization
    void Start () {
        LoadScene LS;
        LS = GameObject.Find("GameObject").GetComponent<LoadScene>();
        if(LS != null)
        {
           
        }
        else
        loadSceneChange = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Sceneload()
    {
        //讀取json檔案並轉存成文字格式
         StreamReader file = new StreamReader(System.IO.Path.Combine(Application.persistentDataPath, "SavePlayer"));
        string loadJson = file.ReadToEnd();
        file.Close();

        //新增一個物件類型為playerState的變數 loadData
        SaveAndLoad.PlayerState loadData = new SaveAndLoad.PlayerState();

        //使用JsonUtillty的FromJson方法將存文字轉成Json
        loadData = JsonUtility.FromJson<SaveAndLoad.PlayerState>(loadJson);

        foreach (GameObject PlayerObj in GameObject.FindGameObjectsWithTag("Slime"))
        {
            DestroyObject(PlayerObj);
        }

        //驗證用，將sammaru的位置變更為json內紀錄的位置
        SceneManager.LoadScene(loadData.snow, LoadSceneMode.Additive);

        loadSceneChange = true;
        
    }
}
