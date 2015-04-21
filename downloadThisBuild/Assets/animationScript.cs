using UnityEngine;
using System.Collections;

public class animationScript : MonoBehaviour {

	Animator anim;
	PlayerStateController animStateManager;
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
	void Start () {
		anim = GetComponent<Animator>();	//fetch the Animator
		anim.SetBool("idle", true);			//set the bools to their initial values
		anim.SetBool("flying", false);
		anim.SetBool ("sidelegs", false);
		animStateManager = gameObject.GetComponentInParent <PlayerStateController> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(animStateManager.GetState().ToString());
		//get the state for animation from the playermovement script
		animStateManager.ChangeState (gameObject.GetComponentInParent <PlayerMovement> ().playerStateManager.GetState()); 
		switch (animStateManager.GetState ()) //change the animations based on the current state of the state machine
		{
		case PlayerStateController.playerStates.starting:
			anim.Play("starting", -1, 0f);
			break;
		case PlayerStateController.playerStates.slide_down:
			anim.SetBool("idle", false);
			anim.Play("slide down ", -1, 0f);
			break;
		case PlayerStateController.playerStates.pre_jump:
			anim.SetBool("flying", true);
			anim.Play("jump off", -1, 0f);
			break;
		case PlayerStateController.playerStates.jumping:
			anim.Play("straight legs flying ", -1, 0f);
			break;
		case PlayerStateController.playerStates.jumping_wide:
			anim.Play("legs on the side flying", -1, 0f);
			break; 
		case PlayerStateController.playerStates.landing:
			anim.SetBool("flying", false);
			anim.Play("landing", -1, 0f);
			break;
		case PlayerStateController.playerStates.post_landing:
			anim.Play("landed", -1, 0f);
			break;
		case PlayerStateController.playerStates.finished:
			anim.SetBool("idle", true);
			anim.Play("starting", -1, 0f);
			break;

		}
	}
}
