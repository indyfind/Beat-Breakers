using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndBattle : MonoBehaviour {
    //public GameObject player1;
    //public GameObject player2;
	private Text UIText;
	private int player1wins = 0;
	private int player2wins = 0;
	public int round = 1;

    // Use this for initialization
    void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playerLoses(int player)
    {
        if (player == 1)
        {
			player2wins += 1;
			//player1.gameObject.SetActive (false);
        }
        else
        {
			player1wins += 1;
			//player2.gameObject.SetActive (false);
		}
		if (player1wins >= 2) {
			//player2.gameObject.SetActive (false);
			//Destroy (player2);
			this.GetComponent<BattleStats> ().winner = 1;
			StartCoroutine (End (2));
		} else if (player2wins >= 2) {
			//player1.gameObject.SetActive (false);
			//Destroy (player1);
			this.GetComponent<BattleStats> ().winner = 2;
			StartCoroutine (End (2));
		} else {
			StartCoroutine (End (1));
		}
    }

    public void Tie()
    {
		UIText = GameObject.FindGameObjectWithTag ("MainText").GetComponent<Text>();
		UIText.text = "Draw!";
        StartCoroutine(End(1));
    }

	IEnumerator End(int sceneToLoad)
    {
		UIText = GameObject.FindGameObjectWithTag ("MainText").GetComponent<Text>();
		round += 1;
		Debug.Log(round);
		UIText.text = "Round Over!";
        yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(sceneToLoad);
        //SceneManager.LoadScene(0);
    }
}
