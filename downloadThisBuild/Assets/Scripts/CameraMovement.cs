using UnityEngine;
using System.Collections;

// Andrew Robson:
// A script that moves the OVR cameras forward and back in line with the player's state changing.
// Attach to the OVRCameraController.

public class CameraMovement : MonoBehaviour 
{
	Vector3 cameraMovementVector = new Vector3(0.714f, -1.0f, 1.35f); 
	public float movementDuration = 0.54f;
	Transform thisTransform;
	float positionProgress;
	float prevPositionProgress;
	PlayerStateController stateController;
	
	// Use this for initialization
	void Start () 
	{
		thisTransform = GetComponent<Transform> ();
		positionProgress = 0.0f;
		stateController = GetComponentInParent<PlayerStateController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		prevPositionProgress = positionProgress;
	
		switch(stateController.GetState())
		{
			case PlayerStateController.PlayerStates.starting:
			case PlayerStateController.PlayerStates.slide_down:
			case PlayerStateController.PlayerStates.landing:
			case PlayerStateController.PlayerStates.post_landing:
			case PlayerStateController.PlayerStates.finished:
			{
				PositionBackward();
			}
			break;
			
			case PlayerStateController.PlayerStates.pre_jump:
			case PlayerStateController.PlayerStates.jumping:
			case PlayerStateController.PlayerStates.jumping_wide:
			{
				PositionForward();
			}
			break;
		}
		AssignCameraPosition();
	}
	
	void PositionForward()
	{
		positionProgress += Time.deltaTime;
		if (positionProgress > movementDuration)
		{
			positionProgress = movementDuration;
		}
	}
	
	void PositionBackward()
	{
		positionProgress -= Time.deltaTime;
		if (positionProgress < 0)
		{
			positionProgress = 0;
		}
	}
	
	void AssignCameraPosition()
	{
		float progressChange = positionProgress - prevPositionProgress;
		
		Vector3 cameraDisplacement = cameraMovementVector * progressChange;
		
		Vector3 newCameraPosition = thisTransform.position + cameraDisplacement;
		
		thisTransform.position = newCameraPosition;
	}
}
