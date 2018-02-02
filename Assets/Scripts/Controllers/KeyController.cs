using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject door;
    public GameObject key;

    public bool doorOpen;
    public bool hasKey;

	// Use this for initialization
	void Start ()
    {
        doorOpen = false;
        hasKey = false;

        door.SetActive(true);
        key.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OpenDoor()
    {
        doorOpen = true;
        hasKey = true;

        door.SetActive(false);
        key.SetActive(false);
    }

    public void ResetObjs()
    {
        doorOpen = false;
        hasKey = false;

        door.SetActive(true);
        key.SetActive(true);
    }
}
