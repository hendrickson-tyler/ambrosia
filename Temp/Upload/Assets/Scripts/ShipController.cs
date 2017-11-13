using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
	public Ship ship = new Ship();
	public GameObject explosiveEffect;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (ship.health <= 0) {
			GameObject.Find ("GameManager").GetComponent<GameManager> ().UI.clearSubMessage ();
			GameObject.Find ("GameManager").GetComponent<GameManager> ().UI.shipDestroyedMessage ();
			GameObject.Find ("GameManager").GetComponent<GameManager> ().game.won = false;
			Destroy(Instantiate(explosiveEffect, gameObject.transform.position, gameObject.transform.rotation), 4.0f);
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (other.gameObject.GetComponent<PlayerController> ().grubby.partHeld == true) {
				GameObject.Find ("GameManager").GetComponent<GameManager> ().game.returnPart ();
				GameObject.Find ("GameManager").GetComponent<GameManager> ().UI.updatePartsReturned (GameObject.Find ("GameManager").GetComponent<GameManager> ().game.partsReturned, GameObject.Find ("GameManager").GetComponent<GameManager> ().game.partsRequired);
				other.gameObject.GetComponent<PlayerController> ().grubby.partHeld = false;
				GameObject.Find ("GameManager").GetComponent<GameManager> ().UI.updatePartIcon (other.gameObject.GetComponent<PlayerController> ().grubby.partHeld);
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Enemy") {
			GameObject.Find ("GameManager").GetComponent<GameManager> ().UI.shipUnderAttackMessage ();
		}
	}
}
