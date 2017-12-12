using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerController> ().die ();
			GameObject.Find ("GameManager").GetComponent<GameManager> ().game.won = false;

			if (gameObject.name == "DeathPlane")
				GameObject.Find ("GameManager").GetComponent<GameManager> ().UI.fallingDeathMessage ();
			else
				GameObject.Find ("GameManager").GetComponent<GameManager> ().UI.drowningDeathMessage ();
			

		}
		Destroy (other.gameObject);
	}
}
