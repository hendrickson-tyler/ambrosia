using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerController> ().die ();
			GameObject.Find ("GameManager").GetComponent<GameManager> ().UI.fallingDeathMessage ();
			GameObject.Find ("GameManager").GetComponent<GameManager> ().game.won = false;
		}
		Destroy (other.gameObject);
	}
}
