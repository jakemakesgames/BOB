using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour
{
    Player player;

	void Start ()
    {
        player = GetComponent<Player>();
	}
	
	void Update ()
    {
        // PLAYER MOVEMENT //
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);

        // FIX ISSUES WITH JUMP NOW WORKING //
    }
}
