using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour
{
	private Vector3 movementVector;
	private CharacterController characterController;
	//leadup animation 1-140
	//actual jump 140-170
	//170-365 still in progress
	//365 - landing
	public float jumpPower = 0.8f;		//this value is applied to the player object when the jump occurs and 
	float jumpStartTime;				//fetches the time when the jump started
	float landingTime;
	bool started;
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
	PlayerStateController PlayerStateManager;	//an instance of the PlayerStateController state machine 
	OVRCameraController OculusCamera;			//an instance of OVRCameraController to handle oculus-related code
	float originalRot;							//stores the original rotation of the camera, in order to be able to switch back to this once the player leaves the shaking part
	
	
	void Start()
	{
		//blurObject = GetComponentInChildren(MotionBlur);
		//GetComponentInChildren<MotionBlur>().enabled = false;
		//GetComponentInChildren<OVRCamera> ().GetComponent<MotionBlur> ().enabled = false;
		//GetComponentInChildren<OVRCameraController> ().Find("CameraLeft");
		setBlur (false);
		PlayerStateManager = gameObject.GetComponent <PlayerStateController> ();
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
				//GamePad.SetVibration(0, 0.3f, 0.3f);
				startVibrate(0.3f, 0.3f);
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
				if (Input.GetButtonDown ("B") || Input.GetKey("b"))
				{
					//stop shaky stuff from happening
					stopShaking();
					return;
				}
				
			}
			else 
			{
				//GamePad.SetVibration(0, 0.0f, 0.0f);
				stopVibrate();
			}
			if ((!inShakyBit))
			{
				stopShaking();
				return;
			}
			
		}
		
		if (landingRumble) {
			//GamePad.SetVibration(0, 0.3f, 0.3f);	
			startVibrate (0.3f, 0.3f);
			
		} 
		else {
			stopVibrate();
		}
		
		if (PlayerStateManager.getState() == PlayerStateController.playerStates.post_landing) {
			
			if (Input.GetButton ("B") || Input.GetKey("b"))
			{
				landingRumble = false;
				return;
			}
		}
		
		
	}
	
	void FixedUpdate()
	{
		//movementVector.x = Input.GetAxis("LeftJoystickX") * movementSpeed;
		//movementVector.z = Input.GetAxis("LeftJoystickY") * movementSpeed;

		//turn slowmo on and off
		if (slowMo) 
		{
			Time.timeScale = timeScaleVal;
			setBlur(true);
				
		} else 
		{
			Time.timeScale = 1.0f;
			setBlur(false);
		}
		
		
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
			//Time.timeScale = timeScaleVal;
			//GetComponentInChildren<MotionBlur>().enabled = true;
			//GetComponentInChildren<OVRCamera> ().GetComponent<MotionBlur> ().enabled = true;
			//setBlur(true);  --- previous blur enabler
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
			//Time.timeScale = 1.0f;
			slowMo =false;
			//GetComponentInChildren<MotionBlur>().enabled = false;
			//GetComponentInChildren<OVRCamera> ().GetComponent<MotionBlur> ().enabled = false;
			//setBlur(false);
			if (landingTime==0.0f) 
			{
				PlayerStateManager.changeState (PlayerStateController.playerStates.landing);
				landingRumble = true;
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
	void startVibrate(float leftIntentsity, float rightIntentsity)
	{
		GamePad.SetVibration(0, leftIntentsity, rightIntentsity);
	}
	
	void stopVibrate()
	{
		GamePad.SetVibration(0, 0.0f, 0.0f);
	}
	
	void setBlur(bool _enabled)
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
