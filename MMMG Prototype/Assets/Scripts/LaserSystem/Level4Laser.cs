using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Laser : MonoBehaviour {

	public GameObject gunNozzle;
	public GameObject Ball;
	public GameObject Spawner;
	public GameObject Sensor;

	// Update is called once per frame
	void Update () {
		Ray Raygun = new Ray (gunNozzle.transform.position, new Vector3(-1, 0, 0));
		RaycastHit hit;
		Debug.DrawRay (gunNozzle.transform.position, new Vector3 (-1, 0, 0), Color.green);
		if (Physics.Raycast(Raygun, out hit, Mathf.Infinity)) {
			if (hit.transform.gameObject == Sensor) {
				Instantiate (Ball, Spawner.transform.position, Spawner.transform.rotation);
			}
		}

	}
}
