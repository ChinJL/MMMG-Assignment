using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MoveScript : MonoBehaviour {

	float directionX;
	public float moveSpeed = 5f;
	Rigidbody rb;

	public Animator anim;

	public AxisTouchButton left_right;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		directionX = CrossPlatformInputManager.GetAxis ("Horizontal");

		GameObject[] button = GameObject.FindGameObjectsWithTag ("Button");
		foreach (GameObject Button in button)
		{
			AxisTouchButton buttons = Button.GetComponent<AxisTouchButton> ();

			if (buttons.axisValue == 1)
			{
				anim.SetBool ("isRunningRight", true);
			}
			if (buttons.axisValue == -1)
			{
				anim.SetBool ("isRunningLeft", true);
			}
		}


	}

	void FixedUpdate()
	{
		rb.velocity = new Vector2 (directionX * moveSpeed, rb.velocity.y);
	}

}
