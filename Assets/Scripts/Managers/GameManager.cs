using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player;
    KeyController keyCon;

	// Use this for initialization
	void Start ()
    {
        keyCon = FindObjectOfType<KeyController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
