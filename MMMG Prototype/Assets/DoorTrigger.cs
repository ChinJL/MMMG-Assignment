using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {
	private string plyer = "Player";
	private Collider m_col;

	private void Awake(){
		m_col = GetComponent<Collider> ();
	}

	private void OnTriggerEnter(Collider col){
		if (col.CompareTag (plyer)) {
			//next level
			print("You WIN");
			m_col.enabled = false;
		}
	}
}
