using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour {
	public bool laser1, laser2;
	[SerializeField] private float reflect_offSet = 0;
	public bool isReflect;
	public bool isReflect_ = false;
	[SerializeField] private Transform reflector = null, reflectionPoint = null;
	private Vector3 laserDir;
	private float laserLength = 10;
	public laser.Tags tags;

	private GameObject currentHitObject;

	private void Update(){
		
		if (isReflect) {
			laserDir = LaserDirection ();
			Ray laserRay = new Ray (reflectionPoint.position,laserDir);
			RaycastHit hit;
			Debug.DrawRay (reflectionPoint.position, laserDir * laserLength, Color.green);
			if (Physics.Raycast (laserRay, out hit, Mathf.Infinity)) {
				currentHitObject = hit.transform.gameObject;
				Debug.DrawLine (reflectionPoint.position, hit.point, Color.red);

			} else {
				currentHitObject = null;
			}
			isReflect = false;
		}
		else {
			currentHitObject = null;
		}

		LaserEffect (currentHitObject);
	}

	private Vector3 LaserDirection(){
		Vector3 heading = reflectionPoint.position - new Vector3 (reflector.position.x + reflect_offSet, reflector.position.y, reflector.position.z);
		float distance = heading.magnitude;
		Vector3 direction = heading / distance;
		return direction;
	}

	private void LaserEffect(GameObject currentHitObj){
		if (currentHitObj != null) {
			if (currentHitObj.CompareTag (tags.s_reflector)) {
				Reflect reflect = currentHitObj.GetComponent<Reflect> ();
				reflect.isReflect = true;
				if (laser1)
					reflect.laser1 = true;
				else if(laser2)
					reflect.laser2 = true;
			}
			else if (currentHitObj.CompareTag (tags.s_laserSensor_1)) {
				LaserSensor laserSensor = currentHitObj.GetComponent<LaserSensor> ();
				laserSensor.isSensor1 = true;
				if (laser1)
					laserSensor.laser1 = true;
				else if(laser2)
					laserSensor.laser2 = true;
			}
			else if (currentHitObj.CompareTag (tags.s_laserSensor_2)) {
				LaserSensor laserSensor = currentHitObj.GetComponent<LaserSensor> ();
				laserSensor.isSensor2 = true;
				if (laser1)
					laserSensor.laser1 = true;
				else if(laser2)
					laserSensor.laser2 = true;
			}
			else if (currentHitObj.CompareTag (tags.s_portal_1I)) {
				Portal portal = currentHitObj.GetComponent<Portal> ();
				portal.isPortal_1I = true;
				portal.laserDirection = laserDir;
				if (laser1)
					portal.laser1 = true;
				else if(laser2)
					portal.laser2 = true;
			}
			else if (currentHitObj.CompareTag (tags.s_portal_1O)) {
				Portal portal = currentHitObj.GetComponent<Portal> ();
				portal.isPortal_1O = true;
				portal.laserDirection = laserDir;
				if (laser1)
					portal.laser1 = true;
				else if(laser2)
					portal.laser2 = true;
			}
			else if (currentHitObj.CompareTag (tags.s_portal_2I)) {
				Portal portal = currentHitObj.GetComponent<Portal> ();
				portal.isPortal_2I = true;
				portal.laserDirection = laserDir;
				if (laser1)
					portal.laser1 = true;
				else if(laser2)
					portal.laser2 = true;
			}
			else if (currentHitObj.CompareTag (tags.s_portal_2O)) {
				Portal portal = currentHitObj.GetComponent<Portal> ();
				portal.isPortal_2O = true;
				portal.laserDirection = laserDir;
				if (laser1)
					portal.laser1 = true;
				else if(laser2)
					portal.laser2 = true;
			}
			else {
			}
		} else {
		}
	}
}
