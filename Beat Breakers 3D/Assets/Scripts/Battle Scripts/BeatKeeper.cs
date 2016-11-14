using UnityEngine;
using System.Collections;
using InControl;

public class BeatKeeper : MonoBehaviour {

	private AudioClip testAudio;
	private float loopTime;
	private AudioSource audio;
	private bool onBeat;
	private bool beatHappened;
	private float nextBeatLog;
    public string rhythmRating;
	public GameObject player1;
	public GameObject player2;
	public GameObject gridModel;
	private GameObject battleMaster;
	//public GameObject boombox;
	private float bpm = 120f;
    private GameObject[] blocks;
	private GameObject[] evenSpaces;
	private GameObject[] oddSpaces;
	private Color gridColor1;
	private Color gridColor2;
	private Color gridColor3;

    private int everyOtherBeat = 1;
    public bool battleStarted = false;
	private int countdown;
	private int beatsLeft = 332;

    void Awake()
    {
        testAudio = Resources.Load<AudioClip>("cryptofthebeatbreakesFinal");
        onBeat = false;
        rhythmRating = "Good!";
        audio = GetComponent<AudioSource>();
		countdown = 3;
    }
	// Use this for initialization
	void Start () {
		battleMaster = GameObject.FindGameObjectWithTag ("BattleMaster");
        StartCoroutine(FirstClick());
        blocks = GameObject.FindGameObjectsWithTag("BeatBlocks");
		evenSpaces = GameObject.FindGameObjectsWithTag ("GridSpace2");
		oddSpaces = GameObject.FindGameObjectsWithTag ("GridSpace1");
		//gridColor1 = new Color (154f/255f, 149f/255f, 135f/255f,  1f); //new Color (0f, .6f, .6f, 1f); // new Color (0f, .90f, .90f, 1f);
		gridColor2 = new Color (196f/255f, 189f/255f, 172f/255f, 1f); //new Color (0f, .4f, .4f, 1f); //new Color (.196f, .189f, .172f, 1f);
		gridColor3 = new Color(64f/255f, 124f/255f, 183f/255f, 1f); //new Color (88f/255f, 153f/255f, 105f/255f, 1f);
        gridColor1 = new Color(88f / 255f, 153f / 255f, 105f / 255f, 1f);// Color.blue * Color.white; //- gridColor3;
        foreach (GameObject space in evenSpaces)
		{
			space.GetComponent<MeshRenderer> ().material.color = gridColor2;
		}
		foreach (GameObject space in oddSpaces)
		{
			space.GetComponent<MeshRenderer> ().material.color = gridColor3;
		}
		this.GetComponent<Timer> ().setText (beatsLeft.ToString ());
    }
	
	// Update is called once per frame
	void Update () {
        if (beatsLeft <= -1)
        {
            if (player1.GetComponent<VanillaCharacter>().health > player2.GetComponent<VanillaCharacter>().health)
            {
				battleMaster.GetComponent<EndBattle>().playerLoses(2);
            }
            else if (player2.GetComponent<VanillaCharacter>().health > player1.GetComponent<VanillaCharacter>().health)
            {
				battleMaster.GetComponent<EndBattle>().playerLoses(1);
            }
            else
            {
				battleMaster.GetComponent<EndBattle>().Tie();
            }
            
        }
	}

    void startgame()
    {
		battleStarted = true;
        audio.Play();
        float nextBeatSample = (float)AudioSettings.dspTime * testAudio.frequency;
        float loopTime = (30f / 1000f);
        float samplePeriod = (60f / bpm) * testAudio.frequency;
        //float nextBeatLog;
        StartCoroutine(ToBeat(samplePeriod, nextBeatSample));

    }

    public bool checkifonbeat()
    {
        return onBeat;
    }

	IEnumerator FirstClick()
    {
        while (!battleStarted)
        {
            if(Input.GetKeyDown("space") || Input.GetMouseButtonDown(0) || InputManager.ActiveDevice.AnyButtonWasPressed)
            {
                startgame();
                break;
            }
            yield return 0;
        }

    }
   
