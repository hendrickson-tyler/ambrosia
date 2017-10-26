using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ammoType { vegetable, smoothie, lemonade } //determine names


public class MainWeapon {
	ammoType ammo;

	public float range;
	public int damage;
	public float currentCharge = 100.0f;
	public float dischargeRate;
	public float rechargeRate;

	public MainWeapon(ammoType theAmmo ) {
		ammo = theAmmo;

		switch (ammo) {
		case ammoType.vegetable:
			damage = 20;
			range = 7.5f;
			dischargeRate = 15.0f;
			rechargeRate = 8.0f;
			break;
		case ammoType.smoothie:
			damage = 5;
			range = 15.0f;
			dischargeRate = 10.5f;
			rechargeRate = 8.0f;
			break;
		case ammoType.lemonade:
			damage = 2;
			range = 25.0f;
			dischargeRate = 2.5f;
			rechargeRate = 8.0f;
			break;
		}
	}

	public void discharge(float time) {
		currentCharge -= (time * dischargeRate);
		currentCharge = Mathf.Clamp (currentCharge, 0, 100);
	}

	public void recharge(float time) {
		currentCharge += (time * rechargeRate);
		currentCharge = Mathf.Clamp (currentCharge, 0, 100);
	}
}
