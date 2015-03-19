using UnityEngine;
using System.Collections;

// Andrew Robson:
// This script checks the output's frame rate

public class CameraBlanker : MonoBehaviour {

	FPSCounter counter;
	public bool BlankFrames = true;
	public float TargetFPS = 59.0f;
	public int MinimumShownFrames = 3;
	private int FramesSinceBlanked;
	
	public Camera LeftCamera;
	public Camera RightCamera;
	
	public Shader BlankingShader;
	
	bool Errors;
	
	// Use this for initialization
	void Start () {
		counter = GetComponent<FPSCounter>();
		
		
		if (counter == null)
		{
			Debug.LogWarning("FPS counter not found!");
			Errors = true;
		}
		
		if ( (LeftCamera == null) || (RightCamera == null) )
		{
			Debug.LogWarning("One or both cameras are not set for blanking.");
			Errors = true;
		}
		// Only check this if both cameras were set;
		else if (LeftCamera == RightCamera)
		{
			Debug.LogWarning("Cameras for blanking are one and the same.");
			Errors = true;
		}
		
		if (BlankingShader == null)
		{
			Debug.LogWarning("The blanking shader has not been set up.");
			Errors = true;
		}
		
		if (Errors)
		{
			Debug.LogWarning("Cameras will not be blanked on slow frames.");
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( (!Errors) && (BlankFrames))
		{
			if ( (counter.GetFPS() < TargetFPS) && (FramesSinceBlanked > MinimumShownFrames) )
			{
				// Blank the frame.
				Debug.Log("Frame blanked out!");
				
				LeftCamera.SetReplacementShader(BlankingShader, null);
				RightCamera.SetReplacementShader(BlankingShader, null);
				
				LeftCamera.clearFlags = CameraClearFlags.SolidColor;
				RightCamera.clearFlags = CameraClearFlags.SolidColor;
				
				RenderSettings.fog = false;
				
				FramesSinceBlanked = 0;
			}
			else
			{
				// Show the frame
				LeftCamera.ResetReplacementShader();
				RightCamera.ResetReplacementShader();
				
				LeftCamera.clearFlags = CameraClearFlags.Skybox;
				RightCamera.clearFlags = CameraClearFlags.Skybox;
				
				RenderSettings.fog = true;
				
				FramesSinceBlanked++;
			}
		}
	}
}
