using UnityEngine;
using System.Collections;

public class PlayerStateController : MonoBehaviour {

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

	public void ChangeState(PlayerStates newState)
	{
		currentState = newState;
	}

	public PlayerStates GetState()
	{
		return currentState;
	}

}
