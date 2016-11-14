using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndBattle : MonoBehaviour {
    //public GameObject player1;
    //public GameObject player2;
	private Text playerWinsText;
	private int player1wins = 0;
	private int player2wins = 0;
	private int round = 1;

    // Use this for initialization
    void Start () {
		playerWinsText = GameObject.FindGameObjectWithTag ("PlayerWinsText").GetComponent<Text>();
		playerWinsText.text = "Round " + round;
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
        playerWinsText.text = "draw!";
		this.GetComponent<BattleStats> ().winner = 3;
        StartCoroutine(End(2));
    }

	IEnumerator End(int sceneToLoad)
    {
		playerWinsText.text = "Battle Over!";
        yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(sceneToLoad);
        //SceneManager.LoadScene(0);
    }
}
