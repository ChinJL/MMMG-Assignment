using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Unlock : MonoBehaviour {

	public GameObject Nextleveldoor;
	// Use this for initialization
//	void OnEnable () {
//		if (Nextleveldoor == null)
//		{
//			
//		}
//	}
	
	void OnTriggerEnter(Collider other){
		if (other.tag == "True")
		{
			Nextleveldoor = GameObject.Find ("SecretDoor");
			print ("hi");
			Nextleveldoor.SetActive(true);
		}
	}

}
