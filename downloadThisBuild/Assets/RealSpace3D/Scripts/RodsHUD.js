#pragma strict

var img_on 	: Texture2D;
var img_off : Texture2D;

@HideInInspector
var ourSoundOn : boolean = false;

@HideInInspector
var unityAS : AudioSource[];


@HideInInspector
var vsAS 		: RealSpace3D.RealSpace3D_AudioSource[];

@HideInInspector
var bShiftOn	: boolean = false;


function OnGUI()
{
	// running on mobile and I don't have gestures setup to toggle the hud  
	if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
		ourSoundOn = true;
	
	// add the visisonics text
	GUI.Label(Rect(0, Screen.height - 20, 300, 100), "VisiSonics 3D RealSpace Sound Demo");

	// check if we should be doing VisiSonics 3D sound
	if(ourSoundOn)
	{
		// show 3D sound on symbol
		GUI.color = Color.green;
		GUI.Label(Rect(34, Screen.height - 85, 200, 200), "3D Sound");	
		GUI.Label(Rect(0, (Screen.height - 40) - 100, 200, 200), img_on);	
	}
	else 
	{
		// show 3D sound off symbol
		GUI.color = Color.red;	
		GUI.Label(Rect(34, Screen.height - 85, 200, 200), "3D Sound");
		GUI.Label(Rect(0, (Screen.height - 40) - 100, 200, 200), img_off);
	}
}

function Awake()
{	
	// get the unity and RealSpace3D AudioSources
   	unityAS = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
   	vsAS = 	  FindObjectsOfType(typeof(RealSpace3D.RealSpace3D_AudioSource)) as RealSpace3D.RealSpace3D_AudioSource[];	
}

function Start()
{
//	StopAllVSAudio();

	PlayAllVSAudio();
	ourSoundOn = true;

}

function FixedUpdate()
{
	//
	// adjust volume
	//
	if(Input.GetKeyDown("="))
	{
		vsAudio_VolumeUp();
	}
	
	if(Input.GetKeyDown("-"))
	{
		vsAudio_VolumeDown();
	}
}

function Update()
{
	// handle the keyboard inputs and toggle the 3D sound on/off
	// update the HUD as well
	//
	
	// running on mobile and I don't have gestures setup to toggle the hud  
	if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
		PlayAllVSAudio();
	
	else
	{
		if(Input.GetKeyDown("t"))
		{	
			if(ourSoundOn)
			{
				StopAllVSAudio();
				ourSoundOn = false;
			}
			else
			{
				PlayAllVSAudio();
				ourSoundOn = true;
			}
		}
	}
	
	// turn on individual sound
	if(Input.GetKeyDown("1"))
		PlayIndividualAudio(1);
	if(Input.GetKeyDown("2"))
		PlayIndividualAudio(2);
	if(Input.GetKeyDown("3"))
		PlayIndividualAudio(3);
	if(Input.GetKeyDown("4"))
		PlayIndividualAudio(4);
	if(Input.GetKeyDown("5"))
		PlayIndividualAudio(5);
		
	// reset...turn back on all sounds
	if(Input.GetKeyDown("r"))
		ResetAllSounds();	
		
	if(Input.GetKeyDown("q"))
		Application.Quit();
}

private function PlayAllVSAudio()
{
	for(var vsAudio : RealSpace3D.RealSpace3D_AudioSource in vsAS) 
		vsAudio.rs3d_PlayIn3D(true);
}
	
private function PlayIndividualAudio(nIndex : int)
{
	// first mute all the sounds
	for(var vsAudio : RealSpace3D.RealSpace3D_AudioSource in vsAS) 
		vsAudio.rs3d_MuteSound(true);

	var vsAudio : RealSpace3D.RealSpace3D_AudioSource;

	switch(nIndex)
	{
		case 1:	
			vsAudio = GameObject.Find("pickup-prefab").GetComponent("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;
			break;
			
		case 2:
			vsAudio = GameObject.Find("TestObj2").GetComponent("RealSpace3D_AudioSource")  as RealSpace3D.RealSpace3D_AudioSource;
			break;

		case 3:
			vsAudio = GameObject.Find("TestObj3").GetComponent("RealSpace3D_AudioSource")  as RealSpace3D.RealSpace3D_AudioSource;
			break;

		case 4:
			vsAudio = GameObject.Find("TestObj4").GetComponent("RealSpace3D_AudioSource")  as RealSpace3D.RealSpace3D_AudioSource;
			break;

		case 5:
			vsAudio = GameObject.Find("m346").GetComponent("RealSpace3D_AudioSource") as RealSpace3D.RealSpace3D_AudioSource;
			break;
	}
	
	if(vsAudio != null)
	{
		vsAudio.rs3d_MuteSound(false);
		vsAudio.rs3d_PlayIn3D(true);	
	}	
}
	
private function ResetAllSounds()
{
	// first mute all the sounds
	for(var vsAudio : RealSpace3D.RealSpace3D_AudioSource in vsAS) 
		vsAudio.rs3d_MuteSound(false);	
}
	
private function StopAllVSAudio()
{
	for(var vsAudio : RealSpace3D.RealSpace3D_AudioSource in vsAS) 
		vsAudio.rs3d_PlayIn3D(false);
}

private function vsAudio_VolumeUp()
{
	var _fVolume = 0.0f;
	
	for(var vsAudio : RealSpace3D.RealSpace3D_AudioSource in vsAS)
	{
		_fVolume = vsAudio.rs3d_GetVolume() + 1.0f;
			
		if(_fVolume < 10.0f)
			vsAudio.rs3d_AdjustVolume(_fVolume);	
	} 
}

private function vsAudio_VolumeDown()
{
	var _fVolume = 0.0f;
	
	for(var vsAudio : RealSpace3D.RealSpace3D_AudioSource in vsAS)
	{
		_fVolume = vsAudio.rs3d_GetVolume() - 1.0f;
		
		if(_fVolume > 1.0)
			vsAudio.rs3d_AdjustVolume(_fVolume);	
	} 
}


