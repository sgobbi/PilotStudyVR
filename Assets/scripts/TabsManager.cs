using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TabsManager : MonoBehaviour
{
    public GameObject[] Tabs;
    public Image[] TabButtons; 
    public Sprite ActiveBG, InactiveBG;

    public void SwitchToTab(int TabID)
    {
        foreach(GameObject tab in Tabs)
        {
            tab.SetActive(false);
        }
        Tabs[TabID].SetActive(true); 

        foreach(Image im in TabButtons)
        {
            im.sprite = InactiveBG;
        }
        TabButtons[TabID].sprite = ActiveBG; 
    }
}
