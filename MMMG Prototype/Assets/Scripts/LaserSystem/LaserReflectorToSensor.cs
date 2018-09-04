using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflectorToSensor : MonoBehaviour {

	private LineRenderer lineRenderer;

	public Reflect reflect;

	public Transform reflector;
	public Transform sensor;

	public bool hittedPortal_1 = false;
	public bool hittedPortal_2 = false;

	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;
		lineRenderer.useWorldSpace = true;
	}

	void Update()
	{
		//reflectorToSensor = true;
		//ReflectorToSensor ();

		if (reflect.isReflect_) {
			lineRenderer.enabled = true;
		}
		else 
		{
			lineRenderer.enabled = false;
		}
	}

//	void ReflectorToSensor()
//	{
//		if (!hittedPortal_1 || !hittedPortal_2)
//		{
//			lineRenderer.SetPosition (0, reflector.position);
//			lineRenderer.SetPosition (1, sensor.position);
//		}
//		if (hittedPortal_1)
//		{
//			if (!reflect.isReflect_)
//			{
//				lineRenderer.SetPosition (0, reflector.position);
//				lineRenderer.SetPosition (1, reflector.position);
//			}
//			else
//			{
//				lineRenderer.SetPosition (0, reflector.position);
//				lineRenderer.SetPosition (1, sensor.position);
//			}
//		}
//		if (hittedPortal_2)
//		{
//			if (!reflect.isReflect_)
//			{
//				lineRenderer.SetPosition (0, reflector.position);
//				lineRenderer.SetPosition (1, reflector.position);
//			}
//			else
//			{
//				lineRenderer.SetPosition (0, reflector.position);
//				lineRenderer.SetPosition (1, sensor.position);
//			}
//		}
//	}
}
