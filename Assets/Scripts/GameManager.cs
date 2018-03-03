using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public Game game = new Game(difficultyLevel.easy);
	public UIController UI = new UIController ();

	GameObject[] spawnLocations;
	GameObject[] shipPartSpawnLocations;
	GameObject player;
	GameObject ship;
	public GameObject enemyPrefab;
	public GameObject shipPartPrefab;

	// Use this for initialization
	void Awake () {
		InitGame ();
	}

	void InitGame() {
		UI.initializeUI ();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		spawnLocations = GameObject.FindGameObjectsWithTag ("EnemySpawn");
		shipPartSpawnLocations = GameObject.FindGameObjectsWithTag ("ShipPartSpawn");
		player = GameObject.FindGameObjectWithTag ("Player");
		ship = GameObject.Find ("Ship");
	}	

	// Update is called once per frame
	void Update () {
		updateTime ();
		checkForDeath ();
	}

	void updateTime() {
		UI.updateTime (game.timeRemaining);
		UI.countDown (Time.deltaTime);

		// between waves
		if (game.betweenWaves) {
			game.betweenWaveDelay -= Time.deltaTime;
			UI.waveCountdownMessage (game.wave, game.betweenWaveDelay);

			if (game.betweenWaveDelay <= 0 && !game.won) {
				UI.beginWave (game.wave);
				game.betweenWaves = false;
				game.resetTimeRemaining ();
				game.partsReturned = 0;
				UI.updatePartsReturned (game.partsReturned, game.partsRequired);
			}
		}

		// active waves
		else {
			if (game.timeRemaining > 0) {
				game.timeRemaining -= Time.deltaTime;
				game.enemySpawnDelay -= Time.deltaTime;
				game.partSpawnDelay -= Time.deltaTime;
			}

			else if (game.timeRemaining <= 0) {
				timeUp ();
			}

			if (game.enemySpawnDelay <= 0)
				spawnEnemies ();

			if (game.partSpawnDelay <= 0)
				spawnShipParts ();
		}
	}

	void timeUp() {
		if (game.partsReturned >= game.partsRequired && !game.betweenWaves) {
			if (game.wave == 3) {
				game.won = true;
				UI.gameWonMessage ();
			}

			game.betweenWaves = true;
			UI.waveCompleteMessage (game.wave);
			game.advanceWave ();
			game.betweenWaveDelay = 8.0f;

			GameObject[] allEnemies = GameObject.FindGameObjectsWithTag ("Enemy");
			foreach(GameObject enemy in allEnemies) {
				Destroy (enemy);
			}

			GameObject[] allParts = GameObject.FindGameObjectsWithTag ("ShipPart");
			foreach(GameObject part in allParts) {
				Destroy (part);
			}
		} 

		else if (game.partsReturned <= game.partsRequired && !game.betweenWaves) {
			UI.partsNotCollectedMessage ();
			game.won = false;
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

	void spawnShipParts() {
		if (shipPartSpawnLocations[0] != null) {
			int rand = Random.Range (0, shipPartSpawnLocations.Length);
			Instantiate (shipPartPrefab, shipPartSpawnLocations [rand].transform.position, shipPartSpawnLocations [rand].transform.rotation);
			game.resetShipPartSpawnDelay ();
		}
	}

	void checkForDeath() {
		if (player != null && player.GetComponent<PlayerController> ().grubby.health <= 0) {
			UI.healthDeathMessage ();
			destroySpawnLocations ();
		} else if (ship != null && ship.GetComponent<ShipController> ().ship.health <= 0) {
			game.timeRemaining = 0;
			UI.shipDestroyedMessage ();
			destroySpawnLocations ();
		}
	}

	void destroySpawnLocations () {
		foreach (GameObject spawnLocation in spawnLocations)
			Destroy (spawnLocation);
	}


}
	
