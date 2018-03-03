using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum difficultyLevel { easy, medium, hard }

public class Game {
	const float TIME_EASY = 90.0f;
	const float TIME_MEDIUM = 120.0f;
	const float TIME_HARD = 120.0f;

	const int PARTS_REQUIRED_EASY = 2;
	const int PARTS_REQUIRED_MEDIUM = 5;
	const int PARTS_REQUIRED_HARD = 8;

	// ship parts
	const float PART_SPAWN_DELAY_EASY = 22.5f;
	const float PART_SPAWN_DELAY_MEDIUM = 15.0f;
	const float PART_SPAWN_DELAY_HARD = 8.5f;

	// enemies
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
	public float partSpawnDelay;
	public float partSpawnAmount = 1;
	public float enemySpawnDelay;
	public int enemySpawnAmount;
	public bool betweenWaves = true;

	public Game(difficultyLevel theDifficulty) {
		difficulty = theDifficulty;

		// modify these
		switch (difficulty) {
		case difficultyLevel.easy:
			partsRequired = PARTS_REQUIRED_EASY;
			timeRemaining = TIME_EASY;
			partSpawnDelay = PART_SPAWN_DELAY_EASY;
			enemySpawnDelay = SPAWN_DELAY_EASY;
			enemySpawnAmount = SPAWN_AMOUNT_EASY * players;
			break;
		case difficultyLevel.medium:
			partsRequired = PARTS_REQUIRED_MEDIUM;
			timeRemaining = TIME_MEDIUM;
			partSpawnDelay = PART_SPAWN_DELAY_MEDIUM;
			enemySpawnDelay = SPAWN_DELAY_MEDIUM;
			enemySpawnAmount = SPAWN_AMOUNT_MEDIUM * players;
			break;
		case difficultyLevel.hard:
			partsRequired = PARTS_REQUIRED_HARD;
			timeRemaining = TIME_HARD;
			partSpawnDelay = PART_SPAWN_DELAY_HARD;
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

	public void resetShipPartSpawnDelay() {
		switch (difficulty) {
		case difficultyLevel.easy:
			partSpawnDelay = PART_SPAWN_DELAY_EASY;
			break;
		case difficultyLevel.medium:
			partSpawnDelay = PART_SPAWN_DELAY_MEDIUM;
			break;
		case difficultyLevel.hard:
			partSpawnDelay = PART_SPAWN_DELAY_HARD;
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
