// *******************************************************************************
// * Copyright (c) 2012,2013,2014 VisiSonics, Inc.
// * This software is the proprietary information of VisiSonics, Inc.
// * All Rights Reserved.
// *
// * © VisiSonics Corporation, 2013-2014
// * VisiSonics Confidential Information
// * Source code provided under the terms of the Software License Agreement 
// * between VisiSonics Corporation and Oculus VR, LLC dated 09/10/2014
// ********************************************************************************
// 
// Original Author: R E Haxton
// $Author$
// $Date$
// $LastChangedDate$
// $Revision$
//
// Purpose:
//
// Comments:
//

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;  // needed for RealSpace3D plug-in
using RealSpace3D;

//
//
// ***** Using TestPrefab ****
//
// * Locate the TestPrefab in the /Resources folder
//	
//* If there are mono behaviour scripts missing on the TestPrefab delete them or if when trying to 
//	use you get an error on the "environment setter" or the audiosource. 
//
//* If you deleted the monobehavior or you wish to change the settings of the TestPrefab, drag the TestPrefab into the scene.
//		
//* In the Hierarchy window click on the TestPrefab
//		
//* Give the TestPrefab the settings you desire. Right now the TestPrefab is tied to a 
// cube you can make your prefab tied to any object. 
//		
// Note: This testprefab sample doesn't assign a sound because it will be handled by 
// script. You could assign an audiosource to be played immediately but be sure to change in the 
// script where PlayOnStart is set to false. 
//
// * Once you have your TestPrefab set. Copy it back to the /Resources folder over the 
// TestPrefab already located there. Delete the TestPrefab from the Hierarchy. 
//
// * Now to test the TestPrefab create an empty component, name it, and addcomponent the prefab 
// sample script to it. The script will instantiate the TestPrefab at a location (you can 
// change this location in the script to where you wish the TestPrefab to be located) indicated in the script. 
// See the script keyboard commands to test it. This is just a sample of what can be 
// done via scripting. 

// This sample script demonstrates a prefab with a realspace3d audio source attached. It is instantiated in the demo 
// on the Start() method. This way you can call the rs3d_PlayOnStart method to set the audio not to play.
// 
// In the Update() method I simulate triggers with the keyboard so as not to have to implement rigidbodies and triggers.
// The sample demonstrates that you can load audio clip(s) dynamically and manipulate them when you so choose.
// 
// RealSpace3D now loads the audio clip(s) from the Resource folder. So, make sure to create a "Resources" folder under "Assets" and place
// your sounds there. 
// 
// I hope this sample is helpful.
//
// Happy coding,
// Rod
//

public class PrefabSample : MonoBehaviour
{
	private bool bDoOnce = true;

	private GameObject thePrefab = null;

	// declaring the realspace3d audio source
	private RealSpace3D.RealSpace3D_AudioSource theAudioSource = null;

	public RealSpace3D.RealSpace3D_AudioSource.SoundOptions theSoundOptions;

	void Start()
	{
		// prefab position in scene
		Vector3 prefabPos = 	new Vector3 (10.0f, 5.0f, 10.0f); // set these coordinats where you wish the sound source to be

		// instantiate the prefab
		thePrefab = 			(GameObject) Instantiate(Resources.Load("TestPrefab"), prefabPos, Quaternion.Euler(0, 180, 0));	

		// grab the realspace3d audio source for later reference
		theAudioSource = 		thePrefab.GetComponent("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;
		
		// check if valid
		if(theAudioSource == null)
		{
			Debug.LogError("theAudioSource isn't valid");
			return;
		}

		theAudioSource.rs3d_PlayOnStart(true);
	}
	
	// Simulating triggers
	void Update ()
	{
		if(Input.GetKeyDown("`"))
		{
			Destroy(thePrefab);
		}

		if(Input.GetKeyDown("l"))
			Debug.LogWarning("Current clip length: " + theAudioSource.rs3d_GetClipLength());
		
		if(Input.GetKeyDown("k"))
		{
			Debug.LogWarning("Sound Source is playing: " + theAudioSource.rs3d_IsPlaying(0));
			Debug.LogWarning("Current Time: " + theAudioSource.rs3d_GetTime());
		}

		if(Input.GetKeyDown("j"))
		{
			theAudioSource.rs3d_SetTime(theAudioSource.rs3d_GetTime() + 20.0f);
		}

		if(Input.GetKeyDown("s"))
		{
			Debug.LogWarning("Current time samples: " + theAudioSource.rs3d_GetTimeSamples());
		}

		if(Input.GetKeyDown("6"))
			AdjustTheVolume(2.0f);
		
		if(Input.GetKeyDown("8"))
			StopTheSound();
		
		if(Input.GetKeyDown("9"))
			PlayTheSound();

		if(Input.GetKeyDown("-"))
			LoadTheSound();
		
		if (Input.GetKeyDown ("=")) 
			LoadTheSounds();
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
		// your sound source must be loaded in a folder labeled "/Resources" 
		// or in a subfolder(s) under, i.e., the following audio clip resides
		// in "Assets/RealSpace3D/Resources/RealSpace3D_Sounds". You do not need to pass 
		// "Assets/RealSpace3D/Resources" Unity knows to look in all /Resources folders
		
		// this method will remove the current audio clip and replace with this one
		theAudioSource.rs3d_LoadAudioClip("RealSpace3D_Sounds/tank2");
	}
	
	void LoadTheSounds()
	{
		// demonstrates loading multiple audio clips by script
		// Note: that in order for the rs3d_LoadAudioClips to work
		// your sound sources must be loaded in a folder labeled "/Resources" 
		// or in a subfolder(s) under, i.e., the following audio clip resides
		// in "Assets/RealSpace3D/Resources/RealSpace3D_Sounds". You do not need to pass 
		// "Assets/RealSpace3D/Resources" Unity knows to look in all /Resources folders
		string [] theSounds = {"RealSpace3D_Sounds/bond0", "RealSpace3D_Sounds/cowboytheme 1", "RealSpace3D_Sounds/tank2", "RealSpace3D_Sounds/rodshornet"};
		
		// this method will load the audioclips to the sound sources clips array
		// each time you call this method the previous audio clips are removed
		// and the new ones are set
		theAudioSource.rs3d_LoadAudioClips(theSounds);
	}
	
	void OnTriggerEnter(Collider other)
	{
		// TBD...Not implemented yet

		// unity allows for trigger events on objects that have colliders and/or rigid bodies
		// you can set triggers and do something with the realspace audiosource
	}
	
	void PlaySound(string sFilename)
	{
		theAudioSource.rs3d_PlaySound(sFilename);
	}

	void PlayTheSound()
	{
		theSoundOptions.fVolume = 		2.5f;
		theSoundOptions.bLoopSound = 	true;
		theSoundOptions.bMuteSound = 	false;
		theSoundOptions.bPlayOnStart = 	true;
		theSoundOptions.bPlay3DSound = 	true;

		// demonstrates changing the sound options via scripting
		theAudioSource.rs3d_SetSoundOptions(theSoundOptions);

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

