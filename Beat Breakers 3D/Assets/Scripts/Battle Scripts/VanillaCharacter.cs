using UnityEngine;
using UnityEngine.UI;
using InControl;
using System.Collections;

public class VanillaCharacter : MonoBehaviour {

	//InControl input device
	private InputDevice device;

    public int health = 100;
    public int meter;
    public string character;
    public string rhythmRating;
    public GameObject grid;
    public GameObject enemy;
	private GameObject battleMaster;
	private bool tripped = false;
    
	public ParticleSystem blood;
    public ParticleSystem rhythmParticlePerfect;
    public ParticleSystem rhythmParticleGreat;
    public ParticleSystem rhythmParticleGood;

	public GameObject ring1;
	public GameObject ring2;
	public GameObject ring3;
	public GameObject ring4;

    public int player;
	public Slider healthSlider;
    //public Slider meterSlider;
	//public bool actionTaken = false;
	private Vector3 scale;
    public Text rhythmRatingUI;
    public string currentAction;
    public Transform meterRadialSlider;
    private int currentcombo;

	private bool roundOver = false;

    //public AudioSource getHitSound;
    //public Text meterCharges;
    // Use this for initialization
    void Start () {
		if (player == 1) {
			device = InputManager.Devices[0];
		} else if (player == 2) {
			device = InputManager.Devices[1];
		}
		battleMaster = GameObject.FindGameObjectWithTag ("BattleMaster");
        meter = 25;
       // battleMaster.GetComponent<BattleStats>().ResetStats();
		//scale = gameObject.GetComponent<Transform>().localScale;
	}
    void Awake()
    {
       // battleMaster.GetComponent<BattleStats>().ResetStats();
    }
	
	// Update is called once per frame
	void Update () {
        //update HUD to reflect current health
        meter = 100;
		healthSlider.value = health;
        if (meter > 100) {
            meter = 100;
        }
		if (meter >= 25) {
			ring1.SetActive(true);
		} else {
			ring1.SetActive(false);
		}
		if (meter >= 50) {
			ring2.SetActive(true);
		} else {
			ring2.SetActive(false);
		}
		if (meter >= 75) {
			ring3.SetActive(true);
		} else {
			ring3.SetActive(false);
		}
		if (meter >= 100) {
			ring4.SetActive(true);
		} else {
			ring4.SetActive(false);
		}
        //meterSlider.value = meter;
		if (health <= 0 && !roundOver) {
			battleMaster.GetComponent<EndBattle>().playerLoses(player);
			roundOver = true;
			this.gameObject.SetActive (false);
		}
        meterRadialSlider.GetComponent<Image>().fillAmount = (float)meter / 100f;
        //meterCharges.text = (meter / 25).ToString();
    }
	
    public void TakeDamage(int dam)
    {
        int chance = (int)Random.Range(1f, 3f);
        if (chance == 1)
        {
            //getHitSound.Play();
            this.GetComponent<SoundMaster>().PlaySound("getHitSound");
        }
        health -= dam;
        meter += 2;
        enemy.GetComponent<VanillaCharacter>().meter += 3;
		blood.Play();
        //Debug.Log(character + "Took this much damage");
        //Debug.Log(character + " Has " + health + " health remaining");
    }

	public void ReadInput(string rating){
		if ((device.LeftBumper.WasPressed || device.LeftTrigger.WasPressed) && !tripped && (meter >= this.GetComponent<SixStep>().meterCost)) // && onb && !onCoolDown
		{
			currentAction = "sixStep";
			rhythmRating = rating;
		}
	}

