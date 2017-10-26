using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public GameObject splashEffect;
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag != "Bullet") {
			if (other.gameObject.tag == "Enemy" && player != null) {
				other.gameObject.GetComponent<EnemyController> ().tasteless.takeDamage (player.GetComponent<PlayerController> ().grubby.blenderBlaster.damage);
				//Debug.Log ("Health is:" + other.gameObject.GetComponent<EnemyController> ().tasteless.health);
			}

			Destroy (Instantiate (splashEffect, gameObject.transform.position, gameObject.transform.rotation), 2.0f);
			Destroy (gameObject);
		}
	}
}
