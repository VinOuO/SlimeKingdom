using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManger : Singleton<UIManger>
{
    private string UI_GAMEPANEL_ROOT = "Prefabs/GamePanel/";

    public GameObject m_CanvasRoot;

    public Dictionary<string, GameObject> m_panelList = new Dictionary<string, GameObject>();

    private bool CheckCanvasRootIsNull()
    {
        if(m_CanvasRoot == null)
        {
            Debug.LogError("m_CanvasRoot is null, Please in your canvas add UIRootHandler.cs");
            return true;
        }
        return false;
    }

    private bool IsPanelLive(string name)
    {

        return m_panelList.ContainsKey(name);
    }

    public GameObject ShowPanel(string name)
    {
        if(CheckCanvasRootIsNull())
            return null;
        if(IsPanelLive(name))
        {
            Debug.LogErrorFormat("[{0}] is showing, if you want to show , please close first!",name);
            return null;
        }

        GameObject loadGo = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>(UI_GAMEPANEL_ROOT + name);
        if(loadGo == null)
            return null;
        GameObject panel = Utility.GameObjectRelate.InstantiateGameObject(m_CanvasRoot, loadGo);
        panel.name = name;

        m_panelList.Add(name, panel);

        return panel;
        
    }

    public void TogglePanel(string name, bool isOn)
    {
        if(IsPanelLive(name) )
        {
            if (m_panelList[name] != null)
                m_panelList[name].SetActive(isOn);
        }
        else
        {
            Debug.LogErrorFormat("Togglepanel [{0}] not found.", name);
        }
         
    }

    public void ClosePanel(string name)
    {
        if (IsPanelLive(name))
        {
            if (m_panelList[name] != null)
                Object.Destroy(m_panelList[name]);
            m_panelList.Remove(name);
        }
        else
        {
            Debug.LogErrorFormat("Closepanel [{0}] not found.", name);
        }
    }

    public void CloseAllPanel()
    {
        foreach(KeyValuePair<string, GameObject> item in m_panelList)
        {
            if (item.Value != null)
                Object.Destroy(item.Value);
        }
        m_panelList.Clear();
    }

    public Vector2 GetCanvasSize()
    {
        if (CheckCanvasRootIsNull())
            return Vector2.one*-1;

        RectTransform trans = m_CanvasRoot.transform as RectTransform;
        return trans.sizeDelta;
    }


}
