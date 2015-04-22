// Changes the player's state when it lands on the slope and plays a sound to indicate that the player has landed.
// Script by Norbert Leskovics; modified by Tony Jarvis. 
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; // Required for realspace3d

public class LandingTrigger : MonoBehaviour 
{
	PlayerStateController playerStateManager;
	public AudioClip landingSound;	// The sound that will be played when the player collides with the landing trigger
	bool soundTrigger = false;	// Used to check whether or not the landing sound has played
	private RealSpace3D.RealSpace3D_AudioSource the_AudioSouce = null;
	private GameObject windObject;
	private WindAmbience windObjectAmbience;

	// Use this for initialization
	void Start ()
	{
		the_AudioSouce = GameObject.Find ("First Person " +
		                                  "Controller").GetComponent ("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;
		
		if(the_AudioSouce == null)
		{
			Debug.LogError("theAudioSource isn't valid");
		}

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
			//other.gameObject.audio.PlayOneShot (landingSound);	// Play the landing sound once.
			//the_AudioSouce.rs3d_StopSound (2);
			the_AudioSouce.rs3d_PlaySound (1);
			//the_AudioSouce.rs3d_LoadAudioClip("Assets/Resources/landing_post_landing_final_resouces.wav");
			soundTrigger = true;	// Used to ensure the landing sound is only played once.
			windObjectAmbience.windOn = true;
		}
	}
}
