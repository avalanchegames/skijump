// Changes the player's state when it lands on the slope and plays a sound to indicate that the player has landed.
// Script by Norbert Leskovics; modified by Tony Jarvis. 
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; // Required for realspace3d

public class LandingTrigger : MonoBehaviour 
{
	PlayerStateController playerStateManager; // Used to check the player's state
	public AudioClip landingSound;	// The sound that will be played when the player collides with the landing trigger
	bool soundTrigger = false;	// Used to check whether or not the landing sound has played
	private RealSpace3D.RealSpace3D_AudioSource the_AudioSouce = null; // Used to play the RealSpace3D audio.
	private GameObject windObject;	// Used to find the object with the source that plays the ambient wind sound
	private WindAmbience windObjectAmbience;	// Used to turn the wind audio on or off.

	// Use this for initialization
	void Start ()
	{
		// Gets the player's realspace3d audio source
		the_AudioSouce = GameObject.Find ("First Person " +
		                                  "Controller").GetComponent ("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;
		
		if(the_AudioSouce == null)
		{
			Debug.LogError("theAudioSource isn't valid");
		}

		// Get the audio source of the wind sound
		windObject = GameObject.Find ("Graphics");
		windObjectAmbience = windObject.GetComponent<WindAmbience> ();
	}

	// Runs when a collider touches this trigger.
	void OnTriggerEnter( Collider other )	// If the landing trigger collides with the player.
	{
		PlayerMovement playerMovementManager = other.gameObject.GetComponent <PlayerMovement>();
		
		if (playerMovementManager != null)
		{
			playerMovementManager.landed = true;	// Change the player's state to landed.
			other.gameObject.audio.Stop (); // Stop any audio the player is currently playing.
			the_AudioSouce.rs3d_PlaySound (1);	// Play the realspace3d sound
			soundTrigger = true;	// Used to ensure the landing sound is only played once.
			windObjectAmbience.windOn = true;	// Turn the ambient wind sound on
		}
	}
}
