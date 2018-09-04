using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
	public bool laser1, laser2;

	[SerializeField] private Transform portal_1In = null, portal_1Out = null, portal_2In = null, portal_2Out = null;
	public bool isPortal_1I, isPortal_1O, isPortal_2I, isPortal_2O;

	private GameObject currentHitObject;
	[SerializeField] private float laserLength = 10;
	public Vector3 laserDirection;

	public laser.Tags tags;

	Portal portal1In, portal1Out, portal2In, portal2Out;

	public SwitchRoom switchRoom;

	public LineRenderer lineRenderer;
	public bool Hitted = true;

	void Start()
	{
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;
		lineRenderer.useWorldSpace = true;
	}

	private void Awake(){
		portal1In = portal_1In.GetComponent<Portal> ();
		portal1Out = portal_1Out.GetComponent<Portal> ();
		portal2In = portal_2In.GetComponent<Portal> ();
		portal2Out = portal_2Out.GetComponent<Portal> ();
	}
	//**
	private void Update(){
		if (isPortal_1I) {
			if (portal_1Out.gameObject.activeSelf) {
				ShootLaser (portal_1Out, laserDirection);
			}
			portal1In.isPortal_1I = false;
		}
		else if (isPortal_1O) {
			if (portal_1In.gameObject.activeSelf) {
				ShootLaser (portal_1In, laserDirection);
			}
			portal1Out.isPortal_1O = false;
		}
		else if(isPortal_2I){
			if (portal2Out.gameObject.activeSelf) {
				ShootLaser (portal_2Out, laserDirection);
			}
			portal2In.isPortal_2I = false;
		}
		else if(isPortal_2O){
			if (portal2In.gameObject.activeSelf) {
				ShootLaser (portal_2In, laserDirection);
			}
			portal2Out.isPortal_2O = false;
		}
		else {
			currentHitObject = null;
		}
		LaserEffect (currentHitObject);
	}

	private void ShootLaser(Transform portalTransform, Vector3 direction){
		Ray laserRay = new Ray (portalTransform.position, direction);
		RaycastHit hit;
		Debug.DrawRay (portalTransform.position, direction * laserLength, Color.cyan);
		if (Physics.Raycast (laserRay, out hit, Mathf.Infinity)) {
			currentHitObject = hit.transform.gameObject;
			lineRenderer.enabled = true;
			lineRenderer.SetPosition (0, portalTransform.position);
			lineRenderer.SetPosition (1, hit.point);
		} else {
			currentHitObject = null;
		}
			
//		lineRenderer.SetPosition (0, portalTransform.position);
//		lineRenderer.SetPosition (1, currentHitObject.transform.position);
	}

	private void LaserEffect(GameObject currentHitObj){
		if (currentHitObj != null) {
			if (currentHitObj.CompareTag (tags.s_reflector)) {
				Reflect reflect = currentHitObj.GetComponent<Reflect> ();
				reflect.isReflect = true;

				if (!switchRoom.isSwitching) {						
					reflect.isReflect_ = true;
				}

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
				portal.laserDirection = laserDirection;
				if (laser1)
					portal.laser1 = true;
				else if(laser2)
					portal.laser2 = true;
			}
			else if (currentHitObj.CompareTag (tags.s_portal_1O)) {
				Portal portal = currentHitObj.GetComponent<Portal> ();
				portal.isPortal_1O = true;
				portal.laserDirection = laserDirection;
				if (laser1)
					portal.laser1 = true;
				else if(laser2)
					portal.laser2 = true;
			}
			else if (currentHitObj.CompareTag (tags.s_portal_2I)) {
				Portal portal = currentHitObj.GetComponent<Portal> ();
				portal.isPortal_2I = true;
				portal.laserDirection = laserDirection;
				if (laser1)
					portal.laser1 = true;
				else if(laser2)
					portal.laser2 = true;
			}
			else if (currentHitObj.CompareTag (tags.s_portal_2O)) {
				Portal portal = currentHitObj.GetComponent<Portal> ();
				portal.isPortal_2O = true;
				portal.laserDirection = laserDirection;
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
