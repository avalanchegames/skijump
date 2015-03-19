using UnityEngine;
using System.Collections;

// Andrew Robson:
// A script to output the framerate to the console.

public class FPSCounter : MonoBehaviour 
{
	public bool Run = false;
	public float OutputFrequency = 1.0f; // How often the FPS should be reported. 0 will report every frame.
	
	private float TimeElapsed;
	
	// Use this for initialization
	void Start () 
	{
		TimeElapsed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Only run if desired.
		if (!Run)
		{
			return;
		}
		
		// Ignore TimeElapsed if Frequency is zero.
		
		if (OutputFrequency <= 0.0f)
		{
			Debug.Log("FPS: " + (1.0f/Time.deltaTime).ToString() );
		}
		else
		{
			TimeElapsed += Time.deltaTime;
			
			if (TimeElapsed > OutputFrequency)
			{
				TimeElapsed -= OutputFrequency;
				
				Debug.Log("FPS: " + (1.0f/Time.deltaTime).ToString() );
			}
		}
	}
}
