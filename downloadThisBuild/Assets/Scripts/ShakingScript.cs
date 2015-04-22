using UnityEngine;
using System.Collections;

// Script by Norbert Leskovics.
// (AR)This script flags to the player object that the screen should be shaking while they are inside the trigger collider.

public class ShakingScript : MonoBehaviour 
{
	// Runs when a collider touches this trigger.
	void OnTriggerEnter( Collider other )
	{
		PlayerMovement playerMovementManager = other.gameObject.GetComponent <PlayerMovement>();
		if (playerMovementManager != null)
		{
			playerMovementManager.inShakyBit = true;
			playerMovementManager.shaking = true;
		}
	}
	
	// Runs when a collider leaves this trigger entirely.
	void OnTriggerExit( Collider other )
	{
		PlayerMovement playerMovementManager = other.gameObject.GetComponent <PlayerMovement>();
		if (playerMovementManager != null)
		{
			playerMovementManager.inShakyBit = false;
		}
	}
}
