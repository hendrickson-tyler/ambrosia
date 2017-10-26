using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum difficultyLevel { easy, medium, hard }

public class Game {

	const float SPAWN_DELAY_EASY = 10.0f;
	const float SPAWN_DELAY_MEDIUM = 8.0f;
	const float SPAWN_DELAY_HARD = 4.0f;

	const int SPAWN_AMOUNT_EASY = 3;
	const int SPAWN_AMOUNT_MEDIUM = 4;
	const int SPAWN_AMOUNT_HARD = 4;

	difficultyLevel difficulty;
	public bool won = false;
	int wave = 1;
	//int players = 1; //hard coded for now, never used
	public int partsReturned = 0;
	public int partsRequired;
	public int timeRemaining;
	public float enemySpawnDelay;
	public int enemySpawnAmount;

	public Game(difficultyLevel theDifficulty) {
		difficulty = theDifficulty;

		// modify these
		switch (difficulty) {
		case difficultyLevel.easy:
			partsRequired = 3;
			timeRemaining = 60;
			enemySpawnDelay = SPAWN_DELAY_EASY;
			enemySpawnAmount = SPAWN_AMOUNT_EASY;
			break;
		case difficultyLevel.medium:
			partsRequired = 5;
			timeRemaining = 120;
			enemySpawnDelay = SPAWN_DELAY_MEDIUM;
			enemySpawnAmount = SPAWN_AMOUNT_MEDIUM;
			break;
		case difficultyLevel.hard:
			partsRequired = 8;
			timeRemaining = 120;
			enemySpawnDelay = SPAWN_DELAY_HARD;
			enemySpawnAmount = SPAWN_AMOUNT_HARD;
			break;
		}
	}

	public void advanceWave() {
		wave++;
	}

	public void returnPart() {
		partsReturned++;
	}

	public void resetSpawnDelay() {
		switch (difficulty) {
		case difficultyLevel.easy:
			enemySpawnDelay = SPAWN_DELAY_EASY;
			break;
		case difficultyLevel.medium:
			enemySpawnDelay = SPAWN_DELAY_MEDIUM;
			break;
		case difficultyLevel.hard:
			enemySpawnDelay = SPAWN_DELAY_HARD;
			break;
		}
	}
}
