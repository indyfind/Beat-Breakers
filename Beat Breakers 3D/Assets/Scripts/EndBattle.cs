using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndBattle : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public Text playerWinsText;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playerLoses(int player)
    {
        if (player == 1)
        {
            playerWinsText.text = "player 2 wins!";
            player1.gameObject.SetActive(false);
        }
        else
        {
            playerWinsText.text = "player 1 wins!";
            player2.gameObject.SetActive(false);
        }
        StartCoroutine(End());
    }

    public void Tie()
    {
        playerWinsText.text = "draw!";
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
        //SceneManager.LoadScene(0);
    }
}
