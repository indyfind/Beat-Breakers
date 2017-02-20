using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class StatWriter : MonoBehaviour {

    private GameObject statMaster;
    public Text WinnerText, PerfectP1UI, PerfectP2UI, GreatP1UI, GreatP2UI, GoodP1UI, GoodP2UI, MaxComboP1UI, MaxComboP2UI;
    public Text ScoreP1UI, ScoreP2UI, FavoriteAttackP1UI, FavoriteAttackP2UI;
    private int tempScoreP1, tempScoreP2;
    private bool buttonpressed;
    public GameObject StyleMaster1, StyleMaster2;
    private int winner;

	// Use this for initialization
	void Start () {
        buttonpressed = false;
        statMaster = GameObject.Find("EternalBattleMaster");
        PerfectP1UI.text =  statMaster.GetComponent<BattleStats>().PerfectsP1.ToString();
        PerfectP2UI.text = statMaster.GetComponent<BattleStats>().PerfectsP2.ToString();
        GreatP1UI.text = statMaster.GetComponent<BattleStats>().GreatsP1.ToString();
        GreatP2UI.text = statMaster.GetComponent<BattleStats>().GreatsP2.ToString();
        GoodP1UI.text = statMaster.GetComponent<BattleStats>().GoodP1.ToString();
        GoodP2UI.text = statMaster.GetComponent<BattleStats>().GoodP2.ToString();
        MaxComboP1UI.text = statMaster.GetComponent<BattleStats>().MaxComboP1.ToString();
        MaxComboP2UI.text = statMaster.GetComponent<BattleStats>().MaxComboP2.ToString();
        winner = statMaster.GetComponent<BattleStats>().winner;
        if (winner == 1)
        {
            WinnerText.text = "Player 1 Wins!";
        } else
        {
            WinnerText.text = "Player 2 Wins!";
        }

        DoScore();
        FavoriteAttack("p1");
        FavoriteAttack("p2");
        //StartCoroutine(FirstClick());
        statMaster.GetComponent<BattleStats>().ResetStats();
        Destroy(statMaster);


    }
	
    private void DoScore()
    {
        double score1 = 0;
        double score2 = 0;
        double good = statMaster.GetComponent<BattleStats>().GoodP1 * 60;
        double great = statMaster.GetComponent<BattleStats>().GreatsP1 * 80;
        double perfect = statMaster.GetComponent<BattleStats>().PerfectsP1 * 100;
        double multiplier = (1 + (statMaster.GetComponent<BattleStats>().MaxComboP1 * .01));
        score1 = (good + great + perfect) * multiplier;
        ScoreP1UI.text = score1.ToString();
        
        good = statMaster.GetComponent<BattleStats>().GoodP2 * 60;
        great = statMaster.GetComponent<BattleStats>().GreatsP2 * 80;
        perfect = statMaster.GetComponent<BattleStats>().PerfectsP2 * 100;
        multiplier = (1 + (statMaster.GetComponent<BattleStats>().MaxComboP2 * .01));
        score2 = (good + great + perfect) * multiplier;
        ScoreP2UI.text = score2.ToString();
        
        if (score1 > score2)
        {
            StyleMaster1.SetActive(true);
        } else if (score1 < score2)
        {
            StyleMaster2.SetActive(true);
        } else
        {
            StyleMaster1.SetActive(true);
            StyleMaster2.SetActive(true);
        }
    }

    private void FavoriteAttack(string player)
    {
        if (player == "p1")
        {
			int rangedAttackP1 = statMaster.GetComponent<BattleStats>().RangedAttackP1;
			int meleeAttackP1 = statMaster.GetComponent<BattleStats>().MeleeAttackP1;
			string fav;
			if (rangedAttackP1 >= meleeAttackP1)
            {
                Debug.Log("First if fired");
                fav = "Ranged Attack";
			} else {
				fav = "Melee Attack";
			}
            FavoriteAttackP1UI.text = fav;
        }
		if (player == "p2")
		{
			int rangedAttackP2 = statMaster.GetComponent<BattleStats>().RangedAttackP2;
			int meleeAttackP2 = statMaster.GetComponent<BattleStats>().MeleeAttackP2;
			string fav;
			if (rangedAttackP2 >= meleeAttackP2)
			{
				Debug.Log("First if fired");
				fav = "Ranged Attack";
			} else {
				fav = "Melee Attack";
			}
			FavoriteAttackP1UI.text = fav;
		}
    }

    IEnumerator FirstClick()
    {
        while (!buttonpressed)
        {
            if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0) || InputManager.ActiveDevice.AnyButtonWasPressed)
            {
                SceneManager.LoadScene(0); ;
                buttonpressed = true;
                break;
            }
            yield return 0;
        }

    }
    // Update is called once per frame
    void Update () {
	
	}
}
