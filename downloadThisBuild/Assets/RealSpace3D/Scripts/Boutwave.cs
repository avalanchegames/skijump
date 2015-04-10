using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

// reh 01Dec14 uncomment this in Tuscany demo if you want the jet fly over
//using OVR;

[ExecuteInEditMode]

//[AddComponentMenu("")] // don't show in component menu

/// <summary>
/// Boutwave. This is a test drive script to handle the vsRealSpace3D AudioSource(s) & AudioListener
/// demo. Feel free to modify and fool around with.
/// "the journey, is the reward." - Steve Jobs
/// </summary>
public class Boutwave : MonoBehaviour 
{
	enum DIRECTION {
		
		FORWARD,
		BACKWARD,
		UP,
		DOWN,
	};

	private bool 			bJetFlying =			false;
	private bool			bOculusPresent = 		false;
	private DIRECTION 		nState = 				DIRECTION.FORWARD;
	private int				nIndex = 				0;
	private GameObject  	thePickupPrefab;
	private GameObject		theJetPlane;
	private Vector3			theJetStartPosition;

	void Awake() 
	{
		// try to get 60 frames a second
		Application.targetFrameRate = 120;

		// check if Oculus is present
		if(CheckFolderExists (Application.dataPath + "/OVR")) 
		{
			bOculusPresent = true;
		}

		if(Application.isPlaying)
		{			
			// don't do if running tuscany demo
			if(!bOculusPresent)
			{
				// grab the pickup-prefab game object
				thePickupPrefab = GameObject.Find ("pickup-prefab");

				// check if valid
				if(thePickupPrefab == null)
					Debug.LogError("thePickupPrefab isn't valid");
			}

			theJetPlane = GameObject.Find("m346");
			
			// check if valid
			if(theJetPlane != null)
				theJetStartPosition = theJetPlane.transform.position;
		}
	}

	void Update() 
	{	
		if(Application.isPlaying)
		{
			if(bOculusPresent)
			{
//reh 02Dec14 uncomment if you have joypad 
/*
				// use the Oculus gamepad controlelr
				if(OVRGamepadController.GPC_GetAxis((int)OVRGamepadController.Axis.RightTrigger) > 0.0f)
				{
					if(theJetPlane != null)
					{
						bJetFlying = true;
						DoJetFlyOver();
					}
				}
*/				
			}
			else
			{
				if(Input.GetKeyDown("f"))
				{
					bJetFlying = true;
					DoJetFlyOver();
				}
			}


			if(bJetFlying)
			{
				// Rod's cheap motion on the jet plane
				if(theJetPlane != null)
					theJetPlane.transform.Translate(Vector3.forward * Time.deltaTime * 30.0f);
			}

			if(!bOculusPresent)
			{
				// Rod's cheap motion on "the box"
				// "what's in the box!?" - Mills
				if(nIndex < 500)
					nState = DIRECTION.FORWARD;
				if(nIndex > 500 && nIndex < 1000)
					nState = DIRECTION.BACKWARD;
				if(nIndex > 1000 && nIndex < 1100)
					nState = DIRECTION.DOWN;
				if(nIndex > 1100 && nIndex < 1200)
					nState = DIRECTION.UP;
		
				if(nIndex > 1200)
					nIndex = 0;
		
				nIndex++;
		
				if(thePickupPrefab != null)
				{
					switch(nState)
					{
						case DIRECTION.FORWARD:
							thePickupPrefab.transform.Translate(Vector3.forward * Time.deltaTime);
							break;
			
						case DIRECTION.BACKWARD:
							thePickupPrefab.transform.Translate(-(Vector3.forward * Time.deltaTime));
							break;
			
						case DIRECTION.UP:
							thePickupPrefab.transform.Translate(Vector3.up * Time.deltaTime);
							break;			
			
						case DIRECTION.DOWN:
							thePickupPrefab.transform.Translate(-(Vector3.up * Time.deltaTime));
							break;				
					}	
				}
			}
		}
	}

	private bool CheckFolderExists(string sPath)
	{
		if(System.IO.Directory.Exists(sPath))
			return true;
		else
			return false;
	}
	void DoJetFlyOver()
	{
		// set the jet's start position
		theJetPlane.transform.position = theJetStartPosition;
	}
}
