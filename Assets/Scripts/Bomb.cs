using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
	public GameObject explosiveEffect;
	GameObject player;
	float explosionRadius = 3.0f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (player != null) {
			player.GetComponent<PlayerController> ().grubby.omNomBomb.tick (Time.deltaTime);

			if (player.GetComponent<PlayerController> ().grubby.omNomBomb.delay <= 0) {
				Explode ();
			}
		} else
			Explode ();
	}

	void Explode() {
		Destroy(Instantiate(explosiveEffect, gameObject.transform.position, gameObject.transform.rotation), 4.0f);

		Vector3 explosionPosition = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPosition, explosionRadius);
		int counter = 0;

		foreach (Collider col in colliders) {
			if (col.tag == "Enemy" && player != null) {
				col.GetComponent<EnemyController> ().tasteless.takeDamage (player.GetComponent<PlayerController>().grubby.omNomBomb.damage);
				counter++;
			}
		}

		Destroy (gameObject);
	}
}
