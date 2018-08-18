using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSensor : MonoBehaviour {
	public bool laser1, laser2;

	public bool isSensor1, isSensor2;

	private MeshRenderer meshRend;
	private Color lightBlue;

	private void Awake(){
		meshRend = GetComponent<MeshRenderer> ();
		meshRend.material.color = Color.black;
		lightBlue = new Color (0, 0.5f, 1);
	}

	private void Update(){
		if (isSensor1 && laser1) {
			meshRend.material.color = lightBlue;
			isSensor1 = false;
		}
		else if (isSensor2 && laser2) {
			meshRend.material.color = lightBlue;
			isSensor2 = false;
		}
		else {
			laser1 = false;
			laser2 = false;
			isSensor1 = false;
			isSensor2 = false;
			meshRend.material.color = Color.black;
		}
	}
}
