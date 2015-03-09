using UnityEngine;
using System.Collections;

public class Slide_Down_Sound : MonoBehaviour {

	PlayerStateController PlayerStateManager;
	public AudioClip ski_sound;
	bool sound_played = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PlayerStateManager = gameObject.GetComponent <PlayerStateController> ();
		if (PlayerStateManager.getState() == PlayerStateController.playerStates.slide_down && !sound_played) 
		{
			audio.clip = ski_sound;
			sound_played = true;
			audio.Play(25000);
		}
	
	}
}
