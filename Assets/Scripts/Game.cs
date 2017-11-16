using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum difficultyLevel { easy, medium, hard }

public class Game {
	const float TIME_EASY = 90.0f;
	const float TIME_MEDIUM = 120.0f;
	const float TIME_HARD = 120.0f;

	const float SPAWN_DELAY_EASY = 15.0f;
	const float SPAWN_DELAY_MEDIUM = 8.0f;
	const float SPAWN_DELAY_HARD = 4.0f;

	const int SPAWN_AMOUNT_EASY = 3;
	const int SPAWN_AMOUNT_MEDIUM = 4;
	const int SPAWN_AMOUNT_HARD = 4;

	difficultyLevel difficulty;
	public bool won;
	public int wave = 1;
	int players = 1; //hard coded for now
	public int partsReturned = 0;
	public int partsRequired;
	public float timeRemaining;
	public float betweenWaveDelay = 8.0f;
	public float enemySpawnDelay;
	public int enemySpawnAmount;
	public bool betweenWaves = true;

	public Game(difficultyLevel theDifficulty) {
		difficulty = theDifficulty;

		// modify these
		switch (difficulty) {
		case difficultyLevel.easy:
			partsRequired = 2;
			timeRemaining = TIME_EASY;
			enemySpawnDelay = SPAWN_DELAY_EASY;
			enemySpawnAmount = SPAWN_AMOUNT_EASY * players;
			break;
		case difficultyLevel.medium:
			partsRequired = 5;
			timeRemaining = TIME_MEDIUM;
			enemySpawnDelay = SPAWN_DELAY_MEDIUM;
			enemySpawnAmount = SPAWN_AMOUNT_MEDIUM * players;
			break;
		case difficultyLevel.hard:
			partsRequired = 8;
			timeRemaining = TIME_HARD;
			enemySpawnDelay = SPAWN_DELAY_HARD;
			enemySpawnAmount = SPAWN_AMOUNT_HARD * players;
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

	public void resetTimeRemaining() {
		switch (difficulty) {
		case difficultyLevel.easy:
			timeRemaining = TIME_EASY;
			break;
		case difficultyLevel.medium:
			timeRemaining = TIME_MEDIUM;
			break;
		case difficultyLevel.hard:
			timeRemaining = TIME_HARD;
			break;
		}
	}
}
