using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeaponController : MonoBehaviour {
	float lifespan = 20;
	public GameObject explosiveEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lifespan -= Time.deltaTime;
		Debug.Log (lifespan);

		if (lifespan <= 0) {
			Explode ();
		}
	}

	void Explode() {
		Instantiate(explosiveEffect, gameObject.transform.position, gameObject.transform.rotation);
		Destroy (gameObject);
	}
}
