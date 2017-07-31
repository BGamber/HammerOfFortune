using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public float playerMoveSpeed = 4.0f;
	public float mouseSensitivity = 2.0f;

	float verticalRotation = 0;
	float upDownRange = 90.0f;

	float playerSprintSpeed;
	float verticalVelocity = 0;
	//float jumpSpeed = 4f;
	//int jumpCount = 0;

	CharacterController controller;
	public Camera playerCam;

	// Use this for initialization
	void Start () {

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		controller = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {

		Move();

	}

	void Move() {

		// Rotation
		float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
		transform.Rotate(0, rotLeftRight, 0);

		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		playerCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

		// Movement
		if(Input.GetButton("Sprint")) {
			playerSprintSpeed = 1.5f;
		} else {
			playerSprintSpeed = 1f;
		}

		float forwardSpeed = Input.GetAxis("Vertical") * playerMoveSpeed * playerSprintSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * playerMoveSpeed * playerSprintSpeed;

		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);

		speed = transform.rotation * speed;

		controller.Move(speed * Time.deltaTime);

	}

}
