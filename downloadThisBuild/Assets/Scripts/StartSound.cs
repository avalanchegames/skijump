// The initial sound when before the player starts their ski jump.

// Script by Tony Jarvis.

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; // Required for realspace3d

public class StartSound : MonoBehaviour 
{
	PlayerStateController playerStateManager;
	public AudioClip start_sound;	// The sound that will beused.
	bool sound_played = false;	// Used to ensure the sound is only played once.
	private RealSpace3D.RealSpace3D_AudioSource the_AudioSouce = null;
	int audioIndex = 1;
	
	void awake()
	{
		if (Application.isPlaying)
		{
			the_AudioSouce = GameObject.Find ("First Person " +
			                                  "Controller").GetComponent ("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;
			
			if(the_AudioSouce == null)
				Debug.LogError("theAudioSource isn't valid");
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		the_AudioSouce = GameObject.Find ("First Person " +
		                                  "Controller").GetComponent ("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;
		
		if(the_AudioSouce == null)
			Debug.LogError("theAudioSource isn't valid");
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerStateManager = gameObject.GetComponent <PlayerStateController> ();	// Get the player's current state.
		if (playerStateManager.GetState() == PlayerStateController.PlayerStates.starting && !sound_played) // If the player's state is starting and the start sound has not been played.
		{
			//audio.clip = start_sound;	// Make the source sound the start sound.
			sound_played = true;	// Stops the sound from being played again.
			audio.Play();	// Play the sound in a loop.
		}
	}
}
