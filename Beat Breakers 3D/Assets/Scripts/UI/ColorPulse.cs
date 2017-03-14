using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPulse : MonoBehaviour {
	private float duration = 0.5f;
	private float t = 0f;
	public Color color1 = Color.magenta;
	public Color color2 = Color.white;
	public bool pulsing = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//meter glow
		if (t < 1) {
			t += Time.deltaTime/duration;
		} else {
			pulsing = false;
		}
		if (pulsing) {
			this.GetComponent<Image>().color = Color.Lerp(color2, color1, t);
		} else {
			this.GetComponent<Image>().color = color1;
		}
	}

	public void Pulse(){
		pulsing = true;
		t = 0f;
	}
}
