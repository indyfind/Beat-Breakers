using UnityEngine;
using System.Collections;
using SonicBloom.Koreo;
using UnityEngine.UI;
using InControl;

public class BeatKeeper2 : MonoBehaviour {
	public GameObject player1;
	public GameObject player2;
	private VanillaCharacter char1script;
	private VanillaCharacter char2script;
	private AudioSource battleSong;
	private bool onBeat;
	private bool beatHappened;
	public GameObject gridModel;
	public GameObject hud;
	private GameObject battleMaster;
	private GameObject[] blocks;
	private GameObject[] evenSpaces;
	private GameObject[] oddSpaces;
	private GameObject[] projectiles;
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
	public GameObject p1meter;
	public GameObject p2meter;
	public GameObject p1HPBar;
	public GameObject p2HPBar;
	public GameObject spotlights;
    public bool ismusicplaying = false;
	//inputs
    private GameObject inputMaster;
	private InputDevice player1device;
    private InputDevice player2device;
	public PlayerKeyboardActions player1KeyboardActions;
	public PlayerKeyboardActions player2KeyboardActions;
    public GameObject pausebutton1, pausebutton2, pausebutton3;
	private bool battleOver = false;

    //private GameObject menuSong;

    // Use this for initialization
    void Start () {

        //find scene objects
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        battleMaster = GameObject.FindGameObjectWithTag ("BattleMaster");
		battleSong = GetComponent<AudioSource>();
		UIText = GameObject.FindGameObjectWithTag ("MainText").GetComponent<Text>();
        //menuSong = GameObject.FindGameObjectWithTag("MenuSong");

		char1script = player1.GetComponent<VanillaCharacter>();
		char2script = player2.GetComponent<VanillaCharacter>();
		timerText = timer.GetComponent<Text>();

		inputMaster = GameObject.FindGameObjectWithTag("InputMaster");
		if (inputMaster.GetComponent<InputMaster>().player1keyboard)
		{
			player1device = new InputDevice();
			player1KeyboardActions = inputMaster.GetComponent<InputMaster>().getP1Actions();
		}
		else
		{
			player1KeyboardActions = new PlayerKeyboardActions();
			player1device = inputMaster.GetComponent<InputMaster>().player1Controller;
		}
		if (inputMaster.GetComponent<InputMaster>().player2keyboard)
		{
			player2device = new InputDevice();
			player2KeyboardActions = inputMaster.GetComponent<InputMaster>().getP2Actions();
		}
		else
		{
			player2KeyboardActions = new PlayerKeyboardActions();
			player2device = inputMaster.GetComponent<InputMaster>().player2Controller;
		}
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
			space.GetComponent<MeshRenderer>().material.color = gridColor2;
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

    void Update()
    {
		if ((player1device.Command.WasPressed || player2device.Command.WasPressed || player1KeyboardActions.Select.WasPressed || player2KeyboardActions.Select.WasPressed) && (ismusicplaying))
        {
			//Debug.Log("game paused");
            pausebutton1.SetActive(true);
            pausebutton2.SetActive(true);
            pausebutton3.SetActive(true);
            ismusicplaying = false;
            this.GetComponent<AudioSource>().Pause();
            foreach (GameObject block in blocks)
            {
                block.GetComponent<BlockMover>().blockstopped();
            }
            this.GetComponent<DoPlayerActions>().pausecharacteranimations();
        }

		if (beatsLeft <= 0 && !battleOver) {
			battleOver = true;
			if (char1script.health > char2script.health) {
				battleMaster.GetComponent<EndBattle>().playerLoses(2);
			}
			else if (char2script.health > char1script.health) {
				battleMaster.GetComponent<EndBattle>().playerLoses(1);
			} else {
				battleMaster.GetComponent<EndBattle>().Tie();
			}
		}
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
			if (char1script.onFire) {
				p1meter.GetComponent<ColorPulse>().Pulse();
			}
			if (char2script.onFire) {
				p2meter.GetComponent<ColorPulse>().Pulse();
			}
			if (char1script.health <= 100) {
				p1HPBar.GetComponent<ColorPulse>().Pulse();
			}
			if (char2script.health <= 100) {
				p2HPBar.GetComponent<ColorPulse>().Pulse();
			}
			//find & move projectiles
			projectiles = GameObject.FindGameObjectsWithTag ("Projectile");
			foreach (GameObject projectile in projectiles)
			{
				//Debug.Log("move projectile");
				projectile.GetComponent<Projectile>().MoveProjectile();
			}
		}

		//if during the first 3 beats, do battle countdown and start beat blocks
		if (countdown > 0) {
            if (countdown == 3)
            {
                battleMaster.GetComponent<SoundPlayer>().PlaySound("Countdown", true);
                //Debug.Log("play countdown sound");
				hud.GetComponent<Canvas>().enabled = true;
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

    //unpauses the game
    public void ResumeGame()
    {
        this.GetComponent<AudioSource>().UnPause();
		StartCoroutine(musicPlayingTrue());
        foreach (GameObject block in blocks)
        {
            block.GetComponent<BlockMover>().blockrestarted();
        }
        this.GetComponent<DoPlayerActions>().unpausecharacteranimations();
		//Debug.Log("game resumed");
    }

    IEnumerator startgame()
	{
		if (battleMaster.GetComponent<EndBattle> ().round == 1 && battleMaster.GetComponent<EndBattle>().tieHappened == false) {
			yield return new WaitForSeconds (1f);
			/*
			spotlights.SetActive(true);
			this.GetComponent<CameraSwitch> ().ShowPlayer1Camera ();
			yield return new WaitForSeconds (1f);
			this.GetComponent<CameraSwitch> ().ShowPlayer2Camera ();
			yield return new WaitForSeconds (1f);
			this.GetComponent<CameraSwitch> ().ShowMainCamera ();
			spotlights.SetActive(false);
			*/
            //Destroy(menuSong);
        }
		UIText.text = "Round " + battleMaster.GetComponent<EndBattle>().round;
        battleMaster.GetComponent<SoundPlayer>().PlaySound("Round" + battleMaster.GetComponent<EndBattle>().round, true);
		yield return new WaitForSeconds(1f);
		battleStarted = true;
		battleSong.Play();
        ismusicplaying = true;
	}

	public bool checkifonbeat(){
		return true;
	}

	IEnumerator musicPlayingTrue(){
		yield return new WaitForSeconds(0.1f);
		ismusicplaying = true;
	}
}
