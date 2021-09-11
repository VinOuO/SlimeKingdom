using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelMain : MonoBehaviour {

    public void OnBtnShowClick()
    {
        UIManger.Instance.ShowPanel("PanelA");
        
    }

    public void starGame()
    {
        SceneManager.LoadScene("Main");
        
    }
}
