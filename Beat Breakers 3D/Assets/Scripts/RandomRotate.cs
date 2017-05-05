using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour {
	private int i = 0;
	private Quaternion from;
	private Quaternion to;
	// Use this for initialization
	void Start () {
		from = transform.rotation;
		to = Random.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (i < 60) {
			if (i == 0) {
				from = transform.rotation;
				to = Random.rotation;
			}
			transform.rotation = Quaternion.Lerp(from, to, Time.time * 0.1f);
			i++;
		} else {
			i = 0;
		}
	}
}
