using UnityEngine;
using System.Collections;

public class SlowMoLanding : MonoBehaviour {
	
	public AudioClip soundFile;	// The sound that is played while the player is in slow motion
	
	void OnTriggerEnter( Collider other )
	{
		other.gameObject.GetComponent <PlayerMovement>().slowMo = true;
		other.gameObject.audio.Stop ();
		other.gameObject.audio.PlayOneShot (soundFile);
	}
}