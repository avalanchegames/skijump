using UnityEngine;
using System.Collections;

// Andrew Robson:
// A script to output the framerate to the console when desired and get the value from an accessor.

public class FPSCounter : MonoBehaviour 
{
	public bool OutputToConsole = false;
	public float OutputFrequency = 1.0f; // How often the FPS should be reported. 0 will report every frame.
	
	private float TimeElapsed;
	private float FPSCount = 61.0f; // Initial value assume that everything's OK.
	
	// Use this for initialization
	void Start () 
	{
		TimeElapsed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Record FPS for accessor.
		FPSCount = 1.0f/Time.deltaTime;
		
		// Only run console output if desired.
		if (!OutputToConsole)
		{
			return;
		}
		
		// Ignore TimeElapsed if Frequency is zero.
		
		if (OutputFrequency <= 0.0f)
		{
			Debug.Log("FPS: " + (FPSCount).ToString() );
		}
		else
		{
			TimeElapsed += Time.deltaTime;
			
			if (TimeElapsed > OutputFrequency)
			{
				TimeElapsed -= OutputFrequency;
				
				Debug.Log("FPS: " + (FPSCount).ToString() );
			}
		}
	}
	
	public float GetFPS()
	{
		return FPSCount;
	}
}
