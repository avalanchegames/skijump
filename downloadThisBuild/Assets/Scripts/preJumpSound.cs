using UnityEngine;
using System.Collections;

public class preJumpSound : MonoBehaviour {

	PlayerStateController PlayerStateManager;
	public AudioClip jump_build_up;
	bool sound_played = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		PlayerStateManager = gameObject.GetComponent <PlayerStateController> ();
		if (PlayerStateManager.getState() == PlayerStateController.playerStates.pre_jump && !sound_played) 
		{
			audio.Stop();
			sound_played = true;
			audio.PlayOneShot(jump_build_up);
		}
	
	}
}
