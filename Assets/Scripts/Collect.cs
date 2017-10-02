using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour {

	public GameObject winText;

	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("You win");
		Destroy (GameObject.Find("Ship Part"));
		winText.SetActive (true);
	}
}
