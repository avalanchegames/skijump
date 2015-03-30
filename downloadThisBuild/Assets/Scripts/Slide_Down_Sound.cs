// Plays a sound when the played is making their way down the slope.

using UnityEngine;
using System.Collections;

public class Slide_Down_Sound : MonoBehaviour {

	PlayerStateController PlayerStateManager;
	public AudioClip ski_sound;	// The sound that is played when the player is moving down the slope.
	bool sound_played = false;	// Usedto ensure the sound is only played once.
	ulong delayTime = 25000;	// The amount of time before the sound is played.
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PlayerStateManager = gameObject.GetComponent <PlayerStateController> ();	// Gets the player's current state.
		if (PlayerStateManager.getState() == PlayerStateController.playerStates.slide_down && !sound_played) // If the player's current state is the slide down state and the slide down sound has not been played.
		{
			audio.clip = ski_sound; // Set the current sound to be the slide sown sound.
			sound_played = true;	// Stops the sound from being played more than once.
			audio.Play(delayTime);	// Play the sound continuously after a short delay.
		}
	
	}
}
