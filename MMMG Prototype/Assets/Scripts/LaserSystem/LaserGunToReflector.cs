using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunToReflector : MonoBehaviour {

	private LineRenderer lineRenderer;

	public Transform reflector_1;
	public Transform reflector_2;
	public Transform portal_1;
	public Transform portal_2;

	public SwitchRoom switchRoom;

	public bool hit_Portal_1 = false;
	public bool hit_Portal_2 = false;

	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;
		lineRenderer.useWorldSpace = true;
	}

	void Update()
	{
		//GunToReflector ();

		if (!switchRoom.isSwitching)
		{
			lineRenderer.enabled = true;
		} 
		else
		{
			lineRenderer.enabled = false;
		}
	}

//	void GunToReflector()
//	{
//		if (!hit_Portal_1 || !hit_Portal_2)
//		{
//			if (!switchRoom.Reflector_1_or_Reflector_2)
//			{
//				lineRenderer.SetPosition (0, transform.position);
//				lineRenderer.SetPosition (1, reflector_1.position);
//			}
//			else
//			{
//				lineRenderer.SetPosition (0, transform.position);
//				lineRenderer.SetPosition (1, reflector_2.position);
//			}
//		}
//		if (hit_Portal_1)
//		{
//			lineRenderer.SetPosition (0, transform.position);
//			lineRenderer.SetPosition (1, portal_1.position);
//		}
//		if (hit_Portal_2)
//		{
//			lineRenderer.SetPosition (0, transform.position);
//			lineRenderer.SetPosition (1, portal_2.position);
//		}
//	}
}