	public void DoRhythmRating()
	{
		if (currentAction == "")
		{
			rhythmRating = "";
            if (currentcombo >= battleMaster.GetComponent<BattleStats>().MaxComboP1 && player == 1)
            {
                battleMaster.GetComponent<BattleStats>().MaxComboP1 = currentcombo;
            }
            if (currentcombo >= battleMaster.GetComponent<BattleStats>().MaxComboP2 && player == 2)
            {
                battleMaster.GetComponent<BattleStats>().MaxComboP2 = currentcombo;
            }
            currentcombo = 0;
        }

		//display rhythm rating
		rhythmRatingUI.text = rhythmRating;

		if (rhythmRating == "Good!")
		{
			rhythmParticleGood.Play();
            if (player == 1)
            {
                battleMaster.GetComponent<BattleStats>().GoodP1 += 1;
                if (currentcombo >= battleMaster.GetComponent<BattleStats>().MaxComboP1)
                {
                    battleMaster.GetComponent<BattleStats>().MaxComboP1 = currentcombo;
                }
            }
            if (player == 2)
            {
                battleMaster.GetComponent<BattleStats>().GoodP2 += 1;
                if (currentcombo >= battleMaster.GetComponent<BattleStats>().MaxComboP2)
                {
                    battleMaster.GetComponent<BattleStats>().MaxComboP2 = currentcombo;
                }
            }
            currentcombo = 0;

            rhythmParticleGood.Play();
			meter += 1;
		}

		if (rhythmRating == "Great!")
		{
			rhythmParticleGreat.Play();
			meter += 2;
            currentcombo += 1;
            if (player == 1)
            {
                battleMaster.GetComponent<BattleStats>().GreatsP1 += 1;
                if (currentcombo >= battleMaster.GetComponent<BattleStats>().MaxComboP1)
                {
                    battleMaster.GetComponent<BattleStats>().MaxComboP1 = currentcombo;
                }
            }
            if (player == 2)
            {
                battleMaster.GetComponent<BattleStats>().GreatsP2 += 1;
                if (currentcombo >= battleMaster.GetComponent<BattleStats>().MaxComboP2)
                {
                    battleMaster.GetComponent<BattleStats>().MaxComboP2 = currentcombo;
                }
            }
        }
		if (rhythmRating == "Perfect!")
		{
			rhythmParticlePerfect.Play();
			meter += 4;
            currentcombo += 1;
            if(player == 1)
            {
                battleMaster.GetComponent<BattleStats>().PerfectsP1 += 1;
                if (currentcombo >= battleMaster.GetComponent<BattleStats>().MaxComboP1)
                {
                    battleMaster.GetComponent<BattleStats>().MaxComboP1 = currentcombo;
                }
            }
            if (player == 2)
            {
                battleMaster.GetComponent<BattleStats>().PerfectsP2 += 1;
                if (currentcombo >= battleMaster.GetComponent<BattleStats>().MaxComboP2)
                {
                    battleMaster.GetComponent<BattleStats>().MaxComboP2 = currentcombo;
                }
            }
        }
		if (!roundOver){
			StartCoroutine(rhythmRatingDisplayOff());
		}
		
	}



    //called each beat to execute the action taken by the player
//    public void DoCurrentAction()
//    {
//        if (currentAction == "") {
//            rhythmRating = "";
//        }
//        //display rhythm rating
//        rhythmRatingUI.text = rhythmRating;
//
//        if (rhythmRating == "Good!")
//        {
//            rhythmParticleGood.Play();
//            meter += 1;
//        }
//        else if (rhythmRating == "Great!")
//        {
//            rhythmParticleGreat.Play();
//            meter += 2;
//        }
//        else if (rhythmRating == "Perfect!")
//        {
//            rhythmParticlePerfect.Play();
//            meter += 4;
//        }
//        //animator.SetBool("idleAnim", false);
//        switch (currentAction)
//        {
//            case "moveUp":
//                animator.SetTrigger("moveAnim");
//                this.GetComponent<CharacterMover>().MoveUp();
//                break;
//            case "moveDown":
//                animator.SetTrigger("moveAnim");
//                this.GetComponent<CharacterMover>().MoveDown();
//                break;
//            case "moveLeft":
//                animator.SetTrigger("moveAnim");
//                this.GetComponent<CharacterMover>().MoveLeft();
//                break;
//            case "moveRight":
//                animator.SetTrigger("moveAnim");
//                this.GetComponent<CharacterMover>().MoveRight();
//                break;
//            case "sixStep":
//                animator.SetTrigger("sixStepAnim");
//                this.GetComponent<SixStep>().Attack();
//                break;
//            case "popNLock":
//                animator.SetTrigger("popNLockAnim");
//                this.GetComponent<PopNLock>().Attack();
//                break;
//            case "headSlideUp":
//                animator.SetTrigger("headSlideAnim");
//                this.GetComponent<HeadSlide>().Attack("up");
//                break;
//            case "headSlideDown":
//                animator.SetTrigger("headSlideAnim");
//                this.GetComponent<HeadSlide>().Attack("down");
//                break;
//            case "headSlideLeft":
//                animator.SetTrigger("headSlideAnim");
//                this.GetComponent<HeadSlide>().Attack("left");
//                break;
//            case "headSlideRight":
//                animator.SetTrigger("headSlideAnim");
//                this.GetComponent<HeadSlide>().Attack("right");
//                break;
//            case "basicAttackLeft":
//                animator.SetTrigger("basicAttackAnim");
//                this.GetComponent<BasicAttack>().Attack("left");
//                break;
//            case "basicAttackRight":
//                animator.SetTrigger("basicAttackAnim");
//                this.GetComponent<BasicAttack>().Attack("right");
//                break;
//            case "basicAttackUp":
//                animator.SetTrigger("basicAttackAnim");
//                this.GetComponent<BasicAttack>().Attack("up");
//                break;
//            case "basicAttackDown":
//                animator.SetTrigger("basicAttackAnim");
//                this.GetComponent<BasicAttack>().Attack("down");
//                break;
//            default:
//                break;
//        }
//        //StartCoroutine(backToIdleAnimation());
//        StartCoroutine(rhythmRatingDisplayOff());
//    }
	public void Tripped(float time) 
	{
		tripped = true;
		//change color to black to show player is tripped
		//gameObject.GetComponent<MeshRenderer> ().color = Color.black;
		//use Coroutine to count down the time tripped
		if (!roundOver) {
			StartCoroutine (TimeTripped (time));
		}
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

