using UnityEngine;
using System.Collections;

public class finishedSound : MonoBehaviour {

	PlayerStateController PlayerStateManager;
	public AudioClip finished;
	bool sound_played = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PlayerStateManager = gameObject.GetComponent <PlayerStateController> ();
		if (PlayerStateManager.getState() == PlayerStateController.playerStates.finished && !sound_played) 
		{
			audio.Stop();
			sound_played = true;
			audio.PlayOneShot(finished);
		}
		
	}
}
