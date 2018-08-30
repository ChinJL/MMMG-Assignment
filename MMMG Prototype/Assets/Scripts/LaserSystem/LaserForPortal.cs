using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserForPortal : MonoBehaviour {

	LineRenderer lineRenderer;

	public Transform reflector_1;
	public Transform reflector_2;

	public SwitchRoom switchRoom;

	public bool hit_Reflector = false;
	// False = Laser 1 // True = Laser 2
	public bool Laser_1_or_Laser_2 = false;
	// False = 1 // True = 2
	public bool reflectior_1_or_2 = false;
	public bool ON_OFF = true;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;
		lineRenderer.useWorldSpace = true;
	}

	void Update()
	{
		LineInfrontPortal ();

		if (!switchRoom.isSwitching)
		{
			lineRenderer.enabled = true;
		} 
		else
		{
			lineRenderer.enabled = false;
		}
	}

	void LineInfrontPortal()
	{
		if (!ON_OFF)
		{
			lineRenderer.enabled = true;

			if (!hit_Reflector)
			{
				if (!Laser_1_or_Laser_2)
				{
					lineRenderer.SetPosition (0, transform.position);
					lineRenderer.SetPosition (1, transform.position + new Vector3 (10, 0, 0));
				}
				else
				{
					lineRenderer.SetPosition (0, transform.position);
					lineRenderer.SetPosition (1, transform.position + new Vector3 (-10, 0, 0));
				}
			}
			else
			{
				if (!reflectior_1_or_2)
				{
					lineRenderer.SetPosition (0, transform.position);
					lineRenderer.SetPosition (0, reflector_1.position);
				}
				else
				{
					lineRenderer.SetPosition (0, transform.position);
					lineRenderer.SetPosition (0, reflector_2.position);
				}
			}
		}
		else
		{
			lineRenderer.enabled = false;
		}
		
	}
}
