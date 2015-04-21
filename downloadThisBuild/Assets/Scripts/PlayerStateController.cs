using UnityEngine;
using System.Collections;

public class PlayerStateController : MonoBehaviour {

	//playerStates below contains all the possible states the player can be in 
	public enum playerStates
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
	private playerStates currentState;	//holds the value of the current state, obviously we want this to be private

	public void ChangeState(playerStates newState)	//setter for changing the state 
	{
		currentState = newState;
	}

	public playerStates GetState()					//getter for the state
	{
		return currentState;
	}

}
