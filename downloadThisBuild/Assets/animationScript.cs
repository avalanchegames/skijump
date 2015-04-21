using UnityEngine;
using System.Collections;

public class animationScript : MonoBehaviour {

	Animator anim;
	PlayerStateController animStateManager;
	PlayerStateController.PlayerStates lastState;
	int framecount;
/*
 * some code i've written for animations before findign a simpler solution, but gonna keep for now just in case 
 * delete this before submitting though, but only after animations are fully working - NL
	int startingHash = Animator.StringToHash("starting");
	int slideHash = Animator.StringToHash("Base Layer.slide down");
	int jumpHash = Animator.StringToHash("Base Layer.jump off");
	int sraightFlyHash = Animator.StringToHash("Base Layer.straight legs flying");
	int legsOutHash = Animator.StringToHash("Base Layer.legs moving to the side");
	int legsOutFlyHash = Animator.StringToHash("Base Layer.legs on the side flying");
	int legsInHash = Animator.StringToHash("Base Layer.legs moving close again");
	int landingHash = Animator.StringToHash("Base Layer.landing");
	int landedHash = Animator.StringToHash("Base Layer.landed");
*/
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		///Animation flyanim = anim.GetComponent<Animation> ();
		//flyanim.wrapMode = WrapMode.Loop;
		anim.SetBool("idle", true);
		anim.SetBool("flying", false);
		anim.SetBool ("sidelegs", false);
		animStateManager = gameObject.GetComponentInParent <PlayerStateController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(animStateManager.GetState().ToString());
		//get the state for animation from the playermovement script
		animStateManager.ChangeState (gameObject.GetComponentInParent <PlayerMovement> ().playerStateManager.GetState()); 

		//change the animations based on the current state of the state machine

		// Check if there is a change.
		if ( lastState != animStateManager.GetState())
		{
			switch (animStateManager.GetState ()) 
			{
				case PlayerStateController.PlayerStates.starting:
				{
					//anim.SetTrigger(startingHash);
					anim.Play("starting", -1, 0f);
				}
				break;
				case PlayerStateController.PlayerStates.slide_down:
				{
					anim.SetBool("idle", false);
					anim.Play("slide down ", -1, 0f);
				}
				break;
				case PlayerStateController.PlayerStates.pre_jump:
				{
					//anim.SetBool("flying", true);
					anim.Play("jump off", -1, 0f);
				}	
				break;
				case PlayerStateController.PlayerStates.jumping:
				{
					anim.SetBool("flying", true);
					anim.Play("straight legs flying ", -1, 0f);
				}
				break;
				case PlayerStateController.PlayerStates.jumping_wide:
				{
					anim.Play("legs on the side flying", -1, 0f);
				}
				break; 
				case PlayerStateController.PlayerStates.landing:
				{
					anim.SetBool("flying", false);
					anim.Play("landing", -1, 0f);
				}
				break;
				case PlayerStateController.PlayerStates.post_landing:
				{
					anim.Play("landed", -1, 0f);
				}
				break;
				case PlayerStateController.PlayerStates.finished:
				{
					anim.SetBool("idle", true);
					anim.Play("starting", -1, 0f);
				}
				break;
			}
		}
		
		lastState = animStateManager.GetState ();
	}
}
