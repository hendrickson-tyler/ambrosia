using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public GameObject bomb_prefab;
	float impulse = 20f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Camera cam = Camera.main;

		// Hold the bomb
		if (Input.GetKeyDown("e")) {

		}

		// Release the bomb
		if (Input.GetKeyUp("e")) {
			GameObject bomb = (GameObject) Instantiate (bomb_prefab, cam.transform.position, cam.transform.rotation);
			bomb.GetComponent<Rigidbody> ().AddForce (cam.transform.forward * impulse, ForceMode.Impulse);
		}
	}
}
