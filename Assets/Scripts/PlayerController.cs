using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	public Player grubby = new Player (ammoType.smoothie, bombType.chili);

	CharacterController character;

	public GameObject bombPrefab;
	public GameObject bulletPrefab;
	public GameObject nozzle;

	float walkSpeed = 5.0f;
    float sprintSpeed = 10.0f;

	float forwardSpeed;
	float sideSpeed;
    
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
		regenerateHealth ();
		checkForDeath ();
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
		if (Input.GetKeyDown("e") && grubby.omNomBomb.thrown == false) {
			
		}

		// Release the bomb
		if (Input.GetKeyUp("e") && grubby.omNomBomb.thrown == false) {
			GameObject bomb = (GameObject) Instantiate (bombPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
			bomb.GetComponent<Rigidbody> ().AddForce (Camera.main.transform.forward * (grubby.omNomBomb.range + forwardSpeed), ForceMode.Impulse);
			grubby.omNomBomb.thrown = true;
		}

		if (grubby.omNomBomb.thrown == true) {
			grubby.omNomBomb.throwDelay -= Time.deltaTime;

			if (grubby.omNomBomb.throwDelay <= 0) {
				grubby.omNomBomb.resetBomb ();
			}
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
		if (Input.GetButton ("Fire1") && grubby.blenderBlaster.currentCharge > 0) {
			GameObject bullet = (GameObject)Instantiate (bulletPrefab, nozzle.transform.position, nozzle.transform.rotation);
			bullet.GetComponent<Rigidbody> ().AddForce (nozzle.transform.forward * (grubby.blenderBlaster.range + forwardSpeed), ForceMode.Impulse);
			grubby.blenderBlaster.discharge (Time.deltaTime);
			//Debug.Log ("CHARGE: " + grubby.blenderBlaster.currentCharge);
		} else if (!Input.GetButton ("Fire1")) {
			grubby.blenderBlaster.recharge (Time.deltaTime);
			//Debug.Log ("Recharge: " + grubby.blenderBlaster.currentCharge);
		}
	}

	void regenerateHealth() {
		grubby.regenDelay -= Time.deltaTime;

		if (grubby.regenDelay <= 0) {
			grubby.regenHealth (Time.deltaTime);
		}
	}

	void checkForDeath() {
		if (grubby.health <= 0) {
			grubby.health = 0;
			Destroy (gameObject);
		}
	}
}

