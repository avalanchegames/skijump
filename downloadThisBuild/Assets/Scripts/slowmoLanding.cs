using UnityEngine;
using System.Collections;

// Script by Norbert Leskovics; modified by Tony Jarvis.
// (AR)This script starts slow motion on the player and plays a sound.

public class SlowmoLanding : MonoBehaviour 
{
	public AudioClip soundFile;	// The sound that is played while the player is in slow motion
	
	// Runs when a collider touches this trigger.
	void OnTriggerEnter( Collider other )
	{
		if (other.gameObject.GetComponent <PlayerMovement>() != null)
		{
			other.gameObject.GetComponent <PlayerMovement>().slowMo = true;
			other.gameObject.audio.Stop ();
			other.gameObject.audio.PlayOneShot (soundFile);
		}
	}
}
