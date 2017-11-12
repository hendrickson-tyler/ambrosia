using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
	public Ship ship = new Ship();
	public GameObject icon;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (ship.health <= 0) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (other.gameObject.GetComponent<PlayerController> ().grubby.partHeld == true) {
				GameObject.Find ("GameManager").GetComponent<GameManager> ().game.returnPart ();
				other.gameObject.GetComponent<PlayerController> ().grubby.partHeld = false;
				icon.SetActive (false);
			}
		}
	}
}
