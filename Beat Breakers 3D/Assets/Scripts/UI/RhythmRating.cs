using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RhythmRating : MonoBehaviour {
	public Text rhythmRating1;
	public Text rhythmRating2;
	public Text rhythmRating3;
	public Transform startMarker;
	public Transform endMarker;
	private int counter = 1;

	void Start() {
		
	}

	void Update() {
		
	}

	public void DisplayRating () {
		if (counter < 3) {
			counter++;
		} else {
			counter = 1;
		}
		//Debug.Log("counter = " + counter);
		if (counter == 1) {
			StartCoroutine(Fade(rhythmRating1));
		} else if (counter == 2) {
			StartCoroutine(Fade(rhythmRating2));
		} else {
			StartCoroutine(Fade(rhythmRating3));
		}
	}

	IEnumerator Fade (Text currentRhythmRatingUI) {
		//Debug.Log("rhythm rating displayed");
		string rating = this.GetComponent<VanillaCharacter>().rhythmRating;
		if (rating == "Good!") {
			currentRhythmRatingUI.color = Color.white;
		} else if (rating == "Great!") {
			currentRhythmRatingUI.color = (Color.white + Color.green) / 2;
		} else if (rating == "Perfect!") {
			currentRhythmRatingUI.color = Color.green;
		}
		currentRhythmRatingUI.text = rating;
		float t = 0f;
		while (t <= 1) {
			t += Time.deltaTime / 1f;
			currentRhythmRatingUI.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, t);
			currentRhythmRatingUI.GetComponent<CanvasRenderer>().SetAlpha(1f - t);
			yield return null;
		}
		currentRhythmRatingUI.text = "";
		currentRhythmRatingUI.transform.position = startMarker.position;
		currentRhythmRatingUI.GetComponent<CanvasRenderer>().SetAlpha(1f);
	}
		
}
