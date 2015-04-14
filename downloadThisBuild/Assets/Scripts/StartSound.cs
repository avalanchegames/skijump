﻿// The initial sound when before the player starts their ski jump.

using UnityEngine;
using System.Collections;

public class StartSound : MonoBehaviour {

	PlayerStateController playerStateManager;
	public AudioClip start_sound;	// The sound that will beused.
	bool sound_played = false;	// Used to ensure the sound is only played once.
	
	// Use this for initialization
	void Start () {
		playerStateManager = gameObject.GetComponent <PlayerStateController> ();	// Get the player's current state.
	}
	
	// Update is called once per frame
	void Update () {
		if (playerStateManager.GetState() == PlayerStateController.playerStates.starting && !sound_played) // If the player's state is starting and the start sound has not been played.
		{
			audio.clip = start_sound;	// Make the source sound the start sound.
			sound_played = true;	// Stops the sound from being played again.
			audio.Play();	// Play the sound in a loop.
		}
		
	}
}
