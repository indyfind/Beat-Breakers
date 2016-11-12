using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour {
	public GameObject countdown;
	private Text countdownText;

	// Use this for initialization
	void Start () {
		countdownText = countdown.GetComponent<Text> ();
		//Debug.Log (countdownText.text);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setText (string text) {
		//Debug.Log (countdownText.text);
		countdownText.text = text;
	}
}
