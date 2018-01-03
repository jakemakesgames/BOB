using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysManager : MonoBehaviour
{
    public GameObject door;
    public GameObject key;

    public bool doorOpened;
    public bool hasKey;

	// Use this for initialization
	void Start ()
    {
        doorOpened = false;
        hasKey = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (door = null)
        {
            return;
        }

        if (key = null)
        {
            return;
        }
    }

    public void OpenDoor()
    {
        doorOpened = true;
        door.SetActive(false);

        hasKey = true;
        key.SetActive(false);
    }

    public void ResetObjs()
    {
        doorOpened = false;
        door.SetActive(true);

        hasKey = false;
        key.SetActive(true);
    }
}
