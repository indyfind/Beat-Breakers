using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	public GameObject timer;
	private Text timerText;

	// Use this for initialization
	void Start () {
		timerText = timer.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setText (string text) {
		//Debug.Log (countdownText.text);
		timerText.text = text; 	
	}
}
