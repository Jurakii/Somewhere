using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DebugMenu : MonoBehaviour
{
    public GameObject escapeMenus;
    public bool isPaus;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setPause();
        }
    }

    public void setPause()
    {
        if (!isPaus)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        isPaus = !isPaus;
        escapeMenus.SetActive(isPaus);
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
