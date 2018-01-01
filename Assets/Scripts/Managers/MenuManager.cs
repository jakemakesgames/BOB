using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string firstLevel;
    
    public void PlayTheGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MENU");
    }

    public void QuitTheGame ()
    {
        Application.Quit();
    }
	
}
