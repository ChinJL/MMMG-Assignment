using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerLevel1 : MonoBehaviour {

	public GameObject doorTrigger;
	public GameObject doorTrigger_2;
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == ("Player"))
		{
			Destroy (gameObject);
			gameObject.SetActive (true);
			doorTrigger_2.SetActive (true);
		}
	}
}
