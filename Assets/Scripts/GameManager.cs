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
	GameObject ship;
	public GameObject enemyPrefab;
	public Camera deathCam;

	// Use this for initialization
	void Awake () {
		InitGame ();
	}

	// Update is called once per frame
	void Update () {
		updateDisplay ();
		updateTime ();
		checkForDeath ();
	}

	void InitGame() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		spawnLocations = GameObject.FindGameObjectsWithTag ("EnemySpawn");
		player = GameObject.FindGameObjectWithTag ("Player");
		ship = GameObject.Find ("Ship");
	}	

	void updateDisplay() {
		timer.text = Mathf.Clamp(game.timeRemaining - (int)Time.timeSinceLevelLoad, 0, game.timeRemaining).ToString();
		partsCollected.text = game.partsReturned + " / " + game.partsRequired;
		if (player != null)
			health.text = player.GetComponent<PlayerController> ().grubby.health.ToString ();
	}

	void updateTime() {
		game.enemySpawnDelay -= Time.deltaTime;

		if (game.enemySpawnDelay <= 0)
			spawnEnemies ();

		if (timer.text == "0") {
			game.won = determineWinLose ();
			destroySpawnLocations ();
		}
	}

	bool determineWinLose() {
		timer.text = "TIME UP";

		// win
		if (game.partsReturned >= game.partsRequired) {
			winLose.SetActive (true);
			GameObject[] allEnemies = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach(GameObject enemy in allEnemies) {
				Destroy (enemy);
			}
			return true;
		} 

		else {
			winLose.GetComponent<Text> ().text = "BUMMER...";
			winLose.SetActive (true);
			winLoseDescription.SetActive (true);
			return false;
		}
	}

	void spawnEnemies() {
		if (spawnLocations[0] != null) {
			for (int i = 0; i < game.enemySpawnAmount; i++) {
				int rand = Random.Range (0, spawnLocations.Length);
				Instantiate (enemyPrefab, spawnLocations [rand].transform.position, spawnLocations [rand].transform.rotation);
			}
			game.resetSpawnDelay (); //for maximum fun, turn this off
		}
	}

	void checkForDeath() {
		//update health
		if (player != null && player.GetComponent<PlayerController> ().grubby.health <= 0) {
			game.timeRemaining = 0;
			winLose.GetComponent<Text> ().text = "OUCH!";
			winLose.SetActive (true);
			winLoseDescription.GetComponent<Text> ().text = "You got chomped!";
			winLoseDescription.SetActive (true);
			deathCam.gameObject.SetActive (true);
			destroySpawnLocations ();
		} else if (ship != null && ship.GetComponent<ShipController> ().ship.health <= 0) {
			game.timeRemaining = 0;
			winLose.GetComponent<Text> ().text = "BOOM!";
			winLose.SetActive (true);
			winLoseDescription.GetComponent<Text> ().text = "Your only means of escape was destroyed!";
			winLoseDescription.SetActive (true);
			destroySpawnLocations ();
		}
	}

	void destroySpawnLocations () {
		foreach (GameObject spawnLocation in spawnLocations)
			Destroy (spawnLocation);
	}
}
	
