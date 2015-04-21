// --- Plays a sound when the player stops at the end of his jump. 

using UnityEngine;
using System.Collections;

public class finishedSound : MonoBehaviour {

	PlayerStateController playerStateManager;
	public AudioClip finished;	// The sound that will be played when the player reaches the end of their journey.
	bool sound_played = false;	// Used to check wether or not the finished sound has played
	
	// Use this for initialization
	void Start () {
		playerStateManager = gameObject.GetComponent <PlayerStateController> ();	// Get the player's current state
	}
	
	// Update is called once per frame
	void Update () {
		if (playerStateManager.GetState() == PlayerStateController.PlayerStates.finished && !sound_played) // If the playrer is in the finished state and the finish sound has not been played.
		{
			audio.Stop();	// Stop any the player is currently playing.
			sound_played = true;	// Stops the sound being played more than once.
			audio.PlayOneShot(finished); // Play the finish sound only once.
		}
		
	}
}
