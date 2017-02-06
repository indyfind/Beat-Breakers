using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndBattle : MonoBehaviour {
    //public GameObject player1;
    //public GameObject player2;
	private Text UIText;
	private int player1wins = 0;
	private int player2wins = 0;
    public float audiovolume;
	public int round = 1;
    public bool fadingsound;
    private GameObject sound;
    bool sceneloaded;
   

    // Use this for initialization
    void Awake() {
        fadingsound = false;
        audiovolume = 1;
        sceneloaded = false;
        sound = GameObject.FindGameObjectWithTag("TheGrid");
    }
	
	// Update is called once per frame
	void Update () {
		
        if (fadingsound)
        {
            Debug.Log("fading out");
            fadeOut();
        }
        if (sceneloaded)
        {
            if (sound == null)
            {
                sound = GameObject.FindGameObjectWithTag("TheGrid");
                audiovolume = 1;
                //sound.GetComponent<AudioSource>().volume = audiovolume;
                fadingsound = false;
                sceneloaded = false;
            }
        }
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
		UIText = GameObject.FindGameObjectWithTag ("MainText").GetComponent<Text>();
		UIText.text = "Draw!";
        StartCoroutine(End(1));
    }
    public void fadeOut()
    {
        if (audiovolume > 0)
        {
            if (sound != null)
           {
                audiovolume -= 0.25f * Time.deltaTime;
                sound.GetComponent<AudioSource>().volume = audiovolume;
            }
        }
        else
        {
            if (sound != null)
            {
                sound.GetComponent<AudioSource>().volume = 0;
            }
        }

    }
    IEnumerator End(int sceneToLoad)
    {
		UIText = GameObject.FindGameObjectWithTag ("MainText").GetComponent<Text>();
		round += 1;
		Debug.Log(round);
		UIText.text = "Round Over!";
        this.GetComponent<SoundPlayer>().PlaySound("RoundOver");
       

        
        yield return new WaitForSeconds(5f);

        
        SceneManager.LoadScene(sceneToLoad);
        sceneloaded = true;
        if (round == 2)
        {
            //Debug.Log("We got here");
            this.GetComponent<SoundPlayer>().PlaySound("Round2Sound");
        } else if (round == 3)
        {
            this.GetComponent<SoundPlayer>().PlaySound("Round3Sound");
        }
        
        //SceneManager.LoadScene(0);
    }
}
