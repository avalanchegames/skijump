using UnityEngine;
using System.Collections;

// Script by Tony Jarvis.

public class WindAmbience : MonoBehaviour 
{
	public bool windOn = true;
	bool soundPlayed = true;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!windOn)
		{
			StopWind();
			soundPlayed = false;
		}
		if (windOn && !soundPlayed)
		{
			PlayWind();
			soundPlayed = true;
		}
	}

	void StopWind()
	{
		audio.Stop ();
	}

	void PlayWind()
	{
		audio.Play ();
	}
}
