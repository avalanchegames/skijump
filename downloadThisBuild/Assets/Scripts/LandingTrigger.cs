using UnityEngine;
using System.Collections;

public class LandingTrigger : MonoBehaviour {

	PlayerStateController PlayerStateManager;
	public AudioClip landingSound;
	bool soundTrigger = false;
	


	void OnTriggerEnter( Collider other )
	{
		other.gameObject.GetComponent <PlayerMovement>().landed = true;
		//PlayerStateManager = gameObject.GetComponent<PlayerStateController> ();
		//if (PlayerStateManager.getState () == PlayerStateController.playerStates.landing && !soundTrigger) {
		if (!soundTrigger)
		{
			other.gameObject.audio.Stop ();
			other.gameObject.audio.PlayOneShot (landingSound);
			soundTrigger = true;
		}
	}
}
