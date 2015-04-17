using UnityEngine;
using System.Collections;

// Script by Norbert Leskovics; modified by Tony Jarvis.
// (AR)This script enables slow motion on the player when they are in contact with the trigger collider and plays a sound when they enter.

public class SlowMoTriggerScript : MonoBehaviour 
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
	
	// Runs when a collider leaves this trigger entirely.
	void OnTriggerExit( Collider other )
	{
		if (other.gameObject.GetComponent <PlayerMovement>() != null)
		{
			other.gameObject.GetComponent <PlayerMovement>().slowMo = false;
		}
	}
}
