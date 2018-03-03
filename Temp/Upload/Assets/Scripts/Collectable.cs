using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	float speed = 2.0f;

	void Update() {
		transform.Rotate (0, speed, 0);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController> ().grubby.partHeld == false) {
			Destroy (gameObject);
			other.gameObject.GetComponent<PlayerController> ().grubby.partHeld = true;
			GameObject.Find ("GameManager").GetComponent<GameManager> ().UI.updatePartIcon (other.gameObject.GetComponent<PlayerController> ().grubby.partHeld);
		}

		else if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerController> ().grubby.partHeld == true) {
			GameObject.Find ("GameManager").GetComponent<GameManager> ().UI.partAlreadyHeldMessage ();
		}
	}

	void OnTriggerExit (Collider other) {
		GameObject.Find ("GameManager").GetComponent<GameManager> ().UI.clearSubMessage ();
	}
}
