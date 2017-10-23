using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacecraft : MonoBehaviour {
	int health = 100;
	public GameObject icon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (other.gameObject.GetComponent<PlayerController> ().grubby.partHeld == true) {
				GameObject.Find ("GameManager").GetComponent<GameManager> ().partsReturned += 1;
				other.gameObject.GetComponent<PlayerController> ().grubby.partHeld = false;
				icon.SetActive (false);
			}
		}
	}
		
	public void takeDamage(int damage) {
		health -= damage;
		Debug.Log("Spacecraft health at:" + health);
	}
}
