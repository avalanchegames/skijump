using UnityEngine;
using System.Collections;

// Andrew Robson:
// This script checks the output's frame rate sets the shader to draw black if the frame rate is too low.
// The script needs to be attached each camera.

public class CameraBlanker : MonoBehaviour {

	public bool BlankFrames = false;
	public float TargetFPS = 59.0f;
	public float MinimumTimeToForceShow = 2.0f;
	private float TimeSinceLastBlank;
	private Camera AttachedCamera;
	
	public Shader BlankingShader;
	
	bool Errors;
	
	// Use this for initialization
	void Start () {
		AttachedCamera = GetComponent<Camera>();
		
		if ( AttachedCamera == null)
		{
			Debug.LogWarning("This script is not attached to a camera.");
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
		
		TimeSinceLastBlank = 0.0f;
		
	}
	
	// Update is called once per frame
	void OnPreRender () 
	{
		if ( (!Errors) && (BlankFrames))
		{
			if ( ( (1/Time.deltaTime) < TargetFPS) && (TimeSinceLastBlank > MinimumTimeToForceShow) )
			{
				// Blank the frame.
				Debug.Log("Frame blanked out!");
				
				AttachedCamera.SetReplacementShader(BlankingShader, null);
				
				AttachedCamera.clearFlags = CameraClearFlags.SolidColor;
				
				RenderSettings.fog = false;
				
				TimeSinceLastBlank = 0.0f;
			}
			else
			{
				// Show the frame
				AttachedCamera.ResetReplacementShader();
				
				AttachedCamera.clearFlags = CameraClearFlags.Skybox;
				
				RenderSettings.fog = true;
				
				TimeSinceLastBlank += Time.deltaTime;
			}
		}
	}
	
	IEnumerator ResetRender()
	{
		yield return new WaitForEndOfFrame();
		 
		if ( TimeSinceLastBlank == 0.0f )
		{
			AttachedCamera.ResetReplacementShader();
			
			AttachedCamera.clearFlags = CameraClearFlags.Skybox;
			
			RenderSettings.fog = true;
		}
	}
}
