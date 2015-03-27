using UnityEngine;
using System.Collections;

public class slowmoLanding : MonoBehaviour {

	void OnTriggerEnter( Collider other )
	{
		other.gameObject.GetComponent <PlayerMovement>().slowMo = true;
	}
}
