using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class VanillaCharacter : MonoBehaviour {

    private int health = 10;
    public int meter;
    public string character;
    public string rhythmRating;
    public GameObject grid;
    public GameObject enemy;
	private bool tripped = false;
    
	public ParticleSystem blood;

    public int player;
	public Slider healthSlider;
    //public Slider meterSlider;
	//public bool actionTaken = false;
	private Vector3 scale;
	public Text playerWins;
    public Text rhythmRatingUI;
    public string currentAction;
    public Transform meterRadialSlider;

    // Use this for initialization
    void Start () {
        meter = 50;
		//scale = gameObject.GetComponent<Transform>().localScale;
	}
	
	// Update is called once per frame
	void Update () {
		//update HUD to reflect current health
		healthSlider.value = health;
        if (meter > 100) {
            meter = 100;
        }
        //meterSlider.value = meter;
		if (health <= 0) {
			if (player == 1) {
				playerWins.text = "player 2 wins!";
			} else {
				playerWins.text = "player 1 wins!";
			}
			StartCoroutine(End());
		}
        meterRadialSlider.GetComponent<Image>().fillAmount = (float)meter / 100f;
    }
	
    public void TakeDamage(int dam)
    {
        health -= dam;
        meter += 2;
        enemy.GetComponent<VanillaCharacter>().meter += 3;
		blood.Play();
        //Debug.Log(character + "Took this much damage");
        //Debug.Log(character + " Has " + health + " health remaining");
    }

    //called each beat to execute the action taken by the player
    public void DoCurrentAction()
    {
        if (currentAction == "") {
            rhythmRating = "";
        }

        //display rhythm rating
        rhythmRatingUI.text = rhythmRating;

        if (rhythmRating == "Good!")
        {
            meter += 1;
        }

        if (rhythmRating == "Great!")
        {
            meter += 2;
        }
        if (rhythmRating == "Perfect!")
        {
            meter += 4;
        }

        switch (currentAction)
        {
            case "moveUp":
                this.GetComponent<CharacterMover>().MoveUp();
                break;
            case "moveDown":
                this.GetComponent<CharacterMover>().MoveDown();
                break;
            case "moveLeft":
                this.GetComponent<CharacterMover>().MoveLeft();
                break;
            case "moveRight":
                this.GetComponent<CharacterMover>().MoveRight();
                break;
            case "sixStep":
                this.GetComponent<SixStep>().Attack();
                break;
            case "popNLock":
                this.GetComponent<PopNLock>().Attack();
                break;
            case "headSlideUp":
                this.GetComponent<HeadSlide>().Attack("up");
                break;
            case "headSlideDown":
                this.GetComponent<HeadSlide>().Attack("down");
                break;
            case "headSlideLeft":
                this.GetComponent<HeadSlide>().Attack("left");
                break;
            case "headSlideRight":
                this.GetComponent<HeadSlide>().Attack("right");
                break;
            case "basicAttackLeft":
                this.GetComponent<BasicAttack>().Attack("left");
                break;
            case "basicAttackRight":
                this.GetComponent<BasicAttack>().Attack("right");
                break;
            case "basicAttackUp":
                this.GetComponent<BasicAttack>().Attack("up");
                break;
            case "basicAttackDown":
                this.GetComponent<BasicAttack>().Attack("down");
                break;
            default:
                break;
        }
        StartCoroutine(rhythmRatingDisplayOff());
    }
	public void Tripped(float time) 
	{
		tripped = true;
		//change color to black to show player is tripped
		//gameObject.GetComponent<MeshRenderer> ().color = Color.black;
		//use Coroutine to count down the time tripped
		StartCoroutine (TimeTripped (time));
	}

	IEnumerator TimeTripped(float time)
	{
		yield return new WaitForSeconds (time);
		tripped = false;
		//change color back to original
//		if (color == "red") {
//			gameObject.GetComponent<MeshRenderer> ().color = Color.red;
//		} else if (color == "white") {
//			gameObject.GetComponent<MeshRenderer> ().color = Color.white;
//		}
	}

	//Checks if player can move based on current effects (tripped, already moved, etc.)
	public bool canMove()
	{
		if (!tripped) { //  && !actionTaken
			return true;
		} else {
			return false;
		}
	}

	IEnumerator End()
	{
		yield return new WaitForSeconds (3f);
		SceneManager.LoadScene (0);
	}

    IEnumerator rhythmRatingDisplayOff()
    {
        yield return new WaitForSeconds(.2f);
        rhythmRatingUI.text = "";
    }

//	public void beatAnimation()
//	{
//		this.transform.localScale = new Vector3 (scale.x / 2, scale.y / 2, scale.z / 2);
//	}
}

