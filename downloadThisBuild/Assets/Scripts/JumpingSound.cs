// Plays a looping sound while the player is in the jumping state.
// Script by Tony Jarvis.

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; // Required for realspace3d

public class JumpingSound : MonoBehaviour 
{	
	PlayerStateController playerStateManager;	// Used to identify what state the player is in
	bool sound_played = false;	// Used to check if the sound has already been played.
	public AudioClip jumpingSoundFile;	// The sound that is played while the player is in the jumping state.
	private RealSpace3D.RealSpace3D_AudioSource the_AudioSouce = null;
	private GameObject windObject;
	
	// Use this for initialization
	void Start ()
	{
		playerStateManager = gameObject.GetComponent <PlayerStateController> ();	// Get the player's current state.
		the_AudioSouce = GameObject.Find ("First Person " +
		                                  "Controller").GetComponent ("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;
				if(the_AudioSouce == null)
			Debug.LogError("theAudioSource isn't valid");

		windObject = GameObject.Find ("Graphics");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerStateManager.GetState() == PlayerStateController.playerStates.jumping && !sound_played && !gameObject.GetComponent <PlayerMovement>().slowMo) // If the player is in the jumping state and has the jumping sound has not already been played
		{
			sound_played = true;	// Stops the sound been played again.
			audio.Stop();	// Stop any sound the player is playing.
			audio.clip = jumpingSoundFile;	// Change the player's source clip to the jumping sound.
			audio.Play();	// Plays the jumping sound in a loop.
			windObject.GetComponent<WindAmbience> ().windOn = true;
			//the_AudioSouce.rs3d_LoadAudioClip("Clothes in wind (hard)");
			//the_AudioSouce.rs3d_PlaySound (0);
		}
	}
}
