using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; // Required for realspace3d

// Script by Norbert Leskovics; modified by Tony Jarvis.
// (AR)This script starts slow motion on the player and plays a sound.

public class SlowmoLanding : MonoBehaviour 
{
	public AudioClip SoundFile;	// The sound that is played while the player is in slow motion
	private RealSpace3D.RealSpace3D_AudioSource the_AudioSouce = null;
	private GameObject windObject;

	// Use this for initialization
	void Start ()
	{
		the_AudioSouce = GameObject.Find ("First Person " +
		                                  "Controller").GetComponent ("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;
		
		if(the_AudioSouce == null)
			Debug.LogError("theAudioSource isn't valid");

		windObject = GameObject.Find ("Graphics");
	}

	// Runs when a collider touches this trigger.
	void OnTriggerEnter( Collider other )
	{
		if (other.gameObject.GetComponent <PlayerMovement>() != null)
		{
			other.gameObject.GetComponent <PlayerMovement>().slowMo = true;
			other.gameObject.audio.Stop ();
			other.gameObject.audio.clip = SoundFile;
			other.gameObject.audio.Play();
		}
		windObject.GetComponent<WindAmbience> ().windOn = false;
		//the_AudioSouce.rs3d_LoadAudioClip("heartbeat");
		//the_AudioSouce.rs3d_PlaySound (0);
	}
}
