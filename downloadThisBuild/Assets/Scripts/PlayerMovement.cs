using UnityEngine;
using System.Collections;
using XInputDotNetPure;					//used for the gamepad functions such as rumble and button input detection

public class PlayerMovement : MonoBehaviour
{
	private Vector3 movementVector;		//used for adding jump power to it
	private CharacterController characterController;
	public float jumpPower = 0.8f;		//this value is applied to the player object when the jump occurs and 
	float jumpStartTime;				//fetches the time when the jump started
	float landingTime;
	bool started;						//used for checking if the player actually started sliding down 
	public bool jumpDone;				
	public bool jumpEnabled;			//used for checking if jump is enabled at the player!s current state/location 
	public bool landed;					//turn true when the player landed
	public bool finished;				//turn true once the player finished the track
	public bool inShakyBit;				//controls if the player is in the "Danger Zone"
	public bool shaking;				//holds if the player is shaking or not
	private bool landingRumble;			//controls if the controller rumble feature is turned on
	public float timeScaleVal = 0.2f;	//the amount of slow motion to apply
	const float maxShakeRot = 4.0f;		//maximum shaking rotation allowed
	const float shakePerFrame = 0.6f;	//how many times to shake the oculus camera per frame
	bool shakingRight = true;			//determine if the shaking takes palce to the left or right
	public bool slowMo;					//determines if slow motion is turned on or not
	float shake;						//controls if shaking is turned on or not
	public PlayerStateController playerStateManager;	//an instance of the PlayerStateController state machine 
	OVRCameraController oculusCamera;			//an instance of OVRCameraController to handle oculus-related code
	float originalRot;							//stores the original rotation of the camera, in order to be able to switch back to this once the player leaves the shaking part
	GamePadState padState;							//current state of the controller
	GamePadState padPrevState;						//previous state of the controller
	
	void Start()
	{
		SetBlur (false);
		playerStateManager = gameObject.GetComponent <PlayerStateController> ();	//set up the manager for state machine
		started = false;
		jumpDone = false;
		jumpEnabled = false;
		landed = false;
		finished = false;
		landingTime = 0.0f;
		inShakyBit = false;
		shaking = false;
		landingRumble = false;
		slowMo = false;
		playerStateManager.ChangeState (PlayerStateController.PlayerStates.starting);
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		oculusCamera = GetComponentInChildren<OVRCameraController>();
		oculusCamera.GetYRotation(ref originalRot);
		
	}

	void Update()
	{
		padPrevState = padState;
		padState = GamePad.GetState(0);
		
		if (playerStateManager.GetState () == PlayerStateController.PlayerStates.slide_down) 
		{
			//what happens when sliding down
			if ((inShakyBit)&&(shaking))
			{
				StartVibrate(0.3f, 0.3f);
				//do shaky stuff here 
								
				oculusCamera.SetCameraRotatesY(true);
								
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
				
				oculusCamera.SetYRotation(shake);
				if (Input.GetButtonDown ("B") || Input.GetKey("b"))
				{
					//stop shaky stuff from happening
					StopShaking();
					return;
				}
			}
			else 
			{
				StopVibrate();
			}
			if ((!inShakyBit))
			{
				StopShaking();
				return;
			}
		}
		
		if (landingRumble) {
			//if this boolean is true, turn on the rumble
			StartVibrate (0.3f, 0.3f);
			
		} 
		else {
			//otherwise stop it
			StopVibrate();
		}
		
		if (playerStateManager.GetState() == PlayerStateController.PlayerStates.post_landing) {
			
			if (Input.GetButton ("B") || Input.GetKey("b"))
			{
				landingRumble = false;
				return;
			}
		}
	}
	
	void FixedUpdate()
	{
		//turn slowmo on and off
		if (slowMo) 
		{
			Time.timeScale = timeScaleVal;
			SetBlur(true);
				
		} else 
		{
			Time.timeScale = 1.0f;
			SetBlur(false);
		}
		
		if (jumpEnabled) {	
			//only do the jump if it is enabled by the trigger
			playerStateManager.ChangeState (PlayerStateController.PlayerStates.pre_jump);
			
			if (( (Input.GetButton ("A")) || Input.GetKey("a") ) && !jumpDone)
			{
				movementVector.y += jumpPower;
			}
		}
		
		if (jumpDone) {
			playerStateManager.ChangeState (PlayerStateController.PlayerStates.jumping);
			jumpStartTime = Time.time;
		}
		
		if (!jumpEnabled)
		{
			movementVector.y = 0.0f;
		}
		if ( (Input.GetButtonDown ("X") || Input.GetKey("x") ) && !started)
		{
			started = true;	
			playerStateManager.ChangeState (PlayerStateController.PlayerStates.slide_down);
			rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
			rigidbody.AddForce(0.0f, 0.0f, -100.0f);
		}

		//button detect and state change for legs to the side 
		if (playerStateManager.GetState () == PlayerStateController.PlayerStates.jumping) 
		{
			if ((padPrevState.Buttons.LeftShoulder == ButtonState.Pressed) && (padState.Buttons.LeftShoulder == ButtonState.Pressed) 
			    &&(padPrevState.Buttons.RightShoulder == ButtonState.Pressed) && (padState.Buttons.RightShoulder == ButtonState.Pressed))
			{
				playerStateManager.ChangeState (PlayerStateController.PlayerStates.jumping_wide);
			}
			if ((padPrevState.Buttons.LeftShoulder == ButtonState.Released) && (padState.Buttons.LeftShoulder == ButtonState.Released) 
			    &&(padPrevState.Buttons.RightShoulder == ButtonState.Released) && (padState.Buttons.RightShoulder == ButtonState.Released))
			{
				playerStateManager.ChangeState (PlayerStateController.PlayerStates.jumping);
			}
		}
		
		if (landed) 
		{
			slowMo =false;
			if (landingTime==0.0f) 
			{
				playerStateManager.ChangeState (PlayerStateController.PlayerStates.landing);
				landingRumble = true;
				landingTime = Time.time;
			}
			else if (landingTime!=0.0f)
			{
				playerStateManager.ChangeState (PlayerStateController.PlayerStates.post_landing);
			}
		}
		
		if (finished) 
		{
			playerStateManager.ChangeState (PlayerStateController.PlayerStates.finished);
		}
		
		//Debug.Log (playerStateManager.GetState());
		
		rigidbody.AddForce (movementVector);
	}
	
	void StopShaking()
	{
		//Debug.Log (shaking.ToString());
		shaking =false;
		shake = originalRot;
		oculusCamera.SetYRotation(originalRot);
	}

	//function for starting vibration of the controller
	void StartVibrate(float leftIntentsity, float rightIntentsity)
	{
		GamePad.SetVibration(0, leftIntentsity, rightIntentsity);
	}

	//function for stopping the vibration of the controller
	void StopVibrate()
	{
		GamePad.SetVibration(0, 0.0f, 0.0f);
	}

	//set the blur on or off
	void SetBlur(bool _enabled)
	{
		if (_enabled) 
		{
			GameObject myLeftCam = GameObject.Find("CameraLeft");
			myLeftCam.GetComponent<MotionBlur> ().enabled = true;
			GameObject myRightCam = GameObject.Find("CameraRight");
			myRightCam.GetComponent<MotionBlur> ().enabled = true;
		} 
		else 
		{
			GameObject myLeftCam = GameObject.Find("CameraLeft");
			myLeftCam.GetComponent<MotionBlur> ().enabled = false;
			GameObject myRightCam = GameObject.Find("CameraRight");
			myRightCam.GetComponent<MotionBlur> ().enabled = false;
		}
	}
	
	
}
