using UnityEngine;
using System.Collections;

// Script by Norbert Leskovics.
// (AR)This script flags to the player object that the screen should be shaking while they are inside the trigger collider.

public class ShakingScript : MonoBehaviour 
{
	// Runs when a collider touches this trigger.
	void OnTriggerEnter( Collider other )
	{
		if (other.gameObject.GetComponent <PlayerMovement>() != null)
		{
			other.gameObject.GetComponent <PlayerMovement>().inShakyBit = true;
			other.gameObject.GetComponent <PlayerMovement>().shaking = true;
		}
	}
	
	// Runs when a collider leaves this trigger entirely.
	void OnTriggerExit( Collider other )
	{
		if (other.gameObject.GetComponent <PlayerMovement>() != null)
		{
			other.gameObject.GetComponent <PlayerMovement>().inShakyBit = false;
		}
	}
}
