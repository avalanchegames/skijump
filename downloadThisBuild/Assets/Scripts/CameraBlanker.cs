using UnityEngine;
using System.Collections;

// Andrew Robson:
// This script checks the output's frame rate sets the shader to draw black if the frame rate is too low.
// The script needs to be attached each camera.

public class CameraBlanker : MonoBehaviour 
{
	public bool blankFrames = false; // Check this to enable frame blanking.
	public float targetFPS = 59.0f; // Frames will start to be blanked when the frame rate drops below this number.
	public float minimumTimeToForceShow = 2.0f; // After a blanked frame, the frame will not be blanked again until this time has elapsed.
	private float timeSinceLastBlank;
	private Camera attachedCamera;
	public Shader blankingShader;
	
	bool errors = false;
	
	// Use this for initialization
	void Start () 
	{
		attachedCamera = GetComponent<Camera>();
		
		if (attachedCamera == null)
		{
			Debug.LogWarning("This script is not attached to a camera.");
			errors = true;
		}
		
		if (blankingShader == null)
		{
			Debug.LogWarning("The blanking shader has not been set up.");
			errors = true;
		}
		
		if (errors)
		{
			Debug.LogWarning("Cameras will not be blanked on slow frames.");
		}
		
		timeSinceLastBlank = 0.0f;
	}
	
	// Update is called once per frame
	void OnPreRender () 
	{
		if ( (!errors) && (blankFrames))
		{
			if ( ( (1/Time.deltaTime) < targetFPS) && (timeSinceLastBlank > minimumTimeToForceShow) )
			{
				// Blank the frame.
				Debug.Log("Frame blanked out!");
				
				attachedCamera.SetReplacementShader(blankingShader, null);
				
				attachedCamera.clearFlags = CameraClearFlags.SolidColor;
				
				RenderSettings.fog = false;
				
				timeSinceLastBlank = 0.0f;
			}
			else
			{
				// Show the frame
				attachedCamera.ResetReplacementShader();
				
				attachedCamera.clearFlags = CameraClearFlags.Skybox;
				
				RenderSettings.fog = true;
				
				timeSinceLastBlank += Time.deltaTime;
			}
		}
	}

	// Reset the render settings in preparation for the next frame, lest there is a frame following a blank without fog, etc.
	IEnumerator ResetRender()
	{
		yield return new WaitForEndOfFrame();
		 
		if ( timeSinceLastBlank == 0.0f )
		{
			attachedCamera.ResetReplacementShader();
			
			attachedCamera.clearFlags = CameraClearFlags.Skybox;
			
			RenderSettings.fog = true;
		}
	}
}
