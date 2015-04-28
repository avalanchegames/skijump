// The initial sound when before the player starts their ski jump.

// Script by Tony Jarvis.

using UnityEngine;
using System.Collections;

public class StartSound : MonoBehaviour 
{
	PlayerStateController playerStateManager; // Used to check the player's state
	public AudioClip start_sound;	// The sound that will beused.
	bool sound_played = false;	// Used to ensure the sound is only played once.
	
	// Use this for initialization
	void Start () 
	{
		playerStateManager = gameObject.GetComponent <PlayerStateController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerStateManager.GetState() == PlayerStateController.PlayerStates.starting && !sound_played) // If the player's state is starting and the start sound has not been played.
		{
			sound_played = true;	// Stops the sound from being played again.
			audio.Play();	// Play the sound in a loop.
		}
	}
}
