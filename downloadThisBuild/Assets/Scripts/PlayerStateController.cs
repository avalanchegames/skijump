using UnityEngine;
using System.Collections;

// Script by Norbert Leskovics.
// (AR)This script holds an enumerator to represent what the player is currently doing.

	//playerStates below contains all the possible states the player can be in public class PlayerStateController : MonoBehaviour 
{
	public enum PlayerStates
	{
		starting,
		slide_down,
		pre_jump,
		jumping,
		jumping_wide,
		landing,
		post_landing,
		finished
	}
	private PlayerStates currentState;	
	
	// Setter for the state.
	public void ChangeState(PlayerStates newState)
	{
		currentState = newState;
	}

	// Getter for the state.
	public PlayerStates GetState()
	{
		return currentState;
	}
}
