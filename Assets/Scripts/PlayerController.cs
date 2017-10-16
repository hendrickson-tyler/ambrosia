using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    float speed = 15.0f;
	float walkSpeed = 15.0f;
    float sprintSpeed = 30.0f;
	float jumpSpeed = 5.0f;
    
    float horizontalSensitivity = 5.0f;
    float verticalSensitivity = 5.0f;
    float lookLimit = 80.0f;
    float pitch = 0.0f;

	float verticalVelocity = 0.0f;

	CharacterController character;
    
	// Use this for initialization
	void Start () {
		character = GetComponent<CharacterController>();
        //Screen.lockCursor = true; //move to game manager
	}

	// Update is called once per frame
	void Update () {
		rotate ();
		move ();

		if (Input.GetButtonDown("Jump") && character.isGrounded) {
			jump();
		}

		if (character.isGrounded) {
			if (Input.GetKey ("left shift")) {
				speed = sprintSpeed;
			}
			else
				speed = walkSpeed;
		}
	}

	// Movement of player
	void move() {
		float deltaX = Input.GetAxis("Horizontal") * speed;
		float deltaZ = Input.GetAxis("Vertical") * speed;

		if(!character.isGrounded)
			verticalVelocity += Physics.gravity.y * Time.deltaTime;

		Vector3 direction = new Vector3(deltaX, verticalVelocity, deltaZ);

		direction = transform.TransformDirection(direction);
		character.Move(direction * Time.deltaTime);
	}

	// Yaw of player and pitch of camera
	void rotate() {
		float yaw = Input.GetAxis("Mouse X") * horizontalSensitivity;
		transform.Rotate(0, yaw, 0);

		pitch -= Input.GetAxis("Mouse Y") * verticalSensitivity;
		pitch = Mathf.Clamp(pitch, -lookLimit, lookLimit);
		Camera.main.transform.localEulerAngles = new Vector3(pitch, 0, 0);
	}

	void jump() {
		verticalVelocity = jumpSpeed;
	}
}

