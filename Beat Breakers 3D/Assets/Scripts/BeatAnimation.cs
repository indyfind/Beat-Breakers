using UnityEngine;
using System.Collections;

public class BeatAnimation : MonoBehaviour {

	private Vector3 originalScale;
	private Vector3 newScale;

	// Use this for initialization
	void Start () {
		originalScale = this.GetComponent<Transform> ().localScale;
		newScale = new Vector3 (originalScale.x * 1.15f, originalScale.y * 1.15f, originalScale.z * 1.15f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Pulse()
	{
		this.transform.localScale = newScale;
		StartCoroutine(PulseLength());
	}

	IEnumerator PulseLength()
	{
		yield return new WaitForSeconds (.1f);
		this.transform.localScale = originalScale;
	}
}
