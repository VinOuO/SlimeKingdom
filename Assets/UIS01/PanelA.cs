using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelA : MonoBehaviour {

    public void ButCloseClick()
    {
        UIManger.Instance.ClosePanel("PanelA");
        
    }
}
