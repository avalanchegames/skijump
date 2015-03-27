using UnityEngine;
using System.Collections;

public class SlowMoTriggerScript : MonoBehaviour {

	void OnTriggerEnter( Collider other )
	{
		other.gameObject.GetComponent <PlayerMovement>().slowMo = true;
	}
	
	void OnTriggerExit( Collider other )
	{
		other.gameObject.GetComponent <PlayerMovement>().slowMo = false;
	}
}
