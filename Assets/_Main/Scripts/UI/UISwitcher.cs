using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;


public class UISwitcher : MonoBehaviour
{

    [SerializeField] public GameObject menu;
    [SerializeField] public GameObject settings;
    
    void Start()
    {
        menu.SetActive(true);
        settings.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
            }
            else if (settings.activeSelf)
            {
                
            }
            else
            {
                menu.SetActive(true);
            }
        }
    }

    public void PlayGame()
    {

    }

    public void Switch()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
            settings.SetActive(true);
        }
        else if (settings.activeSelf)
        {
            settings.SetActive(false);
            menu.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
