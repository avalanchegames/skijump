using UnityEngine;
using System.Collections;

public class slowmoLanding : MonoBehaviour {
	
	public AudioClip SoundFile;	// The sound that is played while the player is in slow motion
	
	void OnTriggerEnter( Collider other )
	{
		other.gameObject.GetComponent <PlayerMovement>().slowMo = true;
		other.gameObject.audio.Stop ();
		other.gameObject.audio.clip = SoundFile;
		other.gameObject.audio.Play ();
	}
}
