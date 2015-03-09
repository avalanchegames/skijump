using UnityEngine;
using System.Collections;

public class landingSound : MonoBehaviour {

	PlayerStateController PlayerStateManager;
	public AudioClip landing;
	bool sound_played = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		PlayerStateManager = gameObject.GetComponent <PlayerStateController> ();
		if (PlayerStateManager.getState() == PlayerStateController.playerStates.landing && !sound_played) 
		{
			audio.Stop();
			sound_played = true;
			audio.PlayOneShot(landing);
		}
		
	}
}
