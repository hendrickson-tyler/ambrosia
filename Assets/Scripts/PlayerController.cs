using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	public Player grubby = new Player ();
	CharacterController character;
	public GameObject bombPrefab;

	MainWeapon blenderBlaster = new MainWeapon();
	public GameObject bulletPrefab;
	public GameObject nozzle;
	Camera deathCam = new Camera();

	float walkSpeed = 5.0f;
    float sprintSpeed = 10.0f;
	float impulse = 15f;

	float forwardSpeed;
	float sideSpeed;

	bool bombThrown = false;
    
    float horizontalSensitivity = 5.0f;
    float verticalSensitivity = 5.0f;
    float lookLimit = 80.0f;
    float pitch = 0.0f;

	float verticalVelocity = 0.0f;
	    
	// Use this for initialization
	void Start () {
		character = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {
		rotate ();
		move ();
		sprint ();
		jump ();
		shoot ();
		throwBomb ();

		// check health
		if (grubby.health <= 0) {
			grubby.health = 0;
			Destroy (gameObject);
		}

		grubby.regenDelay -= Time.deltaTime;

		if (grubby.regenDelay <= 0) {
			grubby.regenHealth (Time.deltaTime);
		}
	}

	void move() {
		sideSpeed = Input.GetAxis("Horizontal") * grubby.speed;
		forwardSpeed = Input.GetAxis("Vertical") * grubby.speed;

		if(!character.isGrounded)
			verticalVelocity += Physics.gravity.y * Time.deltaTime;

		Vector3 direction = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);

		direction = transform.TransformDirection(direction);
		character.Move(direction * Time.deltaTime);
	}

	void rotate() {
		float yaw = Input.GetAxis("Mouse X") * horizontalSensitivity;
		transform.Rotate(0, yaw, 0);

		pitch -= Input.GetAxis("Mouse Y") * verticalSensitivity;
		pitch = Mathf.Clamp(pitch, -lookLimit, lookLimit);
		Camera.main.transform.localEulerAngles = new Vector3(pitch, 0, 0);
	}

	void jump() {
		if (Input.GetButtonDown("Jump") && character.isGrounded) {
			verticalVelocity = grubby.jumpHeight;
		}
	}

	void throwBomb() {
		// Hold the bomb
		if (Input.GetKeyDown("e")) {
			
		}

		// Release the bomb
		if (Input.GetKeyUp("e")) {
			GameObject bomb = (GameObject) Instantiate (bombPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
			bomb.GetComponent<Rigidbody> ().AddForce (Camera.main.transform.forward * (impulse + forwardSpeed), ForceMode.Impulse);
		}
	}

	void sprint() {
		if (character.isGrounded) {
			if (Input.GetKey ("left shift") && !Input.GetButton ("Fire1")) {
				grubby.speed = sprintSpeed;
			}
			else
				grubby.speed = walkSpeed;
		}
	}

	void shoot() {
		if (Input.GetButton ("Fire1")) {
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, nozzle.transform.position, nozzle.transform.rotation);
			bullet.GetComponent<Rigidbody> ().AddForce (nozzle.transform.forward * (impulse + forwardSpeed), ForceMode.Impulse);
		}
	}
}

