using UnityEngine;
using System.Collections;

public class JumpEnabler : MonoBehaviour {

	void OnTriggerEnter( Collider other )
	{
		other.gameObject.GetComponent <PlayerMovement>().jumpEnabled = true;
	}

	void OnTriggerExit( Collider other )
	{
		other.gameObject.GetComponent <PlayerMovement>().jumpEnabled = false;
		other.gameObject.GetComponent <PlayerMovement> ().jumpDone = true;
	}
}
