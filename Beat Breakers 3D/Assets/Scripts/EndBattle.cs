using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndBattle : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public Text playerWinsText;
    public GameObject battleMaster;

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
            playerWinsText.text = "Battle Over!";
            player1.gameObject.SetActive(false);
            battleMaster.GetComponent<BattleStats>().winner = 2;
        }
        else
        {
            playerWinsText.text = "Battle Over!";
            player2.gameObject.SetActive(false);
            battleMaster.GetComponent<BattleStats>().winner = 1;
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
