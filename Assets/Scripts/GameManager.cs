using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Camera deathCam;
	public Text counter;
	public Text collectedRequired;
	public Text health;
	public GameObject winLose;
	public GameObject winLoseDescription;
	public GameObject enemyPrefab;
	bool gameWon = false;

	public int partsReturned = 0;
	int partsRequired = 3;
	int seconds = 60;
	float enemySpawnDelay = 8.0f;

	GameObject[] spawnLocations;

	// Use this for initialization
	void Awake () {
		InitGame ();
	}

	// Update is called once per frame
	void Update () {
		countTime ();
		collectedRequired.text = partsReturned + " / " + partsRequired;

		//update health
		health.text = GameObject.Find("Player").GetComponent<PlayerController>().grubby.health.ToString();

		if (GameObject.Find ("Player").GetComponent<PlayerController> ().grubby.health <= 0) {
			//dies
			seconds = 0;
			winLose.GetComponent<Text> ().text = "OUCH!";
			winLose.SetActive (true);
			winLoseDescription.GetComponent<Text> ().text = "You got chomped!";
			winLoseDescription.SetActive (true);
			deathCam.gameObject.SetActive (true);
			for (int i = 0; i < 3; i++)
				Destroy (spawnLocations [i]);
		}

	}

	void InitGame() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		spawnLocations = GameObject.FindGameObjectsWithTag ("EnemySpawn");
	}		

	void countTime() {
		counter.text = Mathf.Clamp(seconds - (int)Time.timeSinceLevelLoad, 0, seconds).ToString();
		enemySpawnDelay -= Time.deltaTime;

		if (counter.text == "0") {
			counter.text = "TIME UP";

			if (partsReturned >= partsRequired) {
				winLose.SetActive (true);
				for (int i = 0; i < 3; i++)
					Destroy (spawnLocations [i]);
			} else {
				winLose.GetComponent<Text> ().text = "BUMMER...";
				winLose.SetActive (true);
 				winLoseDescription.SetActive (true);
				for (int i = 0; i < 3; i++)
					Destroy (spawnLocations [i]);
			}
		}

		if (enemySpawnDelay <= 0) {
			spawnEnemies ();
		}
	}

	void spawnEnemies() {
		for (int i = 0; i < 3; i++) {
			int rand = Random.Range (0, 3);
			Instantiate (enemyPrefab, spawnLocations [rand].transform.position, spawnLocations [rand].transform.rotation);
		}

		enemySpawnDelay = 8.0f; //for maximum fun, turn this off
	}
}
	
