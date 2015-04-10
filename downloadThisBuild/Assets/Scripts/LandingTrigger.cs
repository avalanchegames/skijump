// Changes the player's state when it lands on the slope and plays a sound to indicate that the player has landed.

using UnityEngine;
using System.Collections;

public class LandingTrigger : MonoBehaviour {

	PlayerStateController playerStateManager;
	public AudioClip landingSound;	// The sound that will be played when the player collides with the landing trigger
	bool soundTrigger = false;	// Used to check wether or not the landing sound has played
	


	void OnTriggerEnter( Collider other )	// If the landing trigger collides with the player.
	{
		other.gameObject.GetComponent <PlayerMovement>().landed = true;	// Change the player's state to landed.
		//playerStateManager = gameObject.GetComponent<PlayerStateController> ();
		//if (playerStateManager.getState () == PlayerStateController.playerStates.landing && !soundTrigger) {
		if (!soundTrigger) // If the sound has not already been played.
		{
			other.gameObject.audio.Stop (); // Stop any audio the player is currently playing.
			other.gameObject.audio.PlayOneShot (landingSound);	// Play the landing sound once.
			soundTrigger = true;	// Used to ensure the landing sound is only played onbce.
		}
	}
}
