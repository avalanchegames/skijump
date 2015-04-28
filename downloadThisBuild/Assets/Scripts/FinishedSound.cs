// --- Plays a sound when the player stops at the end of his jump. 
// Script by Tony Jarvis.

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; // Required for realspace3d

public class FinishedSound : MonoBehaviour 
{
	PlayerStateController playerStateManager;	// Used to check the player's state
	public AudioClip finished;	// The sound that will be played when the player reaches the end of their journey.
	bool sound_played = false;	// Used to check wether or not the finished sound has played
	private RealSpace3D.RealSpace3D_AudioSource the_AudioSouce = null;	// Used to play the RealSpace3D audio.
	
	// Use this for initialization
	void Start ()
	{
		// Gets the player's realspace3d audio source
		the_AudioSouce = GameObject.Find ("First Person " +
		                                  "Controller").GetComponent ("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;
		
		if (the_AudioSouce == null) 
		{
			Debug.LogError ("theAudioSource isn't valid");
		}

		playerStateManager = gameObject.GetComponent <PlayerStateController> ();	// Get the player's current state
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerStateManager.GetState() == PlayerStateController.PlayerStates.finished && !sound_played) // If the playrer is in the finished state and the finish sound has not been played.
		{
			audio.Stop();	// Stop any audio the player is currently playing.
			sound_played = true;	// Stops the sound being played more than once.
			audio.PlayOneShot(finished); // Play the finish sound only once.
			the_AudioSouce.rs3d_StopSound(1);	// Stop the realspace3d audio from playing
		}
	}
}
