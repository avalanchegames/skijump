using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;  // needed for RealSpace3D plug-in

//
// the following is a sample of c# scripting with the RealSpace3D AudioSource plug-in
// please refer to the API section of the reference manual for more API possibilities
//
// currently this sample script is attached to the same place the realSpace3d audio listener is but, 
// you can write and place your scripts where ever you wish.
//

[ExecuteInEditMode]		// allows to run in edit mode

public class Sample : MonoBehaviour
{
	private int nCount = 0;

	// declaring the realspace3d audio source
	private RealSpace3D.RealSpace3D_AudioSource theAudioSource = null;

	void Awake()
	{
		Debug.LogWarning ("Awake in Sample called");

		if(Application.isPlaying)
		{
			// the pickup-prefab is the floating cube in the vsDemo
			// this will find the game object you wish and get the realspace3d
			// audio source component that is attached to it
			theAudioSource = GameObject.Find("pickup-prefab").GetComponent("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;

			// check if valid
			if(theAudioSource == null)
				Debug.LogError("theAudioSource isn't valid");
		}
	}

	// Use this for initialization
	void Start()
	{
		if(Application.isPlaying)
		{
			// if you didn't deselect the "Play On Start" inspector option
			// this will stop the playing of the audio source
			theAudioSource.rs3d_StopSound();
		}
	}
	
	void Update ()
	{
		// you can do whatever you like here...it will be called once per frame
		// here I will just use the keyboard to trigger events. Note: you can use 
		// the Unity OnTriggerEnter, or other Trigger methods, refer to the Unity documentation

		//
		// Note: you can write your own functions and replace these and test your own...have fun!
		//

		if(Input.GetKeyDown("6"))
			AdjustTheVolume(2.0f);

		if(Input.GetKeyDown("8"))
			StopTheSound();

		if(Input.GetKeyDown("9"))
			PlayTheSound();

		if(Input.GetKeyDown("0"))
		{
			PlayTheSoundByIndex(nCount);

			nCount++;

			if(nCount >= 3)
				nCount = 0;
		}

		if(Input.GetKeyDown("-"))
			LoadTheSound();

		if (Input.GetKeyDown ("=")) 
			LoadTheSounds ();
	}

	void FixedUpdate()
	{
		// if your game object is a rigid body or has physics you can update here
	}

	void AdjustTheVolume(float fVolume)
	{
		// can get the current volume and add or subtract
		float fCurrentVolume = theAudioSource.rs3d_GetVolume();

		fCurrentVolume += fVolume;

		theAudioSource.rs3d_AdjustVolume(fCurrentVolume);	

		// or you can just set the volume to whatever you wish
		theAudioSource.rs3d_AdjustVolume(5.0f);
	}

	void LoadTheSound()
	{
		// demonstrates loading one audio clip by script
		// Note: that in order for the rs3d_LoadAudioClip to work
		// your sound source must be loaded in the folder "Assets/Resources" 
		// or in a subfolder(s) under, i.e., the following audio clip resides
		// in "Assets/Resources/RealSpace3D_Sounds". You do not need to pass 
		// "Assets/Resources" Unity knows to look there

		// this method will remove the current audio clip and replace with this one
		theAudioSource.rs3d_LoadAudioClip("RealSpace3D_Sounds/bond0");
	}

	void LoadTheSounds()
	{
		// demonstrates loading audio clips by script
		// Note: that in order for the rs3d_LoadAudioClips to work
		// your sound sources must be loaded in the folder "Assets/Resources" 
		// or in a subfolder(s) under, i.e., the following audio clips reside
		// in "Assets/Resources/RealSpace3D_Sounds". You do not need to pass 
		// "Assets/Resources" Unity knows to look there
		string [] theSounds = {"RealSpace3D_Sounds/bond0", "RealSpace3D_Sounds/cowboytheme 1", "RealSpace3D_Sounds/tank2", "RealSpace3D_Sounds/rodshornet"};

		// this method will load the audioclips to the sound sources clips array
		// each time you call this method the previous audio clips are removed
		// and the new ones are set
		theAudioSource.rs3d_LoadAudioClips(theSounds);
	}

	void OnTriggerEnter(Collider other)
	{
		// TBD...not implemented yet

		// unity allows for trigger events on objects that have colliders and/or rigid bodies
		// you can set triggers and do something with the realspace audiosource
	}

	void PlayTheSound()
	{
		// play the audio source in realspace 3d
		theAudioSource.rs3d_SetPlay3DSound(true);

		// play the audio source
		theAudioSource.rs3d_PlaySound();
	}

	void PlayTheSoundByIndex(int nAudioClipIndex)
	{
		theAudioSource.rs3d_PlaySound(nAudioClipIndex);
	}

	void StopTheSound()
	{
		theAudioSource.rs3d_StopSound();
	}
}

