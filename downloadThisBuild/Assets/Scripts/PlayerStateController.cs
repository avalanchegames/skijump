using UnityEngine;
using System.Collections;

public class PlayerStateController : MonoBehaviour {

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

	public void ChangeState(playerStates newState)
	{
		currentState = newState;
	}

	public playerStates GetState()
	{
		return currentState;
	}

}
