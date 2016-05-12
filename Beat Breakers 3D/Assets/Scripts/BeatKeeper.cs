using UnityEngine;
using System.Collections;

public class BeatKeeper : MonoBehaviour {

	private AudioClip testAudio;
	private float loopTime;
	private AudioSource audio;
	private bool onBeat;
	private bool greenSquare;
	//public GUIText scoreText;
	public int score;
	private float nextBeatLog;
	public GameObject player1;
	public GameObject player2;
	public GameObject boombox;
	private float bpm = 120f;
    
    private bool screentapped = false;
    void Awake()
    {
        testAudio = Resources.Load<AudioClip>("cryptofthebeatbreakesFinal");
        score = 0;
        onBeat = false;
        audio = GetComponent<AudioSource>();

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
        while (!screentapped)
        {
            if(Input.GetKeyDown("space") || Input.GetMouseButtonDown(0) || Input.GetButtonDown("StartButton1"))
            {
                startgame();
                break;
            }
            //print("Awaiting initial tap");
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
				greenSquare = true;

				boombox.GetComponent<BeatAnimation>().Pulse ();
//				player1.GetComponent<VanillaCharacter>().beatAnimation();
//				player2.GetComponent<VanillaCharacter>().beatAnimation();
                
				nextBeatSample += samplePeriod;
               
				nextBeatLog = nextBeatSample / testAudio.frequency;
				//yield return new WaitForSeconds(.05f);
				yield return new WaitForSeconds(.1f);
			}
			//if (currentSample >= nextBeatSample - (.05f * testAudio.frequency) ) {
			if (currentSample >= nextBeatSample - (.1f * testAudio.frequency) ) {
				onBeat = true;
			}
			//GetComponent<Renderer>().material.color = Color.red;
			if (greenSquare == true){
				greenSquare = false;
				onBeat = false;
				//Reset both players actions once the actionable period is over
				//(so they can move next beat)
				player1.GetComponent<VanillaCharacter>().actionTaken = false;
				player2.GetComponent<VanillaCharacter>().actionTaken = false;
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