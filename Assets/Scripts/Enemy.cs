using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy {
	public float health = 100;
	public int attackStrength = 20;
	public float attackDelay = 0;
	public bool attacking = false;

	public void takeDamage(int damage) {
		health -= damage;
	}
}
