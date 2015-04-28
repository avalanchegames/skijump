using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; // Required for realspace3d

// Script by Norbert Leskovics; modified by Tony Jarvis.
// (AR)This script starts slow motion on the player and plays a sound.

public class SlowmoLanding : MonoBehaviour 
{
	public AudioClip SoundFile;	// The sound that is played while the player is in slow motion
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
	void OnTriggerEnter( Collider other )
	{
		if (other.gameObject.GetComponent <PlayerMovement>() != null)
		{
			other.gameObject.GetComponent <PlayerMovement>().slowmo = true;	// Turn on the slow motion effects
			other.gameObject.audio.Stop (); // Stop any audio the player is currently playing.
			other.gameObject.audio.clip = SoundFile;	// Make the slow motion sound the new audio clip in the source
			other.gameObject.audio.Play();	// Play the sound
		}
		windObjectAmbience.windOn = false;	// Turn off the wind sound
	}
}
