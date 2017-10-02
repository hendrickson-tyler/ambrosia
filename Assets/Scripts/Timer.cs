using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public Text counter;
	public int seconds = 150;

	// Use this for initialization
	void Start () {
		counter = GetComponent<Text> () as Text;
	}

	// Update is called once per frame
	void Update () {
		counter.text = Mathf.Clamp(seconds - (int)Time.timeSinceLevelLoad, 0, seconds).ToString();

		if (counter.text == "0") {
			counter.text = "TIME UP";
			Application.Quit();
		}
	}
}
