using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUI : MonoBehaviour {
	Camera cameraToLookAt;
	GameObject bar;
	Vector3 barVector;

	// Use this for initialization
	void Start () {
		cameraToLookAt = GameObject.Find("PlayerCamera").GetComponent<Camera>();
		bar = GameObject.Find ("HealthBar");
		barVector = bar.transform.localScale;
	}

	// Update is called once per frame
	void Update () {
		if (cameraToLookAt != null) {
			barVector.x = ((float)GameObject.Find ("Ship").GetComponent<ShipController> ().ship.health / 1000);
			bar.transform.localScale = barVector;

			Vector3 v = cameraToLookAt.transform.position - transform.position;
			v.x = v.z = 0.0f;
			transform.LookAt (cameraToLookAt.transform.position - v); 
			transform.Rotate (0, 180, 0);
		} else
			Destroy (gameObject);
	}
}
