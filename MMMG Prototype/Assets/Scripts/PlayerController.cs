using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
	private Rigidbody m_rb;
	[SerializeField] private GameObject mainCamera = null;
	[SerializeField] private float cam_min_x = -1, cam_max_x = 11;
	[SerializeField] private float speed = 10;
	[SerializeField] private float rotationSpeed = 16;
	private float m_speed;
	private float hAxis;
	private Vector3 hMovement;
	[HideInInspector] public bool isPlaying;
	//[SerializeField] private Transform tempObj;

	float directionX;
	public float moveSpeed = 5f;

	private string s_horizontal = "Horizontal";

	public Animator anim;

	private void Awake()
	{
		m_rb = GetComponent<Rigidbody> ();
	}

	private void OnEnable()
	{
		isPlaying = true;
	}

	private void OnDisable()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (isPlaying) {
			InitializeHorizontalMovement ();
			HorizontalMovement (transform);
			//FixPlayerVelocity ();
		}

		m_rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

		directionX = CrossPlatformInputManager.GetAxis (s_horizontal);
	}

	//For Gravity Related
		//GetAxisRaw dont have float in between 0 to 1, GetAxis has float in between from 0 before reaching 1
		//use GetAxis on ground, use GetAxisRaw in air
		//unfreeze if no ground / not touching anything
		//m_rb.constraints = RigidbodyConstraints.None;

	//horizontal movement is move along z axis
	//lateral movement is move along x axis
	//vertical movement is move along y axis

	//along x-axis
	//i lazy test :P

	//along y-axis
	Quaternion y_left = Quaternion.Euler (0, 180, 0);
	Quaternion y_right = Quaternion.Euler (0, 0, 0);
//	Quaternion y_back = Quaternion.Euler (0, 90, 0);
//	Quaternion y_front = Quaternion.Euler (0, 270, 0);
//
//	//anti-clockwise
	Quaternion y_left1 = Quaternion.Euler(0,181,0);
	Quaternion y_right1 = Quaternion.Euler(0,1,0);
