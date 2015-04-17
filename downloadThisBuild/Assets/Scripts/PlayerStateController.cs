using UnityEngine;
using System.Collections;

// Script by Norbert Leskovics.
// (AR)This script holds an enumerator to represent what the player is currently doing.

public class PlayerStateController : MonoBehaviour 
{
	public enum playerStates
	{
		starting,
		slide_down,
		pre_jump,
		jumping,
		landing,
		post_landing,
		finished
	}
	private playerStates currentState;	
	
	// Setter for the state.
	public void ChangeState(playerStates newState)
	{
		currentState = newState;
	}

	// Getter for the state.
	public playerStates GetState()
	{
		return currentState;
	}
}
