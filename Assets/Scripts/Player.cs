using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
	MainWeapon blenderBlaster = new MainWeapon();
	SubWeapon omNomBom = new SubWeapon();

	public float speed = 15.0f;
	public float walkingSpeed = 15.0f;
	public float sprintingSpeed = 30.0f;
	public float jumpHeight = 5.0f;

	public float health = 100; //change to int?
	public int healthRegen = 2;
	public bool partHeld = false;
}
