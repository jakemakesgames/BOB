using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlNextManager : MonoBehaviour
{
    public string lvlNextName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(lvlNextName);
    }
}
