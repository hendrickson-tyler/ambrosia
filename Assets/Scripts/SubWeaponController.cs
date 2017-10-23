using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeaponController : MonoBehaviour {
	SubWeapon omNomBomb = new SubWeapon();
	public GameObject explosiveEffect;
	int bombDamage = 60;

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
		// explosion effect
		Destroy(Instantiate(explosiveEffect, gameObject.transform.position, gameObject.transform.rotation), 1.0f);

		Vector3 explosionPosition = transform.position;
		float explosionRadius = 2.0f;
		Collider[] colliders = Physics.OverlapSphere (explosionPosition, explosionRadius);
		int counter = 0;

		foreach (Collider col in colliders) {
			if (col.tag == "Enemy") {
				
				col.GetComponent<EnemyController> ().tasteless.takeDamage (bombDamage);
				counter++;
				Debug.Log (counter);
			}
		}

		Destroy (gameObject);
	}
}
