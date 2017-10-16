using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeaponController : MonoBehaviour {
	SubWeapon omNomBomb = new SubWeapon();
	public GameObject explosiveEffect;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		omNomBomb.tick (Time.deltaTime);

		if (omNomBomb.delay <= 0) {
			Explode ();
		}
	}

	void Explode() {
		omNomBomb.detonated = true;

		// explosion effect
		Destroy(Instantiate(explosiveEffect, gameObject.transform.position, gameObject.transform.rotation), 4.0f);

		// hurt enemies

		// get rid of the object
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy" && omNomBomb.detonated == true) {
			Destroy (other.gameObject);
		}
	}
}
