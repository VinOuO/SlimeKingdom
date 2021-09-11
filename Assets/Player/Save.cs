using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {


    //是否讀檔切換場景
    public bool mySceneChange = LoadScene.loadSceneChange;
    //呼叫音樂
    //AudioManager.instance.PlaySound("throw", this.transform.position);
    void Start () {
		
	}
	
	void Update () {
        //給予讀檔資料
        mySceneChange = LoadScene.loadSceneChange;
        if (mySceneChange)
        {
            LoadScene.loadSceneChange = false;
            SaveAndLoad _saveload;
            _saveload = GameObject.Find("GameObject").GetComponent<SaveAndLoad>();
            _saveload.load();
        }
        //
    }
}