	IEnumerator ToBeat(float samplePeriod, float nextBeatSample)
	{
		while (audio.isPlaying)
		{
			float currentSample = (float)AudioSettings.dspTime * testAudio.frequency;
//			Debug.Log(currentSample + "current");
//			Debug.Log(nextBeatSample + "nextBeat");

            //Beat happens
			if (currentSample >= nextBeatSample ) {
				//GetComponent<Renderer>().material.color = Color.green;
				beatHappened = true; //beatHappened is directly when beat happens
				//gridModel.GetComponent<Renderer>().material.color = new Color(.85f, .85f, .85f, 1f);
				if (countdown > 0) {
					//start battle countdown
					this.GetComponent<StartCountdown> ().setText (countdown.ToString ());
					foreach (GameObject block in blocks) {
						block.GetComponent<BlockMover> ().BattleStart (4 - countdown);
					}
					countdown--;
				} else if (countdown == 0) {
					player1.GetComponent<VanillaCharacter> ().currentAction = "";
					player2.GetComponent<VanillaCharacter> ().currentAction = "";
					this.GetComponent<StartCountdown> ().setText ("dance!");
					foreach (GameObject block in blocks) {
						block.GetComponent<BlockMover> ().BattleStart (4 - countdown);
					}
					this.GetComponent<Timer> ().setText (beatsLeft.ToString ());
					beatsLeft--;
					countdown--;
				} else if (countdown == -1) {
					this.GetComponent<StartCountdown> ().setText ("");
					this.GetComponent<Timer> ().setText (beatsLeft.ToString ());
					beatsLeft--;
				}

                //recharge each player's meter by 1 every other beat
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
                    player1.GetComponent<VanillaCharacter>().meter++;
                    player2.GetComponent<VanillaCharacter>().meter++;
                    everyOtherBeat--;
                }
                //make boombox pulse to beat
                //boombox.GetComponent<BeatAnimation>().Pulse ();
//				player1.GetComponent<VanillaCharacter>().beatAnimation();
//				player2.GetComponent<VanillaCharacter>().beatAnimation();
                
				nextBeatSample += samplePeriod;
                
				nextBeatLog = nextBeatSample / testAudio.frequency;
                //yield return new WaitForSeconds(.05f); .15
                //Good  .025 <
                //Great .025 - .075  //great 0.125 - .15
                //Perfect .075 - .125 
                yield return new WaitForSeconds(.025f);
                rhythmRating = "Great!";
                yield return new WaitForSeconds(.024f);
                rhythmRating = "Good!";
				gridModel.GetComponent<Renderer>().material.color = Color.white;
                yield return new WaitForSeconds(.001f);
                ///This makes beat .05 after beat
			}

			//start of timing window
			if (currentSample >= nextBeatSample - (.1f * testAudio.frequency) ) {
                if (countdown < 0)
                {
                    onBeat = true;
                    rhythmRating = "Good!";
                    //onBeat is the timing window for player actions (slightly before + after when beat actually happens)
                }
			}

            if (currentSample >= nextBeatSample - (.075f * testAudio.frequency))
            {

                rhythmRating = "Great!";
            }
            if (currentSample >= nextBeatSample - (.025f * testAudio.frequency))
            {

                rhythmRating = "Perfect!";
            }

            //GetComponent<Renderer>().material.color = Color.red;
            if (beatHappened == true){
				beatHappened = false;
				onBeat = false;

                //if game has started, do current action for each player
                if (countdown < 0)
                {
					this.GetComponent<DoPlayerActions>().DoCurrentAction();
					player1.GetComponent<VanillaCharacter>().DoRhythmRating();
					player2.GetComponent<VanillaCharacter>().DoRhythmRating();
					player1.GetComponent<VanillaCharacter>().currentAction = "";
					player2.GetComponent<VanillaCharacter>().currentAction = "";
//                    player1.GetComponent<VanillaCharacter>().DoCurrentAction();
//                    player2.GetComponent<VanillaCharacter>().DoCurrentAction();
//                    player1.GetComponent<VanillaCharacter>().currentAction = "";
//                    player2.GetComponent<VanillaCharacter>().currentAction = "";
                }
                //Reset both players actions once the actionable period is over
                //(so they can move next beat)
                //player1.GetComponent<VanillaCharacter>().actionTaken = false;
				//player2.GetComponent<VanillaCharacter>().actionTaken = false;
			}
			yield return new WaitForSeconds(loopTime);
		}
	}

	public float getBPM () {
		return bpm;
	}
}