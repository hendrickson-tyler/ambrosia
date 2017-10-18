using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Text counter;
	public Text collectedRequired;
	public GameObject winLose;
	public GameObject winLoseDescription;

	public int partsReturned = 0;
	int partsRequired = 3;
	int seconds = 160;

	// Use this for initialization
	void Awake () {
		InitGame ();
	}

	// Update is called once per frame
	void Update () {
		countTime ();
		collectedRequired.text = partsReturned + " / " + partsRequired;
	}

	void InitGame() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}		

	void countTime() {
		counter.text = Mathf.Clamp(seconds - (int)Time.timeSinceLevelLoad, 0, seconds).ToString();

		if (counter.text == "0") {
			counter.text = "TIME UP";

			if (partsReturned >= partsRequired) {
				winLose.SetActive (true);
			} else {
				winLose.GetComponent<Text> ().text = "BUMMER...";
				winLose.SetActive (true);
 				winLoseDescription.SetActive (true);
			}

		}
	}
}
	