//	//clockwise
//	Quaternion y_left2 = Quaternion.Euler(0,180,0);
//	Quaternion y_right2 = Quaternion.Euler(0,0,0);
//
//	//along  z-axis
//	Quaternion z_left = Quaternion.Euler (0, 0, 180);
//	Quaternion z_right = Quaternion.Euler (0, 0, 0);
//	Quaternion z_up = Quaternion.Euler (0, 0, 90);
//	Quaternion z_down = Quaternion.Euler (0, 0, 270);

	//set specific direction, then use RotateTowards that dir
		//Quaternion q = Quaternion.LookRotation(target.position - transform.position);

	//Slerp: rotate with abit motion afetrwards
		//player.rotation = Quaternion.Slerp (player.rotation, y_right, Time.deltaTime);
	//SlerpUnclamped: slerp but faster?
		//player.rotation = Quaternion.SlerpUnclamped (player.rotation, y_right, Time.deltaTime);
	//RotateTowards: so slow...
		//player.rotation = Quaternion.RotateTowards (player.rotation, y_right, Time.deltaTime);

	//FromToRotation: unadjustable speed which look at the target position instantly
		//player.rotation = Quaternion.FromToRotation((player.position - Vector3.right), player.position);

	private void FixedUpdate()
	{
		if (isPlaying) {
			m_rb.MovePosition (transform.position + hMovement);
			ResetPlayerRotation (transform);
			//CameraMovement (cam_min_x, cam_max_x);
		}

		m_rb.velocity = new Vector2 (directionX * moveSpeed, m_rb.velocity.y);
	}

	private void CameraMovement(float min_cam_x, float max_cam_x)
	{
		if(mainCamera.transform.position.x >= min_cam_x && mainCamera.transform.position.x <= max_cam_x){
			mainCamera.transform.position += hMovement;
		}
		if (mainCamera.transform.position.x < min_cam_x) {
			mainCamera.transform.position = new Vector3 (min_cam_x, mainCamera.transform.position.y, mainCamera.transform.position.z);
		}
		if (mainCamera.transform.position.x > max_cam_x) {
			mainCamera.transform.position = new Vector3 (max_cam_x, mainCamera.transform.position.y, mainCamera.transform.position.z);
		}
	}

	private void InitializeHorizontalMovement()
	{
		hAxis = Input.GetAxis (s_horizontal);
		m_speed = speed * Time.deltaTime;
		hMovement = new Vector3 (hAxis, 0, 0) * m_speed;
	}

	private bool clamp;
	[SerializeField] private float clampCooldown = 1;
	private float m_clampCooldown;
	public void HorizontalMovement(Transform player)
	{
		if (Input.GetButton (s_horizontal))
		{
			//freeze x,y,z pos b4 use to avoid collision
//			if (Input.GetAxis (s_horizontal) > 0 && Input.GetAxis (s_horizontal) < 1) {
//				player.rotation = Quaternion.Slerp (player.rotation, y_right1, rotationSpeed * Time.deltaTime);
//			}
//			if (Input.GetAxis (s_horizontal) > -1 && Input.GetAxis (s_horizontal) < 0) {
//				player.rotation = Quaternion.Slerp (player.rotation, y_left1, rotationSpeed * Time.deltaTime);
//			}
//			if (Input.GetAxis (s_horizontal) == 1) {
//				player.rotation = Quaternion.Slerp (player.rotation, y_right, rotationSpeed * Time.deltaTime);
//			}
//			if (Input.GetAxis (s_horizontal) == -1) {
//				player.rotation = Quaternion.Slerp (player.rotation, y_left, rotationSpeed * Time.deltaTime);
//			}

			clamp = false;
			m_clampCooldown = clampCooldown;
			if (Input.GetAxis (s_horizontal) > 0 && Input.GetAxis (s_horizontal) < 1)
			{
				player.rotation = Quaternion.RotateTowards (player.rotation, y_right1, rotationSpeed);
				//anim.SetBool ("isRunningRight", true);
			}
			if (Input.GetAxis (s_horizontal) > -1 && Input.GetAxis (s_horizontal) < 0)
			{
				player.rotation = Quaternion.RotateTowards (player.rotation, y_left1, rotationSpeed);
				//anim.SetBool ("isRunningLeft", true);
			}
			if (Input.GetAxis (s_horizontal) == 1)
			{
				player.rotation = Quaternion.RotateTowards (player.rotation, y_right, rotationSpeed);
				//anim.SetBool ("isRunningRight", true);
			}
			if (Input.GetAxis (s_horizontal) == -1)
			{
				player.rotation = Quaternion.RotateTowards (player.rotation, y_left, rotationSpeed);
				//anim.SetBool ("isRunningLeft", true);
			}
		}
		else {
			if (m_clampCooldown <= 0) {
				clamp = true;
			} else {
				m_clampCooldown -= Time.deltaTime;
			}
//			anim.SetBool ("isRunningRight", false);
//			anim.SetBool ("isRunningLeft", false);
		}

		if (CrossPlatformInputManager.GetButton (s_horizontal))
		{
			
		}
	}

	private void ResetPlayerRotation(Transform player)
	{
		if (clamp) {
			//cant use this because euler angle starts calculating at 0 again and ends at 360
			//			if (player.eulerAngles.y < 90 && player.eulerAngles.y >= 270) {
			//				player.rotation = Quaternion.RotateTowards (player.rotation, y_right, rotationSpeed);
			//			} 
			if (player.eulerAngles.y < 90 && player.eulerAngles.y >= 0) {
				player.rotation = Quaternion.RotateTowards (player.rotation, y_right, rotationSpeed);
			}
			else if (player.eulerAngles.y < 360 && player.eulerAngles.y >= 270) {
				player.rotation = Quaternion.RotateTowards (player.rotation, y_right, rotationSpeed);
			}
			else if (player.eulerAngles.y < 270 && player.eulerAngles.y >= 90) {
				player.rotation = Quaternion.RotateTowards (player.rotation, y_left, rotationSpeed);
			}
		}
	}

	private void FixPlayerVelocity()
	{
		if (Input.GetButtonUp (s_horizontal))
			m_rb.velocity = Vector3.zero;
	}
}
