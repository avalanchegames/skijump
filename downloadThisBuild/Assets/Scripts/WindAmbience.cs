using UnityEngine;
using System.Collections;

// Script by Tony Jarvis.

public class WindAmbience : MonoBehaviour 
{
	public bool windOn = true;	// Used to determine the wind sound should be playing or not
	bool soundPlayed = true;	// Used to determine if the sound has already been played

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!windOn)	// If the wind is stopped
		{
			StopWind();	// Stop playing the sound
			soundPlayed = false;	// Allows the sound to be restarted later.
		}
		if (windOn && !soundPlayed)	// If the wind is turned on
		{
			PlayWind();	// Play the wind sound
			soundPlayed = true;	// Stops the sound from being triggered infinately
		}
	}

	void StopWind()	// Stops the ambient wind sound
	{
		audio.Stop ();
	}

	void PlayWind()	// Plays the ambient wind sound
	{
		audio.Play ();
	}
}
