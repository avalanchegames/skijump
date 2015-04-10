using UnityEngine;
using System.Collections;

public class vsBlinker : MonoBehaviour
{
	private float fTimer;
	private float fWaitTime = 0.5f;
	private float fResetPoint;
	
	// Use this for initialization
	void Awake ()
	{
		fResetPoint = fWaitTime * 2;
	}
	
	// Update is called once per frame
	void Update ()
	{	
		fTimer += Time.deltaTime;
		
		if(fTimer < fWaitTime)
			gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;
					
		if(fTimer > fWaitTime)
			gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
		
		if(fTimer > fResetPoint)
			fTimer = 0.0f;		
	}
}