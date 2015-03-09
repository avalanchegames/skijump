using UnityEngine;
using System.Collections;

public class PhysicsMaterialChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider other )
	{
		other.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		other.gameObject.GetComponent <PlayerMovement>().finished = true;
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
