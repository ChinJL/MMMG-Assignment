using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace laser {
	[System.Serializable]
	public class Tags{
		public string s_reflector = "Reflector";
		public string s_laserSensor_1 = "LaserSensor1";
		public string s_laserSensor_2 = "LaserSensor2";
		public string s_portal_1I = "Portal_1I";
		public string s_portal_1O = "Portal_1O";
		public string s_portal_2I = "Portal_2I";
		public string s_portal_2O = "Portal_2O";
	}

	public class Laser : MonoBehaviour {
		public bool laser1, laser2;

		[SerializeField] private Transform laserGun = null, gunNozzle = null;
		private Vector3 heading;
		private float distance;
		private Vector3 direction;
		[SerializeField] private GameObject roomLayer = null; 
		SwitchRoom switchRoom;

		public Reflect reflect;
		public LaserGunToReflector LG_to_R;
		public LaserReflectorToSensor LR_to_S;

		void Start()
		{
			reflect = GetComponent<Reflect> ();
		}

		private void LaserDirection(){
			heading = gunNozzle.position - laserGun.position;
			distance = heading.magnitude;
			direction = heading / distance;
		}

		private void OnEnable(){
			switchRoom = roomLayer.GetComponent<SwitchRoom> ();
			LaserDirection ();
		}

		private void Update(){
			if (!switchRoom.isSwitching)
				ShootLaser ();

			LaserEffect(currentHitObject);
		}

		[SerializeField] private float laserLength = 10;
		private GameObject currentHitObject;
		public Tags tags;

		private void ShootLaser(){
			Ray laserRay = new Ray (gunNozzle.position, direction);
			RaycastHit hit;
			Debug.DrawRay (gunNozzle.position, direction * laserLength, Color.green);
			if (Physics.Raycast (laserRay, out hit, Mathf.Infinity)) {
				currentHitObject = hit.transform.gameObject;
				Debug.DrawLine (gunNozzle.position, hit.point, Color.red);
			} else {
				currentHitObject = null;
			}
		}

		//reference: https://www.youtube.com/watch?v=Nplcqwq_oJU
		private void LaserEffect(GameObject currentHitObj){
			if (currentHitObj != null) {
				if (currentHitObj.CompareTag (tags.s_reflector)) {
					Reflect reflect = currentHitObj.GetComponent<Reflect> ();
					reflect.isReflect = true;

					if (!switchRoom.isSwitching) {						
						reflect.isReflect_ = true;
					}

					//ignore one of them for different laser source
					if (laser1) {
						reflect.laser1 = true;
						reflect.laser2 = false;
					} else if (laser2) {
						reflect.laser2 = true;
						reflect.laser1 = false;
					}
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
					portal.laserDirection = direction;

					LG_to_R.hit_Portal_1 = true;
					LR_to_S.hittedPortal_1 = true;

					if (laser1)
						portal.laser1 = true;
					else if(laser2)
						portal.laser2 = true;
				}
				else if (currentHitObj.CompareTag (tags.s_portal_1O)) {
					Portal portal = currentHitObj.GetComponent<Portal> ();
					portal.isPortal_1O = true;
					portal.laserDirection = direction;
					if (laser1)
						portal.laser1 = true;
					else if(laser2)
						portal.laser2 = true;
				}
				else if (currentHitObj.CompareTag (tags.s_portal_2I)) {
					Portal portal = currentHitObj.GetComponent<Portal> ();
					portal.isPortal_2I = true;
					portal.laserDirection = direction;

					LG_to_R.hit_Portal_2 = true;
					LR_to_S.hittedPortal_2 = true;

					if (laser1)
						portal.laser1 = true;
					else if(laser2)
						portal.laser2 = true;
				}
				else if (currentHitObj.CompareTag (tags.s_portal_2O)) {
					Portal portal = currentHitObj.GetComponent<Portal> ();
					portal.isPortal_2O = true;
					portal.laserDirection = direction;
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
}
