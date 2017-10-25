using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
	public GameObject explosiveEffect;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().grubby.omNomBomb.tick (Time.deltaTime);

			if (GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().grubby.omNomBomb.delay <= 0) {
				Explode ();
			}
		} else
			Explode ();
	}

	void Explode() {
		// explosion effect
		Destroy(Instantiate(explosiveEffect, gameObject.transform.position, gameObject.transform.rotation), 4.0f);

		Vector3 explosionPosition = transform.position;
		float explosionRadius = 2.0f;
		Collider[] colliders = Physics.OverlapSphere (explosionPosition, explosionRadius);
		int counter = 0;

		foreach (Collider col in colliders) {
			if (col.tag == "Enemy" && GameObject.FindGameObjectWithTag("Player") != null) {
				col.GetComponent<EnemyController> ().tasteless.takeDamage (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().grubby.omNomBomb.damage);
				counter++;
			}
		}

		Destroy (gameObject);
	}
}
