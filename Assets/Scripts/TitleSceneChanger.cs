using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneChanger : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetAxis("TitleChange") !=0)
        {
            SceneManager.LoadScene("MENU");
        }
    }
}
