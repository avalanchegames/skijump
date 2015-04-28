using UnityEngine;
using System.Collections;

// Script by Norbert Leskovics.
// Script checks the player's state for changes - upon change, an animation on the Animator is played, and variables in the Animation controller are altered.

public class AnimationScript : MonoBehaviour 
{
	Animator anim;
	PlayerStateController animStateManager;
	PlayerStateController.PlayerStates lastState;
	int framecount;
	PlayerMovement playerMovementManager;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();	//fetch the Animator
		anim.SetBool("idle", true);			//set the bools to their initial values
		anim.SetBool("flying", false);
		anim.SetBool ("sidelegs", false);
		animStateManager = gameObject.GetComponentInParent <PlayerStateController> ();
		playerMovementManager = gameObject.GetComponentInParent <PlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log(animStateManager.GetState().ToString());
		//get the state for animation from the playermovement script
		animStateManager.ChangeState (playerMovementManager.playerStateManager.GetState()); 
		//change the animations based on the current state of the state machine

		// Check if there is a change.
		if (lastState != animStateManager.GetState ()) 
		{
			//change the animations based on the current state of the state machine
			switch (animStateManager.GetState ()) 
			{ 
				case PlayerStateController.PlayerStates.starting:
				{
					//anim.SetTrigger(startingHash);
					anim.Play ("starting", -1, 0f);
				}
				break;
				case PlayerStateController.PlayerStates.slide_down:
				{
					anim.SetBool ("idle", false);
					anim.Play ("slide down ", -1, 0f);
				}
				break;
				case PlayerStateController.PlayerStates.pre_jump:
				{
					//anim.SetBool("flying", true);
					anim.Play ("jump off", -1, 0f);
				}	
				break;
				case PlayerStateController.PlayerStates.jumping:
				{
					anim.SetBool ("flying", true);
					anim.Play ("straight legs flying ", -1, 0f);
					anim.SetBool("sidelegs", false);
				}
				break;
				case PlayerStateController.PlayerStates.jumping_wide:
				{
					anim.SetBool("sidelegs", true);
					anim.Play ("legs on the side flying", -1, 0f);
				}
				break; 
				case PlayerStateController.PlayerStates.landing:
				{
					anim.SetBool ("flying", false);
					anim.Play ("landing", -1, 0f);
				}
				break;
				case PlayerStateController.PlayerStates.post_landing:
				{
					anim.Play ("landed", -1, 0f);
				}
				break;
				case PlayerStateController.PlayerStates.finished:
				{
					anim.SetBool ("idle", true);
					anim.Play ("starting", -1, 0f);
				}
				break;
			}		
		}

		lastState = animStateManager.GetState ();
	}
}
