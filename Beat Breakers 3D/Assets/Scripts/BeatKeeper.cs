using UnityEngine;
using System.Collections;

public class BeatKeeper : MonoBehaviour {

	private AudioClip testAudio;
	private float loopTime;
	private AudioSource audio;
	private bool onBeat;
	private bool beatHappened;
	//public GUIText scoreText;
	public int score;
	private float nextBeatLog;
	public GameObject player1;
	public GameObject player2;
	public GameObject boombox;
	private float bpm = 120f;
    
    private bool battleStarted = false;
	private int countdown;
    void Awake()
    {
        testAudio = Resources.Load<AudioClip>("cryptofthebeatbreakesFinal");
        score = 0;
        onBeat = false;
        audio = GetComponent<AudioSource>();
		countdown = 3;
    }
	// Use this for initialization
	void Start () {
        StartCoroutine(FirstClick());
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) && onBeat == true) {
			score += 1;
		//	Debug.Log (((float)AudioSettings.dspTime - nextBeatLog) + " ON");
		} else if ((Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) && onBeat == false) {
			score -= 1;
		//	Debug.Log (((float)AudioSettings.dspTime - nextBeatLog) + " OFF");
			//Debug.Log ( + "NEXT");
		}
		UpdateScore ();
	}

    void startgame()
    {
        audio.Play();
        float nextBeatSample = (float)AudioSettings.dspTime * testAudio.frequency;
        float loopTime = (30f / 1000f);
        float samplePeriod = (60f / bpm) * testAudio.frequency;
        //float nextBeatLog;
        StartCoroutine(ToBeat(samplePeriod, nextBeatSample));
        UpdateScore();

    }

    public bool checkifonbeat()
    {
        return onBeat;
    }

	IEnumerator FirstClick()
    {
        while (!battleStarted)
        {
            if(Input.GetKeyDown("space") || Input.GetMouseButtonDown(0) || Input.GetButtonDown("StartButton"))
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
			if (currentSample >= nextBeatSample ) {
				//GetComponent<Renderer>().material.color = Color.green;
				beatHappened = true; //beatHappened is directly when beat happens

				if (countdown > 0) {
					//start battle countdown
					this.GetComponent<StartCountdown> ().setText (countdown.ToString ());
				} else if (countdown == 0) {
                    player1.GetComponent<VanillaCharacter>().currentAction = "";
                    player2.GetComponent<VanillaCharacter>().currentAction = "";
                    this.GetComponent<StartCountdown> ().setText ("Dance!");
				} else if (countdown == -1) {
					this.GetComponent<StartCountdown> ().setText ("");
				}
				countdown--;

				//make boombox pulse to beat
				boombox.GetComponent<BeatAnimation>().Pulse ();
//				player1.GetComponent<VanillaCharacter>().beatAnimation();
//				player2.GetComponent<VanillaCharacter>().beatAnimation();
                
				nextBeatSample += samplePeriod;
                
				nextBeatLog = nextBeatSample / testAudio.frequency;
				//yield return new WaitForSeconds(.05f);
				yield return new WaitForSeconds(.05f); ///This makes beat .1 after beat
			}

			//if (currentSample >= nextBeatSample - (.05f * testAudio.frequency) ) {
			if (currentSample >= nextBeatSample - (.1f * testAudio.frequency) ) {
                if (countdown < 0)
                {
                    onBeat = true; //onBeat is the timing window for player actions (slightly before + after when beat actually happens)
                }
			}

			//GetComponent<Renderer>().material.color = Color.red;
			if (beatHappened == true){
				beatHappened = false;
				onBeat = false;
                if (countdown < 0)
                {
                    player1.GetComponent<VanillaCharacter>().DoCurrentAction();
                    player2.GetComponent<VanillaCharacter>().DoCurrentAction();
                    player1.GetComponent<VanillaCharacter>().currentAction = "";
                    player2.GetComponent<VanillaCharacter>().currentAction = "";
                }
                //Reset both players actions once the actionable period is over
                //(so they can move next beat)
                //player1.GetComponent<VanillaCharacter>().actionTaken = false;
				//player2.GetComponent<VanillaCharacter>().actionTaken = false;
			}
			yield return new WaitForSeconds(loopTime);
		}
	}

	void UpdateScore () {
		//scoreText.text = "Score: " + score;
	}

	public float getBPM () {
		return bpm;
	}
}