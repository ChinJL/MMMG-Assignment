using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
	[SerializeField] SpriteRenderer m_spriteRend = null;
	Rigidbody m_rb;
	private float hAxis;
	[SerializeField] private float speed = 5;
	private float m_speed;
	private Vector3 hMovement;
	private bool isLeft, isRight;

	private void OnEnable(){
		m_rb = GetComponent<Rigidbody>();
	}

	private void Update(){
		DecideDirection ();
		CalculateMovement ();
	}

	private void FixedUpdate(){
		MoveRb ();
	}

	public void MoveLeft(){
		isLeft = true;
		FlipLeft ();
	}

	public void ResetMovement(){
		isLeft = false;
		isRight = false;
	}

	public void MoveRight(){
		isRight = true;
		FlipRight ();
	}

	private void DecideDirection(){
		if (isLeft)
		{
			hAxis = -1;
		}
		else if (isRight)
		{
			hAxis = 1;
		}
		else
		{
			hAxis = 0;
		}
	}

	private void CalculateMovement(){
		m_speed = speed * Time.deltaTime;
		hMovement = new Vector3 (hAxis, 0, 0) * m_speed;
	}

	private void MoveRb(){
		m_rb.MovePosition (transform.position + hMovement);
	}

	private void FlipLeft(){
		m_spriteRend.flipX = true;
	}
	private void FlipRight(){
		m_spriteRend.flipX = false;
	}
}
