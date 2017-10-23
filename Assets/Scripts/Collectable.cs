using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	public GameObject status;
	public GameObject icon;
	float speed = 2.0f;

	void Start() {
		
	}

	void Update() {
		transform.Rotate (0, 0, speed);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController> ().grubby.partHeld == false) {
			Destroy (gameObject);
			other.gameObject.GetComponent<PlayerController> ().grubby.partHeld = true;
			icon.SetActive (true);
		}

		else if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController> ().grubby.partHeld == true) {
			status.SetActive (true);

		}
	}

	void OnTriggerExit (Collider other) {
		status.SetActive (false);
	}
}
