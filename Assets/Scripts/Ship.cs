using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship {
	public int health = 500;

	public void takeDamage(int damage) {
		health -= damage;
		Debug.Log (health);
	}
}
