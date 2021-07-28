using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;

public class ManagerInteractive : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClickGrid()
    {
        if (GetComponent<SwitchManager>().isOn)
        {
            Debug.Log("IsOFF");
        }
        if (GetComponent<SwitchManager>().isOn==false)
        {
            Debug.Log("IsOn");
        }
    } 
}
