using UnityEngine;
using System.Collections;
using SonicBloom.Koreo;
using UnityEngine.UI;

public class BeatKeeper2 : MonoBehaviour {
	public GameObject player1;
	public GameObject player2;
	private VanillaCharacter char1script;
	private VanillaCharacter char2script;
	private AudioSource battleSong;
	private bool onBeat;
	private bool beatHappened;
	public string rhythmRating = "Good!";
	public GameObject gridModel;
	private GameObject battleMaster;
	private GameObject[] blocks;
	private GameObject[] evenSpaces;
	private GameObject[] oddSpaces;
	private Color gridColor1;
	private Color gridColor2;
	private Color gridColor3;
	private int everyOtherBeat = 1;
	public bool battleStarted = false;
	private int countdown = 3;
	private int beatsLeft = 332;
	private Text UIText;
	public GameObject timer;
	private Text timerText;

	// Use this for initialization
	void Start () {
		battleMaster = GameObject.FindGameObjectWithTag ("BattleMaster");
		battleSong = GetComponent<AudioSource>();
		UIText = GameObject.FindGameObjectWithTag ("MainText").GetComponent<Text>();
		char1script = player1.GetComponent<VanillaCharacter>();
		char2script = player2.GetComponent<VanillaCharacter>();
		timerText = timer.GetComponent<Text>();
		//set character scripts based on which character it is
//		switch (player1.tag){
//		case "Eva":
//			char1script = player1.GetComponent<VanillaCharacter>();
//			break;
//		default:
//			break;
//		}
//		switch (player2.tag){
//		case "Eva":
//			char1script = player1.GetComponent<VanillaCharacter>();
//			break;
//		default:
//			break;
//		}
		blocks = GameObject.FindGameObjectsWithTag("BeatBlocks");
		evenSpaces = GameObject.FindGameObjectsWithTag ("GridSpace2");
		oddSpaces = GameObject.FindGameObjectsWithTag ("GridSpace1");
		//gridColor1 = new Color (154f/255f, 149f/255f, 135f/255f,  1f); //new Color (0f, .6f, .6f, 1f); // new Color (0f, .90f, .90f, 1f);
		gridColor1 = new Color(88f / 255f, 153f / 255f, 105f / 255f, 1f);// Color.blue * Color.white; //- gridColor3;
		gridColor2 = new Color (196f/255f, 189f/255f, 172f/255f, 1f); //new Color (0f, .4f, .4f, 1f); //new Color (.196f, .189f, .172f, 1f);
		gridColor3 = new Color(64f/255f, 124f/255f, 183f/255f, 1f); //new Color (88f/255f, 153f/255f, 105f/255f, 1f);
		//set grid colors
		foreach (GameObject space in evenSpaces)
		{
			space.GetComponent<MeshRenderer> ().material.color = gridColor2;
		}
		foreach (GameObject space in oddSpaces)
		{
			space.GetComponent<MeshRenderer> ().material.color = gridColor3;
		}
		//set timer
		timerText.text = beatsLeft.ToString ();
		//koreographer listen for events
		Koreographer.Instance.RegisterForEvents("beat", OnBeat);
		Koreographer.Instance.RegisterForEvents("good", OnGoodSpan);
		Koreographer.Instance.RegisterForEvents("great", OnGreatSpan);
		Koreographer.Instance.RegisterForEvents("perfect", OnPerfectSpan);
		//start battle
		StartCoroutine(startgame());
	}
	//when beat actually happens
	void OnBeat (KoreographyEvent evt) {
		//if battle started, do both player's actions
		if (countdown < 0)
		{
			this.GetComponent<DoPlayerActions>().DoCurrentAction();
			player1.GetComponent<VanillaCharacter>().DoRhythmRating();
			player2.GetComponent<VanillaCharacter>().DoRhythmRating();
			player1.GetComponent<VanillaCharacter>().currentAction = "";
			player2.GetComponent<VanillaCharacter>().currentAction = "";
		}

		//if during the first 3 beats, do battle countdown and start beat blocks
		if (countdown > 0) {
            if (countdown == 3)
            {
                battleMaster.GetComponent<SoundPlayer>().PlaySound("Countdown", true);
                //Debug.Log("play countdown sound");
            }
			UIText.text = countdown.ToString ();
			foreach (GameObject block in blocks) {
				block.GetComponent<BlockMover> ().BattleStart (4 - countdown);
			}
			countdown--;
		} else if (countdown == 0) {
			UIText.text = "Dance!";
			//set actions to null
			char1script.currentAction = "";
			char2script.currentAction = "";
			foreach (GameObject block in blocks) {
				block.GetComponent<BlockMover> ().BattleStart (4 - countdown);
			}
			countdown--;
		} else if (countdown == -1) {
			UIText.text = "";
			countdown--;
		}

		//update timer
		if (countdown <= -1) {
			beatsLeft--;
			timerText.text = (beatsLeft.ToString ());
		}
		//every other beat:
		//	update grid colors
		//	increase each player's meter by 1
		if (everyOtherBeat == 0) {
			foreach (GameObject space in evenSpaces)
			{
				space.GetComponent<MeshRenderer> ().material.color = gridColor1;
			}
			foreach (GameObject space in oddSpaces)
			{
				space.GetComponent<MeshRenderer> ().material.color = gridColor2;
			}
			everyOtherBeat++;
		} else {
			foreach (GameObject space in evenSpaces)
			{
				space.GetComponent<MeshRenderer> ().material.color = gridColor2;
			}
			foreach (GameObject space in oddSpaces)
			{
				space.GetComponent<MeshRenderer> ().material.color = gridColor3;
			}
			char1script.meter++;
			char2script.meter++;
			everyOtherBeat--;
		}
	}
	void OnGoodSpan(KoreographyEvent evt) {
		char1script.ReadInput("Good!");
		char2script.ReadInput("Good!");
	}

	void OnGreatSpan(KoreographyEvent evt) {
		char1script.ReadInput("Great!");
		char2script.ReadInput("Great!");
	}

	void OnPerfectSpan(KoreographyEvent evt) {
		char1script.ReadInput("Perfect!");
		char2script.ReadInput("Perfect!");
	}

	IEnumerator startgame()
	{
		if (battleMaster.GetComponent<EndBattle> ().round == 1) {
			yield return new WaitForSeconds (1.5f);
			this.GetComponent<CameraSwitch> ().ShowPlayer1Camera ();
			yield return new WaitForSeconds (1f);
			this.GetComponent<CameraSwitch> ().ShowPlayer2Camera ();
			yield return new WaitForSeconds (1f);
			this.GetComponent<CameraSwitch> ().ShowMainCamera ();
		}
		UIText.text = "Round " + battleMaster.GetComponent<EndBattle>().round;
        battleMaster.GetComponent<SoundPlayer>().PlaySound("Round" + battleMaster.GetComponent<EndBattle>().round, true);
		yield return new WaitForSeconds(1f);
		battleStarted = true;
		battleSong.Play();
	}

	public bool checkifonbeat(){
		return true;
	}

}
