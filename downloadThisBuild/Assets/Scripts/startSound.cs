using UnityEngine;
using System.Collections;

public class startSound : MonoBehaviour {

	PlayerStateController PlayerStateManager;
	public AudioClip start_sound;
	bool sound_played = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PlayerStateManager = gameObject.GetComponent <PlayerStateController> ();
		if (PlayerStateManager.getState() == PlayerStateController.playerStates.starting && !sound_played) 
		{
			audio.clip = start_sound;
			sound_played = true;
			audio.Play();
		}
		
	}
}
