using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RhythmRating : MonoBehaviour {
    private int player;
	private Text rhythmRating1;
	private Text rhythmRating2;
	private Text rhythmRating3;
	private Transform startMarker;
	private Transform endMarker;
	private int counter = 1;

	void Start() {
        player = this.GetComponent<VanillaCharacter>().player;
        if (player == 1)
        {
            rhythmRating1 = GameObject.FindGameObjectWithTag("p1RhythmRating1").GetComponent<Text>();
            rhythmRating2 = GameObject.FindGameObjectWithTag("p1RhythmRating2").GetComponent<Text>();
            rhythmRating3 = GameObject.FindGameObjectWithTag("p1RhythmRating3").GetComponent<Text>();
            startMarker = GameObject.FindGameObjectWithTag("p1StartMarker").transform;
            endMarker = GameObject.FindGameObjectWithTag("p1EndMarker").transform;
        } else if (player == 2)
        {
            rhythmRating1 = GameObject.FindGameObjectWithTag("p2RhythmRating1").GetComponent<Text>();
            rhythmRating2 = GameObject.FindGameObjectWithTag("p2RhythmRating2").GetComponent<Text>();
            rhythmRating3 = GameObject.FindGameObjectWithTag("p2RhythmRating3").GetComponent<Text>();
            startMarker = GameObject.FindGameObjectWithTag("p2StartMarker").transform;
            endMarker = GameObject.FindGameObjectWithTag("p2EndMarker").transform;
        }
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
