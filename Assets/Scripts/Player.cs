using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	MainWeapon blenderBlaster;
	SubWeapon omNomBom;

	public float health = 100; //change to int?
	public int healthRegen = 2;
	public bool partHeld = false;
}
