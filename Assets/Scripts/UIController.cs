using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController {
	const float SHIP_ATTACKED_TIMEOUT = 1.0f;

	GameObject HUD;
	Image damageFrame;
	Image ammoBar;
	Image rechargingBar;
	GameObject omNombBombIcon;
	Text waveCounter;
	Text timer;
	Text partsCollected;
	Text message;
	Text messageDescription;
	Text subMessage;
	GameObject partIcon;
	GameObject deathCam;

	float shipAttackedDelay = 0;
	bool freezeUI = false;

	public void initializeUI () {
		damageFrame = GameObject.Find ("DamageFrame").GetComponent<Image> ();
		ammoBar = GameObject.Find ("AmmoBar").GetComponent<Image> ();
		rechargingBar = GameObject.Find ("RechargeIcon").GetComponent<Image> ();
		omNombBombIcon = GameObject.Find ("OmNomBombIcon");
		waveCounter = GameObject.Find ("Wave").GetComponent<Text> ();
		timer = GameObject.Find("Time").GetComponent<Text>();
		partsCollected = GameObject.Find ("PartsCollected").GetComponent<Text> ();
		partIcon = GameObject.Find ("PartIcon");
		message = GameObject.Find ("Message").GetComponent<Text>();
		messageDescription = GameObject.Find ("MessageDescription").GetComponent<Text>();
		subMessage = GameObject.Find ("SubMessage").GetComponent<Text>();
		deathCam = GameObject.Find ("DeathCam");

		Color alpha = damageFrame.color;
		alpha.a = 0;
		damageFrame.color = alpha;

		//disable elements
		ammoBar.fillAmount = 0;
		rechargingBar.fillAmount = 0;
		omNombBombIcon.SetActive (false);
		message.gameObject.SetActive(false);
		messageDescription.gameObject.SetActive(false);
		subMessage.gameObject.SetActive(false);
		partIcon.SetActive (false);
		deathCam.SetActive (false);
	}

	// essentially an update function
	public void countDown(float time) {
		if (shipAttackedDelay <= 10.0f)
			shipAttackedDelay -= time;

		if (shipAttackedDelay <= 0) {
			shipAttackedDelay = 20.0f;
			clearSubMessage ();
			subMessage.GetComponent<Text> ().color = Color.white;
		}
		// ammo bar
		if (GameObject.Find ("Player") != null && GameObject.Find ("Player").GetComponent<PlayerController> ().grubby.blenderBlaster.currentCharge < 100.0f) {
			ammoBar.fillAmount = GameObject.Find ("Player").GetComponent<PlayerController> ().grubby.blenderBlaster.currentCharge / 100.0f;
		} else {
			ammoBar.fillAmount = 0.0f;
		}

		// recharge bar
		if (GameObject.Find ("Player") != null && GameObject.Find ("Player").GetComponent<PlayerController> ().grubby.omNomBomb.throwDelay != 4.0f) {
			omNombBombIcon.SetActive (true);
			rechargingBar.fillAmount = GameObject.Find ("Player").GetComponent<PlayerController> ().grubby.omNomBomb.throwDelay / 4.0f;
		} else {
			omNombBombIcon.SetActive (false);
			rechargingBar.fillAmount = 0.0f;
		}
	}

	public void healthDeathMessage() {
		if (!freezeUI) {
			message.text = "OUCH!";
			message.gameObject.SetActive (true);
			messageDescription.text = "You got chomped!";
			messageDescription.gameObject.SetActive (true);
			freezeUI = true;
		}
		deathCam.SetActive (true);
	}

	public void fallingDeathMessage() {
		if (!freezeUI) {
			message.GetComponent<Text> ().text = "WHOOPS!";
			message.gameObject.SetActive (true);
			messageDescription.text = "Somehow you fell to your death!";
			messageDescription.gameObject.SetActive (true);

			Color alpha = damageFrame.color;
			alpha.a = 1.0f;
			damageFrame.color = alpha;

			freezeUI = true;
		}
		deathCam.SetActive (true);
	}

	public void drowningDeathMessage() {
		if (!freezeUI) {
			message.GetComponent<Text> ().text = "KERPLUNK!";
			message.gameObject.SetActive (true);
			messageDescription.text = "Grubbies can't swim!";
			messageDescription.gameObject.SetActive (true);

			Color alpha = damageFrame.color;
			alpha.a = 1.0f;
			damageFrame.color = alpha;

			freezeUI = true;
		}
		deathCam.SetActive (true);
	}

	public void shipUnderAttackMessage() {
		if (!freezeUI) {
			shipAttackedDelay = SHIP_ATTACKED_TIMEOUT;
			subMessage.text = "Your ship is under attack!";
			subMessage.color = Color.red;
			subMessage.gameObject.SetActive (true);
		}
	}

	public void shipDestroyedMessage() {
		if (!freezeUI) {
			message.text = "BOOM!";
			message.gameObject.SetActive (true);
			messageDescription.text = "Your only means of escape was destroyed!";
			messageDescription.gameObject.SetActive (true);

			Color alpha = damageFrame.color;
			alpha.a = 1.0f;
			damageFrame.color = alpha;

			freezeUI = true;
		}
	}

	public void partsNotCollectedMessage() {
		if (!freezeUI) {
			message.text = "BUMMER...";
			message.gameObject.SetActive (true);
			messageDescription.text = "You failed to collect all of the ship parts...";
			messageDescription.gameObject.SetActive (true);
			freezeUI = true;
		}
	}

	public void partAlreadyHeldMessage() {
		subMessage.text = "You need to return the currently held ship part first.";
		subMessage.gameObject.SetActive (true);
	}

	public void waveCompleteMessage(int waveNumber) {
		if (!freezeUI) {
			message.text = "WAVE " + waveNumber + " COMPLETE!";
			message.gameObject.SetActive (true);
		}
	}

	public void waveCountdownMessage(int waveNumber, float roundDelay) {
		if (!freezeUI) {
			subMessage.text = "Wave " + waveNumber + " starting in " + roundDelay.ToString ("F0") + "...";
			subMessage.gameObject.SetActive (true);
		}
	}

	public void gameWonMessage() {
		if (!freezeUI) {
			message.text = "CONGRATULATIONS!";
			message.gameObject.SetActive (true);
			messageDescription.text = "You live to be eaten another day!";
			messageDescription.gameObject.SetActive (true);
			freezeUI = true;
		}
	}

	public void updateTime(float time) {
		if (!freezeUI) {
			timer.text = time.ToString ("F0");
		}
	}

	public void updatePartsReturned(int returned, int required) {
		partsCollected.text = returned + " / " + required;
	}

	public void updatePartIcon(bool partHeld) {
		if (partHeld)
			partIcon.SetActive (true);
		else
			partIcon.SetActive (false);
	}

	public void updateDamageFrame(float health) {
		if (GameObject.Find("Player") != null && !freezeUI) {
			Color alpha = damageFrame.color;
			alpha.a = Mathf.Abs(health - 100) / 100;
			damageFrame.color = alpha;
		}
	}

	public void beginWave(int waveNumber) {
		waveCounter.text = "WAVE " + waveNumber;
		message.gameObject.SetActive (false);
		subMessage.gameObject.SetActive (false);
	}

	public void clearSubMessage() {
		subMessage.gameObject.SetActive (false);
	}
}
