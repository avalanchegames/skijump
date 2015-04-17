using UnityEngine;
using System.Collections;

// Script by Norbert Leskovics.
// (AR)Script attached to a trigger collider. The player will only be allowed to jump when they are in contact with such a trigger collider.
// Edit by Andrew Robson: check existance of Componant in collider to avoid Null Pointer Exceptions.

public class JumpEnabler : MonoBehaviour 
{
	// Runs when a collider touches this trigger.
	void OnTriggerEnter( Collider other )
	{
		if (other.gameObject.GetComponent <PlayerMovement>() != null)
		{
			other.gameObject.GetComponent <PlayerMovement>().jumpEnabled = true;
		}
	}

	// Runs when a collider leaves this trigger entirely.
	void OnTriggerExit( Collider other )
	{
		if (other.gameObject.GetComponent <PlayerMovement>() != null)
		{
			other.gameObject.GetComponent <PlayerMovement>().jumpEnabled = false;
			other.gameObject.GetComponent <PlayerMovement>().jumpDone = true;
		}
	}
}
