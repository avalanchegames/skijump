using UnityEngine;
using System.Collections;

// Andrew Robson:
// A script to output the framerate to the console when desired and get the value from an accessor.

public class FPSCounter : MonoBehaviour 
{
	public bool outputToConsole = false;
	public float outputFrequency = 1.0f; // How often the FPS should be reported. 0 will report every frame.
	
	private float timeElapsed;
	private float FPSCount = 61.0f; // Initial value; assume that everything's OK.
	
	// Use this for initialization
	void Start () 
	{
		timeElapsed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Record FPS for accessor.
		FPSCount = 1.0f/Time.deltaTime;
		
		// Only run console output if desired.
		if (!outputToConsole)
		{
			return;
		}
		
		// Ignore timeElapsed if Frequency is zero.
		
		if (outputFrequency <= 0.0f)
		{
			Debug.Log("FPS: " + (FPSCount).ToString() );
		}
		else
		{
			timeElapsed += Time.deltaTime;
			
			if (timeElapsed > outputFrequency)
			{
				timeElapsed -= outputFrequency;
				
				Debug.Log("FPS: " + (FPSCount).ToString() );
			}
		}
	}
	
	// Getter that returns the last value written.
	public float GetFPS()
	{
		return FPSCount;
	}
}
