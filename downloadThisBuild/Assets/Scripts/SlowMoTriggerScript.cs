using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; // Required for realspace3d

// Script by Norbert Leskovics; modified by Tony Jarvis.
// (AR)This script enables slow motion on the player when they are in contact with the trigger collider and plays a sound when they enter.

public class SlowMoTriggerScript : MonoBehaviour 
{
	public AudioClip soundFile;	// The sound that is played while the player is in slow motion
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
			other.gameObject.audio.clip = soundFile;
			other.gameObject.audio.Play ();
		}
		the_AudioSouce.rs3d_StopSound (0);
		windObject.GetComponent<wind_ambience> ().windOn = false;
		/*the_AudioSouce.rs3d_StopSound (1);
		the_AudioSouce.rs3d_LoadAudioClip("heartbeat");
		the_AudioSouce.rs3d_PlaySound (0);*/
	}
	
	// Runs when a collider leaves this trigger entirely.
	void OnTriggerExit( Collider other )
	{
		if (other.gameObject.GetComponent <PlayerMovement>() != null)
		{
			other.gameObject.GetComponent <PlayerMovement>().slowMo = false;
		}
	}
}
