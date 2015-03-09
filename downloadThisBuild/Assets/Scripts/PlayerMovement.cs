using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour
{
	private Vector3 movementVector;
	private CharacterController characterController;
	
	private float movementSpeed = 8;
	public float jumpPower = 0.8f;
	public float jumpDelay = 1.0f;
	float jumpStartTime;
	float landingTime;
	bool started;
	public bool jumpDone;				
	public bool jumpEnabled;			//used for checking if jump is enabled at the player!s current state/location 
	public bool landed;					//turn true when the player landed
	public bool finished;				//turn true once the player finished the track
	public bool inShakyBit;				//controls if the player is in the "Danger Zone"
	public bool shaking;				//holds if the player is shaking or not
	const float maxShakeRot = 4.0f;
	const float shakePerFrame = 0.6f;
	bool shakingRight = true;
	float shake;
	PlayerStateController PlayerStateManager;
	OVRCameraController OculusCamera;
	float originalRot;

	void Start()
	{
		PlayerStateManager = gameObject.GetComponent <PlayerStateController> ();
		started = false;
		jumpDone = false;
		jumpEnabled = false;
		landed = false;
		finished = false;
		landingTime = 0.0f;
		inShakyBit = false;
		shaking = false;
		PlayerStateManager.changeState (PlayerStateController.playerStates.starting);
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		OculusCamera = GetComponentInChildren<OVRCameraController>();
		OculusCamera.GetYRotation(ref originalRot);

	}
	
	void Update()
	{
		if (PlayerStateManager.getState () == PlayerStateController.playerStates.slide_down) 
		{
			//what happens when sliding down
			if ((inShakyBit)&&(shaking))
			{
				GamePad.SetVibration(0, 0.3f, 0.3f);
				//do shaky stuff here 
				//float shaketime = Time.time;

				OculusCamera.SetCameraRotatesY(true);


				if (shakingRight)
				{
					shake+=shakePerFrame;
					if (shake>=maxShakeRot)
					{
						shakingRight= false;
						shake = 2*maxShakeRot - shake;
					}
				}
				else 
				{
					shake -= shakePerFrame;
					if (shake <= -maxShakeRot)
					{
						shakingRight = true;
						shake = -2*maxShakeRot-shake;
					}
				}

				//Transform shakyTransform = OculusCamera.gameObject.transform;
				//shakyTransform.Rotate(Vector3.up, 90.0f, Space.World);
				OculusCamera.SetYRotation(shake);
				if (Input.GetButtonDown ("Y") || Input.GetKey("y"))
				{
					//stop shaky stuff from happening
					stopShaking();
					return;
				}

			}
			else 
			{
				GamePad.SetVibration(0, 0.0f, 0.0f);
			}
			if ((!inShakyBit))
			{
				stopShaking();
				return;
			}

		}

	}
	
	void FixedUpdate()
	{
		movementVector.x = Input.GetAxis("LeftJoystickX") * movementSpeed;
		movementVector.z = Input.GetAxis("LeftJoystickY") * movementSpeed;


		
		if (jumpEnabled) {	
			//only do the jump if it is enabled by the trigger
			PlayerStateManager.changeState (PlayerStateController.playerStates.pre_jump);

			if (( (Input.GetButton ("A")) || Input.GetKey("a") ) && !jumpDone)
			{
				//jumpStartTime = Time.time;
				movementVector.y += jumpPower;
				//jumpDone = true;
			}
		}
		
		if (jumpDone) {
			PlayerStateManager.changeState (PlayerStateController.playerStates.jumping);
			jumpStartTime = Time.time;
			Time.timeScale = 0.2f;
		}
		
		if (!jumpEnabled)
		{
			movementVector.y = 0.0f;
		}
		if ( (Input.GetButtonDown ("X") || Input.GetKey("x") ) && !started)
		{
			started = true;	
			PlayerStateManager.changeState (PlayerStateController.playerStates.slide_down);
			rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
			rigidbody.AddForce(0.0f, 0.0f, -100.0f);
		}

		if (landed) 
		{
			Time.timeScale = 1.0f;
			if (landingTime==0.0f) 
			{
				PlayerStateManager.changeState (PlayerStateController.playerStates.landing);
				landingTime = Time.time;
			}
			else if (landingTime!=0.0f)
			{
				PlayerStateManager.changeState (PlayerStateController.playerStates.post_landing);
			}
		}
		
		if (finished) 
		{
			PlayerStateManager.changeState (PlayerStateController.playerStates.finished);
		}

		Debug.Log (PlayerStateManager.getState());
			
		rigidbody.AddForce (movementVector);
	}

	void stopShaking()
	{
		//Debug.Log (shaking.ToString());
		shaking =false;
		shake = originalRot;
		OculusCamera.SetYRotation(originalRot);
	}
	
}
