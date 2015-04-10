using UnityEngine;
using System.Collections;

public class LandingSound : MonoBehaviour {

	PlayerStateController playerStateManager;
	public AudioClip landing;
	bool sound_played = false;
	
	// Use this for initialization
	void Start () {
		playerStateManager = gameObject.GetComponent <PlayerStateController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerStateManager.GetState() == PlayerStateController.playerStates.landing && !sound_played) 
		{
			audio.Stop();
			sound_played = true;
			audio.PlayOneShot(landing);
		}
		
	}
}
