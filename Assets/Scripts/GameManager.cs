using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Game game = new Game(difficultyLevel.easy);

	// UI
	public Text timer;
	public Text partsCollected;
	public Text health;
	public GameObject winLose;
	public GameObject winLoseDescription;

	GameObject[] spawnLocations;
	GameObject player;
	public GameObject enemyPrefab;
	public Camera deathCam;

	// Use this for initialization
	void Awake () {
		InitGame ();
	}

	// Update is called once per frame
	void Update () {
		updateTime ();
		updateHealth ();
		partsCollected.text = game.partsReturned + " / " + game.partsRequired;
	}

	void InitGame() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		spawnLocations = GameObject.FindGameObjectsWithTag ("EnemySpawn");
		player = GameObject.FindGameObjectWithTag ("Player");
	}		

	void updateTime() {
		timer.text = Mathf.Clamp(game.timeRemaining - (int)Time.timeSinceLevelLoad, 0, game.timeRemaining).ToString();
		game.enemySpawnDelay -= Time.deltaTime;

		if (timer.text == "0") {
			timer.text = "TIME UP";

			if (game.partsReturned >= game.partsRequired) {
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

		if (game.enemySpawnDelay <= 0) {
			spawnEnemies ();
		}
	}

	void spawnEnemies() {
		if (spawnLocations[0] != null) {
			for (int i = 0; i < 3; i++) {
				int rand = Random.Range (0, 3);
				Instantiate (enemyPrefab, spawnLocations [rand].transform.position, spawnLocations [rand].transform.rotation);
			}
			game.enemySpawnDelay = 8.0f; //for maximum fun, turn this off
		}
	}

	void updateHealth() {
		//update health
		if (player != null) {
			health.text = player.GetComponent<PlayerController> ().grubby.health.ToString ();

			if (player.GetComponent<PlayerController> ().grubby.health <= 0) {
				//dies
				game.timeRemaining = 0;
				winLose.GetComponent<Text> ().text = "OUCH!";
				winLose.SetActive (true);
				winLoseDescription.GetComponent<Text> ().text = "You got chomped!";
				winLoseDescription.SetActive (true);
				deathCam.gameObject.SetActive (true);
				for (int i = 0; i < 3; i++)
					Destroy (spawnLocations [i]);
			}
		}
	}
}
	
