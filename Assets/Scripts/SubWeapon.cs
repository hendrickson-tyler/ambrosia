using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeapon {
	public enum bombType { garlic, chili, honey }

	public bombType type = bombType.chili;

	public bool detonated = false;
	public float delay = 2.0f;

	public SubWeapon() {
		
	}

	public void tick(float time) {
		delay -= time;
	}
}
