using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bombType { garlic, chili, honey }

public class SubWeapon {
	public bombType type = bombType.chili;

	public int damage;
	public float delay = 2.0f;
	public float throwDelay = 4.0f;
	public float range;
	public bool thrown = false;

	public SubWeapon(bombType theType) {
		type = theType;

		switch (type) {
		case bombType.garlic:
			break;
		case bombType.chili:
			damage = 60;
			range = 15.0f;
			break;
		case bombType.honey:
			break;
		}
	}

	public void tick(float time) {
		delay -= time;
	}	

	public void resetBomb() {
		switch (type) {
		case bombType.garlic:
			break;
		case bombType.chili:
			delay = 2.0f;
			throwDelay = 4.0f;
			thrown = false;
			break;
		case bombType.honey:
			break;
		}
	}
}
