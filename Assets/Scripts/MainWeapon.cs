using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ammoType { vegetable, smoothie, lemonade } //determine names


public class MainWeapon {
	ammoType ammo;

	public float range;
	public int damage;
	public float currentCharge;
	public float rechargeRate;

	public MainWeapon(ammoType theAmmo ) {
		ammo = theAmmo;

		switch (ammo) {
		case ammoType.vegetable:
			damage = 20;
			range = 7.5f;
			break;
		case ammoType.smoothie:
			damage = 5;
			range = 15.0f;
			break;
		case ammoType.lemonade:
			damage = 2;
			range = 25.0f;
			break;
		}
	}
}
