using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private ScenesManager scenesManager;

    private void Awake()
    {
        GameObject managerObject = GameObject.Find("ScenesManager");
        if (managerObject != null)
        {
            scenesManager = managerObject.GetComponent<ScenesManager>();
        }
        else
        {
            Debug.LogError("Le GameObject 'ScenesManager' est introuvable.");
        }
    }


    public void Play()
    {
        scenesManager.PlayScene("DemoScene");
    }



    public void Quit()
    {
        scenesManager.Quit();
    }
}
