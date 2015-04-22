using UnityEngine;
using System.Collections;

// Script by Norbert Leskovics.
// (AR)This script halts the player when the player enters the attached trigger collider.

public class PhysicsMaterialChange : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	// Runs when a collider touches this trigger.
	void OnTriggerEnter( Collider other )
	{
		other.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		
		PlayerMovement playerMovementManager = other.gameObject.GetComponent <PlayerMovement>();
		if (playerMovementManager == null)
		{
			playerMovementManager.finished = true;
		}
/*		// Find the Hill that the player is on.
		GameObject theLandingHill = GameObject.Find ("LandingRescaled");

		// The other collider will be the player.
		GameObject thePlayer = other.gameObject;

		// Get the colliders of the objects
		Collider HillCollider = theLandingHill.GetComponent ("MeshCollider");
		Collider PlayerCollider = thePlayer.GetComponent ("Box Collider");

		// Change the physics materials to a grippier material.
		HillCollider.material = PhysicsMaterial ();
		PlayerCollider.material = PhysicsMaterial ();
*/
	}
}
