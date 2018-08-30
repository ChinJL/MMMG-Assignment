using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class PortalGun : MonoBehaviour {
	[SerializeField] private Transform player = null;
	private float projectile_distance = 15f;
	[SerializeField] private Transform source = null;
	private Vector3 heading;
	private float distance;
	private Vector3 direction;
	private bool isShoot;
	private Transform surface;
	[SerializeField] private GameObject[] portalObj = null;
	private int turn = 0;
	[SerializeField] private float bulletSpeed = 10f;
	[SerializeField] private float offset_y = 5;
	private bool isReached;
	[SerializeField] private GameObject roomLayer = null;
	SwitchRoom switchRoom;
	[SerializeField] private KeyCode aimKey = KeyCode.None, shootKey = KeyCode.None, resetKey = KeyCode.None;
	[SerializeField] private LineRenderer lineRend = null;

	private void OnEnable(){
		isShoot = false;
		isReached = true;
		switchRoom = roomLayer.GetComponent<SwitchRoom> ();
	}

	private void Update(){
		MeasureDirection (source, player);
		ToggleShoot ();
		if (isShoot) {
			Aim ();
			lineRend.enabled = true;
		}else{
			lineRend.enabled = false;
		}

		if (switchRoom.isSwitching) {
			foreach (GameObject portal in portalObj) {
				portal.SetActive (false);
			}
		} 
//		else {
//			foreach (GameObject portal in portalObj) {
//				portal.SetActive (true);
//			}
//		}
	}

	private void ToggleShoot(){
		if (Input.GetKeyDown (aimKey))
			isShoot = !isShoot;
	}

	private void MeasureDirection(Transform source, Transform player){
		heading = source.position - player.position;
		distance = heading.magnitude;
		direction = heading / distance;
	}

	private void Aim(){
		Ray ray = new Ray (source.position, direction);
		RaycastHit hit;
		Debug.DrawRay (source.position, direction * projectile_distance, Color.green);
//		if (Physics.Raycast (source.position, transform.forward, out hit)) {
//			if (hit.collider) {
//				lineRend.SetPosition (0, source.position);
//				lineRend.SetPosition (1, direction * hit.distance);
//				Shoot (hit.point);
//			}
//		}
		if (Physics.Raycast (ray, out hit, projectile_distance)) { //Mathf.Infinity
			Debug.DrawLine (source.position, hit.point, Color.red);
			//reminder: set line rend setting to use world space position, wasted hours for using local position - -
			lineRend.SetPosition (0, source.position);
			lineRend.SetPosition (1, hit.point);
			Shoot (hit.point);
		} else {
			//sound effect?
		}
	}

	private void Shoot(Vector3 location){
		//if (Input.GetKeyDown (shootKey) && isReached) {
		if (Input.GetKey(KeyCode.Mouse0) && isReached)
		{	
			isReached = false;
			Vector3 aimedPos = location;
			aimedPos = aimedPos + (Vector3.down * offset_y);
			if (turn == 2)
			{
				ResetPortal ();
			}
			else
			{
				portalObj [turn].SetActive (true);
				StartCoroutine (PlacePortal (portalObj [turn], aimedPos));
				turn++;
			}
		}

		if (Input.GetKeyDown (resetKey) && isReached) {
			ResetPortal ();
		}
	}

	private void ResetPortal(){
		foreach (GameObject portal in portalObj) {
			portal.SetActive (false);
		}
		isReached = true;
		turn = 0;

		GameObject[] laserGun = GameObject.FindGameObjectsWithTag ("LaserGun");
		foreach (GameObject gun in laserGun)
		{
			LaserGunToReflector portal = gun.GetComponent<LaserGunToReflector> ();
			portal.hit_Portal_1 = false;
			portal.hit_Portal_2 = false;
		}
		GameObject[] reflector = GameObject.FindGameObjectsWithTag ("Reflector");
		foreach (GameObject reflect in reflector) {
			LaserReflectorToSensor portalHit = reflect.GetComponent<LaserReflectorToSensor> ();
			portalHit.hittedPortal_1 = false;
			portalHit.hittedPortal_2 = false;
		}
	}

	private IEnumerator PlacePortal(GameObject bullet, Vector3 targetLocation){
		bullet.transform.position = source.position;
		while(bullet.transform.position!= targetLocation){
			bullet.transform.position = Vector3.MoveTowards (bullet.transform.position, targetLocation, bulletSpeed * Time.deltaTime);
			yield return null;
		}
		yield return isReached = true;
		yield return null;
	}
}
