// Plays a sound when the played is making their way down the slope.
// Script by Tony Jarvis.
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; // Required for realspace3d

public class SlideDownSound : MonoBehaviour 
{
	PlayerStateController playerStateManager;
	public AudioClip ski_sound;	// The sound that is played when the player is moving down the slope.
	bool sound_played = false;	// Used to ensure the sound is only played once.
	ulong delayTime = 25000;	// The amount of time before the sound is played.
	private RealSpace3D.RealSpace3D_AudioSource the_AudioSouce = null;

	// Use this for initialization
	void Start ()
	{
		the_AudioSouce = GameObject.Find ("First Person " +
		                                  "Controller").GetComponent ("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;
		
		if(the_AudioSouce == null)
		{
			Debug.LogError("theAudioSource isn't valid");
		}

		playerStateManager = gameObject.GetComponent <PlayerStateController> ();	// Gets the player's current state.
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerStateManager.GetState() == PlayerStateController.PlayerStates.slide_down && !sound_played) // If the player's current state is the slide down state and the slide down sound has not been played.
		{
			//audio.clip = ski_sound; // Set the current sound to be the slide sown sound.
			sound_played = true;	// Stops the sound from being played more than once.
			//the_AudioSouce.rs3d_LoadAudioClip("Assets/Audio/new sfx/Resources/buildupjump3.wav");
			//audio.Play(delayTime);	// Play the sound continuously after a short delay.
			the_AudioSouce.rs3d_PlaySound(0);
		}
	}
}
