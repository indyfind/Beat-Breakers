using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OnButtonHighlight : MonoBehaviour {
	public AudioSource highlightSound;
	//public BaseEventData 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnHighlight(BaseEventData eventData)
	{
		//do your stuff when highlighted
		Debug.Log("SOUND");
		highlightSound.Play();
	}
}
