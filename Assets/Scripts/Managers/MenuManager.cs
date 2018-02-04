using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string level;
    public string controlsMenu;
    public string backTo;

    public void StartGame()
    {
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ControlsMenu()
    {
        SceneManager.LoadScene(controlsMenu);
    }

    public void Back()
    {
        SceneManager.LoadScene(backTo);
    }
	
}
