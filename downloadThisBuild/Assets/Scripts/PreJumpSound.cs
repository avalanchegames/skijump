// Plays a sound as the player is about to perform their jump.
// Script by Tony Jarvis.
using UnityEngine;
using System.Collections;

public class PreJumpSound : MonoBehaviour 
{
	PlayerStateController playerStateManager;
	public AudioClip jump_build_up;	// The sound of the pre jump.
	bool sound_played = false;	// To check if the sound has been played or not

	// Use this for initialization
	void Start () 
	{
		playerStateManager = gameObject.GetComponent <PlayerStateController> ();	// Get the player's current state.
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerStateManager.GetState() == PlayerStateController.playerStates.pre_jump && !sound_played) //If the player's current state is pre jump and if the pre jump sound has not played
		{
			audio.Stop();	// Stop any sound the player is playing
			sound_played = true;	// Stops the sound from being played again.
			audio.PlayOneShot(jump_build_up);	// Play the pre jump sound once.
		}
	}
}
