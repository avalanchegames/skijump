// --- Plays a sound when the player stops at the end of his jump. 
// Script by Tony Jarvis.

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; // Required for realspace3d

public class FinishedSound : MonoBehaviour 
{
	PlayerStateController playerStateManager;
	public AudioClip finished;	// The sound that will be played when the player reaches the end of their journey.
	bool sound_played = false;	// Used to check wether or not the finished sound has played
	private RealSpace3D.RealSpace3D_AudioSource the_AudioSouce = null;
	
	// Use this for initialization
	void Start ()
	{
		the_AudioSouce = GameObject.Find ("First Person " +
		                                  "Controller").GetComponent ("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;
		
		if(the_AudioSouce == null)
			Debug.LogError("theAudioSource isn't valid");

		playerStateManager = gameObject.GetComponent <PlayerStateController> ();	// Get the player's current state
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerStateManager.GetState() == PlayerStateController.playerStates.finished && !sound_played) // If the playrer is in the finished state and the finish sound has not been played.
		{
			audio.Stop();	// Stop any the player is currently playing.
			sound_played = true;	// Stops the sound being played more than once.
			audio.PlayOneShot(finished); // Play the finish sound only once.
			the_AudioSouce.rs3d_StopSound(1);
		}
	}
}
