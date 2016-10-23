using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class StatWriter : MonoBehaviour {

    private GameObject statMaster;
    public Text PerfectP1UI, PerfectP2UI, GreatP1UI, GreatP2UI, GoodP1UI, GoodP2UI, MaxComboP1UI, MaxComboP2UI;
    public Text ScoreP1UI, ScoreP2UI, FavoriteAttackP1UI, FavoriteAttackP2UI;
    private int tempScoreP1, tempScoreP2;
    private bool buttonpressed;

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
        DoScore("p1");
        DoScore("p2");
        FavoriteAttack("p1");
        FavoriteAttack("p2");
        StartCoroutine(FirstClick());


    }
	
    private void DoScore(string player)
    {
        if (player == "p1")
        {
            double good = statMaster.GetComponent<BattleStats>().GoodP1 * 60;
            double great = statMaster.GetComponent<BattleStats>().GreatsP1 * 80;
            double perfect = statMaster.GetComponent<BattleStats>().PerfectsP1 * 100;
            double multiplier = (1 + (statMaster.GetComponent<BattleStats>().MaxComboP1 * .01));
            double score = (good + great + perfect) * multiplier;
            ScoreP1UI.text = score.ToString();
        }
        if (player == "p2")
        {
            double good = statMaster.GetComponent<BattleStats>().GoodP2 * 60;
            double great = statMaster.GetComponent<BattleStats>().GreatsP2 * 80;
            double perfect = statMaster.GetComponent<BattleStats>().PerfectsP2 * 100;
            double multiplier = (1 + (statMaster.GetComponent<BattleStats>().MaxComboP2 * .01));
            double score = (good + great + perfect) * multiplier;
            ScoreP2UI.text = score.ToString();
        }
    }

    

    private void FavoriteAttack(string player)
    {
        if (player == "p1")
        {
            int popandlock = statMaster.GetComponent<BattleStats>().PopLockP1;
            int headslide = statMaster.GetComponent<BattleStats>().HeadSlideP1;
            int sixstep = statMaster.GetComponent<BattleStats>().SixStepP1;
            string fav = "GlowString Swipes";
            if (headslide >= popandlock)
            {
                fav = "JumpStyle Slam";
            }
            if (sixstep >= popandlock && sixstep >= popandlock)
            {
                fav = "Circle Shufflin' ";
            }
            FavoriteAttackP1UI.text = fav;
        }
        if (player == "p2")
        {
            int popandlock = statMaster.GetComponent<BattleStats>().PopLockP2;
            int headslide = statMaster.GetComponent<BattleStats>().HeadSlideP2;
            int sixstep = statMaster.GetComponent<BattleStats>().SixStepP2;
            string fav = "GlowString Swipes";
            if (headslide >= popandlock)
            {
                fav = "JumpStyle Slam";
            }
            if (sixstep >= popandlock && sixstep >= popandlock)
            {
                fav = "Circle Shufflin' ";
            }
            FavoriteAttackP2UI.text = fav;
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
