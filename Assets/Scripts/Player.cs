using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
	public MainWeapon blenderBlaster;
	public SubWeapon omNomBomb;

	public float speed = 15.0f;
	public float walkingSpeed = 15.0f;
	public float sprintingSpeed = 30.0f;
	public float jumpHeight = 5.0f;
	public float regenDelay = 4.0f;

	public float health = 100; //change to int?
	public int healthRegen = 1;
	public bool partHeld = false;

	public Player(ammoType theAmmoType, bombType theBombtype) {
		blenderBlaster = new MainWeapon (theAmmoType);
		omNomBomb = new SubWeapon (theBombtype);
	}

	public void takeDamage(int damage) {
		health -= damage;
		regenDelay = 4.0f;
	}

	public void regenHealth(float time) {
		if (health < 100)
			health += healthRegen;
	}
}
