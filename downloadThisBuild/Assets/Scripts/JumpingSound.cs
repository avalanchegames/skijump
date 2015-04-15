// Plays a looping sound while the player is in the jumping state.

using UnityEngine;
using System.Collections;

public class jumpingSound : MonoBehaviour {
	
	PlayerStateController PlayerStateManager;	// Used to identify what state the player is in
	bool sound_played = false;	// Used to check if the sound has already been played.
	public AudioClip jumpingSoundFile;	// The sound that is played while the player is in the jumping state.
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PlayerStateManager = gameObject.GetComponent <PlayerStateController> ();	// Get the player's current state.
		if (PlayerStateManager.GetState() == PlayerStateController.playerStates.jumping && !sound_played && !gameObject.GetComponent <PlayerMovement>().slowMo) // If the player is in the jumping state and has the jumping sound has not already been played
		{
			sound_played = true;	// Stops the sound been played again.
			audio.Stop();	// Stop any sound the player is playing.
			audio.clip = jumpingSoundFile;	// Change the player's source clip to the jumping sound.
			audio.Play();	// Plays the jumping sound in a loop. 
		}
		
	}
}
