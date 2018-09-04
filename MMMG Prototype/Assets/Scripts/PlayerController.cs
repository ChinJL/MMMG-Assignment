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

	Quaternion y_left = Quaternion.Euler (0, 180, 0);
	Quaternion y_right = Quaternion.Euler (0, 0, 0);
	Quaternion y_left1 = Quaternion.Euler(0,181,0);
	Quaternion y_right1 = Quaternion.Euler(0,1,0);
	private void FixedUpdate()
	{
		if (isPlaying) {
			m_rb.MovePosition (transform.position + hMovement);
			ResetPlayerRotation (transform);
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
			clamp = false;
			m_clampCooldown = clampCooldown;
			if (Input.GetAxis (s_horizontal) > 0 && Input.GetAxis (s_horizontal) < 1)
			{
				player.rotation = Quaternion.RotateTowards (player.rotation, y_right1, rotationSpeed);
			}
			if (Input.GetAxis (s_horizontal) > -1 && Input.GetAxis (s_horizontal) < 0)
			{
				player.rotation = Quaternion.RotateTowards (player.rotation, y_left1, rotationSpeed);
			}
			if (Input.GetAxis (s_horizontal) == 1)
			{
				player.rotation = Quaternion.RotateTowards (player.rotation, y_right, rotationSpeed);
			}
			if (Input.GetAxis (s_horizontal) == -1)
			{
				player.rotation = Quaternion.RotateTowards (player.rotation, y_left, rotationSpeed);
			}
		}
		else {
			if (m_clampCooldown <= 0) {
				clamp = true;
			} else {
				m_clampCooldown -= Time.deltaTime;
			}
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
