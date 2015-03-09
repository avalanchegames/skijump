using UnityEngine;
using System.Collections;

public class ShakingScript : MonoBehaviour {
	void OnTriggerEnter( Collider other )
	{
		other.gameObject.GetComponent <PlayerMovement>().inShakyBit = true;
		other.gameObject.GetComponent <PlayerMovement>().shaking = true;
	}
	
	void OnTriggerExit( Collider other )
	{
		other.gameObject.GetComponent <PlayerMovement>().inShakyBit = false;
	}
}
