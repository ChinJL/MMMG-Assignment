using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannon : Laser {

	[SerializeField] Transform m_laserPoint = null;
	[SerializeField] GameObject roomLayer = null;
	SwitchRoom switchRoom;
	LightSwitch lightSwitch;

	public bool isPortals = false;
	public bool isSwitched = false;
	public GameObject correctReflector_1;
	public GameObject correctReflector_2;
	public GameObject portal_1;
	public GameObject portal_2;

	private void OnEnable(){
		laserPoint = m_laserPoint;
		laserSource = gameObject.transform;
		AdjustDirection ();
		switchRoom = roomLayer.GetComponent<SwitchRoom> ();
		lightSwitch = roomLayer.GetComponent<LightSwitch> ();
	}

	private void Update(){
		if (!switchRoom.isSwitching && lightSwitch.isLightOn)
		{
			ShootLaser ();
			PoweredSound ();

//			if (!isSwitched)
//			{
//				lineRenderer.enabled = true;
//				lineRenderer.SetPosition (0, laserPoint.position);
//				lineRenderer.SetPosition (1, correctReflector_1.transform.position);
//			}
//			else
//			{
//				lineRenderer.enabled = true;
//				lineRenderer.SetPosition (0, laserPoint.position);
//				lineRenderer.SetPosition (1, correctReflector_2.transform.position);
//			}
		}
		else
		{
			lineRenderer.SetPosition(0, Vector3.zero);
			lineRenderer.SetPosition(1, Vector3.zero);
			lineRenderer.enabled = false;
			isShoot = true;
		}
	}

	private bool isShoot = true;
	private void PoweredSound(){
		if (!switchRoom.isSwitching && isShoot)
		{
			AudioManager.audioManager.PlaySfx (AudioManager.Sfx.laser);
			isShoot = false;
		}
	}

	protected override void LaserEffect1(){
		LaserReflect laserReflect = laserReflector.GetComponent<LaserReflect> ();
		laserReflect.powerLvl += Time.deltaTime * 2;
	}

	protected override void LaserEffect3(){
		LaserPortal laserPortal = portal.GetComponent<LaserPortal> ();
		laserPortal.powerLvl += Time.deltaTime * 2;
		laserPortal.laserDirection = direction;
	}
}
