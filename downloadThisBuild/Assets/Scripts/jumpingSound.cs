using UnityEngine;
using System.Collections;

public class jumpingSound : MonoBehaviour {

	PlayerStateController PlayerStateManager;
	bool sound_played = false;
	public AudioClip jumpingSoundFile;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PlayerStateManager = gameObject.GetComponent <PlayerStateController> ();
		if (PlayerStateManager.getState() == PlayerStateController.playerStates.jumping && !sound_played) 
		{
			sound_played = true;
			audio.Stop();
			audio.clip = jumpingSoundFile;
			audio.Play();
		}
		
	}
}
