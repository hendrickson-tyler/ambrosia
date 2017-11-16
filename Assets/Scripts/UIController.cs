using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController {
	const float SHIP_ATTACKED_TIMEOUT = 1.0f;

	Image damageFrame;
	Image rechargingBar;
	GameObject omNombBombIcon;
	Text waveCounter;
	Text timer;
	Text partsCollected;
	GameObject message;
	GameObject messageDescription;
	GameObject subMessage;
	GameObject partIcon;
	GameObject deathCam;

	float shipAttackedDelay = 0;
	bool freezeUI = false;

	public void initializeUI () {
		damageFrame = GameObject.Find ("DamageFrame").GetComponent<Image> ();
		rechargingBar = GameObject.Find ("RechargeIcon").GetComponent<Image> ();
		omNombBombIcon = GameObject.Find ("OmNomBombIcon");
		waveCounter = GameObject.Find ("Wave").GetComponent<Text> ();
		timer = GameObject.Find("Time").GetComponent<Text>();
		partsCollected = GameObject.Find ("PartsCollected").GetComponent<Text> ();
		partIcon = GameObject.Find ("PartIcon");
		message = GameObject.Find ("Message");
		messageDescription = GameObject.Find ("MessageDescription");
		subMessage = GameObject.Find ("SubMessage");
		deathCam = GameObject.Find ("DeathCam");

		Color alpha = damageFrame.color;
		alpha.a = 0;
		damageFrame.color = alpha;

		//disable elements
		rechargingBar.fillAmount = 0;
		omNombBombIcon.SetActive (false);
		message.SetActive(false);
		messageDescription.SetActive(false);
		subMessage.SetActive(false);
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
			message.GetComponent<Text> ().text = "OUCH!";
			message.SetActive (true);
			messageDescription.GetComponent<Text> ().text = "You got chomped!";
			messageDescription.SetActive (true);
			freezeUI = true;
		}
		deathCam.SetActive (true);
	}

	public void fallingDeathMessage() {
		if (!freezeUI) {
			message.GetComponent<Text> ().text = "WHOOPS!";
			message.SetActive (true);
			messageDescription.GetComponent<Text> ().text = "Somehow you fell to your death!";
			messageDescription.SetActive (true);

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
			subMessage.GetComponent<Text> ().text = "Your ship is under attack!";
			subMessage.GetComponent<Text> ().color = Color.red;
			subMessage.SetActive (true);
		}
	}

	public void shipDestroyedMessage() {
		if (!freezeUI) {
			message.GetComponent<Text> ().text = "BOOM!";
			message.SetActive (true);
			messageDescription.GetComponent<Text> ().text = "Your only means of escape was destroyed!";
			messageDescription.SetActive (true);

			Color alpha = damageFrame.color;
			alpha.a = 1.0f;
			damageFrame.color = alpha;

			freezeUI = true;
		}
	}

	public void partsNotCollectedMessage() {
		if (!freezeUI) {
			message.GetComponent<Text> ().text = "BUMMER...";
			message.SetActive (true);
			messageDescription.GetComponent<Text> ().text = "You failed to collect all of the ship parts...";
			messageDescription.SetActive (true);
			freezeUI = true;
		}
	}

	public void partAlreadyHeldMessage() {
		subMessage.GetComponent<Text> ().text = "You need to return the currently held ship part first.";
		subMessage.SetActive (true);
	}

	public void waveCompleteMessage(int waveNumber) {
		if (!freezeUI) {
			message.GetComponent<Text> ().text = "WAVE " + waveNumber + " COMPLETE!";
			message.SetActive (true);
		}
	}

	public void waveCountdownMessage(int waveNumber, float roundDelay) {
		if (!freezeUI) {
			subMessage.GetComponent<Text> ().text = "Wave " + waveNumber + " starting in " + roundDelay.ToString ("F0") + "...";
			subMessage.SetActive (true);
		}
	}

	public void gameWonMessage() {
		if (!freezeUI) {
			message.GetComponent<Text> ().text = "CONGRATULATIONS!";
			message.SetActive (true);
			messageDescription.GetComponent<Text> ().text = "You live to be eaten another day!";
			messageDescription.SetActive (true);
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
		message.SetActive (false);
		subMessage.SetActive (false);
	}

	public void clearSubMessage() {
		subMessage.SetActive (false);
	}
}
