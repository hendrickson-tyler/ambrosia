using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
	public Enemy tasteless = new Enemy();

	GameObject player;
	GameObject ship;
	NavMeshAgent nav;

	Transform destination;


	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		ship = GameObject.Find ("Ship");
		nav = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		determineDestination ();
		tasteless.attackDelay -= Time.deltaTime;

		if (tasteless.health <= 0) {
			Destroy (gameObject);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player" && tasteless.attackDelay <= 0) {
			other.gameObject.GetComponent<PlayerController> ().grubby.takeDamage (tasteless.attackStrength);
			tasteless.attackDelay = 1.5f;
			tasteless.attacking = true;
		} else if (other.name == "Ship" && tasteless.attackDelay <= 0) {
			other.gameObject.GetComponent<ShipController> ().ship.takeDamage (tasteless.attackStrength);
			tasteless.attackDelay = 1.5f;
			tasteless.attacking = true;
		}
		//Debug.Log ("WITHIN RANGE!");
	}

	void OnTriggerExit(Collider other) {
		tasteless.attacking = false;
	}

	void determineDestination() {
		if (player != null && ship != null) {
			float distanceToPlayer = Vector3.Distance (transform.position, player.transform.position);
			float distanceToShip = Vector3.Distance (transform.position, ship.transform.position);

			if (distanceToPlayer <= distanceToShip)
				nav.SetDestination (player.transform.position);
			else
				nav.SetDestination (ship.transform.position);

			if (tasteless.attacking == true) {
				nav.SetDestination (gameObject.transform.position);
			}
		} else if (ship == null && player != null)
			nav.SetDestination (player.transform.position);
		else if (ship != null && player == null)
			nav.SetDestination (ship.transform.position);
	}
}
