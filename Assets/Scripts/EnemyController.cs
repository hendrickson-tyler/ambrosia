using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
	public Enemy tasteless = new Enemy();

	GameObject player;
	GameObject ship;
	GameObject attackTarget;
	NavMeshAgent nav;

	Transform destination;


	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		ship = GameObject.Find ("Spacecraft");
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
			attackTarget = other.gameObject;
			other.gameObject.GetComponent<PlayerController> ().grubby.takeDamage (tasteless.attackStrength);
			tasteless.attackDelay = 1.5f;

			Debug.Log ("Player health: " + other.gameObject.GetComponent<PlayerController> ().grubby.health);
		}
		Debug.Log ("WITHIN RANGE!");
	}

	void determineDestination() {
		float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
		float distanceToShip = Vector3.Distance(transform.position, ship.transform.position);

		if (distanceToPlayer <= distanceToShip)
			nav.SetDestination (player.transform.position);
		else
			nav.SetDestination (ship.transform.position);
	}
}
