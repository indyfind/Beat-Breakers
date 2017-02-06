using UnityEngine;
using UnityEngine.UI;
using InControl;
using System.Collections;

public class VanillaCharacter : MonoBehaviour {

	//InControl input device
	private InputDevice device;

    public int health = 1000;
    public int meter;
    public string character;
    public string rhythmRating;
    public GameObject grid;
    public GameObject enemy;
    public string playerForm;
	private GameObject battleMaster;
    public bool tripped = false;
    public bool justTripped = false;
	public GameObject orb;
    public ParticleSystem blood;
    public ParticleSystem rhythmParticlePerfect;
    public ParticleSystem rhythmParticleGreat;
    public ParticleSystem rhythmParticleGood;

	public GameObject ring1;
	public GameObject ring2;
	public GameObject ring3;
	public GameObject ring4;

    public int player;
    private bool blocking;
    private int blockMeter;
    private int blockTimer;
    public int formTimer;
    public GameObject blockVisual;
    

	public Image healthSlider;
	public Image chargeSlider;
    public GameObject blockMeter1;
    public GameObject blockMeter2;
    public GameObject blockMeter3;
    public GameObject blockMeter4;
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
        playerForm = "none";
        formTimer = 0;
		if (player == 1) {
			device = InputManager.Devices[0];
		} else if (player == 2) {
			device = InputManager.Devices[1];
		}
		battleMaster = GameObject.FindGameObjectWithTag ("BattleMaster");
        blockMeter = 4;
        blockTimer = 0;
        blocking = false;
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
        //meter = 100; //GOD MODE
		healthSlider.fillAmount = health/1000f;
        if (meter > 100) {
            meter = 100;
        }
		/*
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
		*/
        //meterSlider.value = meter;
		if (health <= 0 && !roundOver) {
            battleMaster.GetComponent<EndBattle>().fadingsound = true; ;
            battleMaster.GetComponent<EndBattle>().playerLoses(player);
			roundOver = true;
			this.gameObject.SetActive (false);

		}
        chargeSlider.fillAmount = (float)meter / 100f;
        //meterCharges.text = (meter / 25).ToString();
        if (blockMeter >= 1)
        {
            blockMeter1.SetActive(true);
        } else
        {
            blockMeter1.SetActive(false);
        }
        if (blockMeter >= 2)
        {
            blockMeter2.SetActive(true);
        }
        else
        {
            blockMeter2.SetActive(false);
        }
        if (blockMeter >= 3)
        {
            blockMeter3.SetActive(true);
        }
        else
        {
            blockMeter3.SetActive(false);
        }
        if (blockMeter >= 4)
        {
            blockMeter4.SetActive(true);
        }
        else
        {
            blockMeter4.SetActive(false);
        }
    }

	public void TakeDamage(int dam, bool DOT = false, int knockbackDistance = 0)
    {
        string enemyform = enemy.GetComponent<VanillaCharacter>().playerForm;
        if (DOT)
        {
            enemyform = "flare";
        }
        else if (enemyform == "flare" && !blocking)
        {
            this.GetComponent<Flare>().StartFlareAttack();
        }
        else if (enemyform == "flow" && !(justTripped) && !blocking)
        {
            this.GetComponent<Flow>().TripPlayer();
        }
        if (enemyform == "foundation")
        {
			knockbackDistance += 1;
        }
		if (!blocking) {
			this.GetComponent<Knockback>().GetKnockedBack(knockbackDistance);
		}
		//Debug.Log("player " + player + " had this much health  " + health );
        //Debug.Log("player " + player + " " + playerForm);
		int chance = (int)Random.Range(1f, 3f);
        if (chance == 1 && !blocking)
        {
            //getHitSound.Play();
            this.GetComponent<SoundMaster>().PlaySound("getHitSound");
        }
        if (blocking)
        {
            if (formWeakness(playerForm, enemyform)) {
                health -= (dam / 4);
            }
        }
        else
        {
            if (formWeakness(playerForm, enemyform))
            {
                health -= (int)(dam * 1.5);
            }
            else if (formStrength(playerForm, enemyform))
            {
                health -= (int)(dam * .75);
            }
            else if (string.Equals(playerForm, "none") && !string.Equals(enemyform, "none")){
                health -= (int)(dam * 1.5);
            }
            else
            {
                health -= dam;
            }
            enemy.GetComponent<VanillaCharacter>().meter += 3;
            blood.Play();
        }
		meter += 2;
        //Debug.Log("player "+ player + " Has " + health + " health remaining");
    }


    private bool formWeakness(string playerf, string enemyf)
    {
        if (string.Equals(playerf, "flow") && string.Equals(enemyf, "foundation"))
        {
            return true;
        }
        else if (string.Equals(playerf, "foundation") && string.Equals(enemyf, "flare"))
        {
            return true;
        }
        else if (string.Equals(playerf, "flare") && string.Equals(enemyf, "flow"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool formStrength(string playerf, string enemyf)
    {
        if (string.Equals(playerf, "flow") && string.Equals(enemyf, "flare"))
        {
            return true;
        }
        else if (string.Equals(playerf, "foundation") && string.Equals(enemyf, "flow"))
        {
            return true;
        }
        else if (string.Equals(playerf, "flare") && string.Equals(enemyf, "foundation"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void checkFormTimer()
    {
        if (!(string.Equals(playerForm, "none")))
        {
            if (formTimer >= 12)
            {
                Debug.Log("Form Has Been Reset");
				orb.GetComponent<MeshRenderer>().material.color = Color.white;
                playerForm = "none";
                formTimer = 0;
            }
            else
            {
                formTimer += 1;
            }
        }
    }

    public void block()
    {
            blocking = true;
            blockMeter -= 1;
            blockVisual.SetActive(true);
        
    }
    public void resetblocking()
    {
        blockVisual.SetActive(false);
        blocking = false;
        if (blockTimer >= 4)
        {
            blockTimer = 0;
            blockMeter += 1;
        }
        if (blockMeter < 4)
        {
            blockTimer += 1;
        }
    }
  

	public void ReadInput(string rating){
		//Movement
		if ((device.DPad.WasPressed || device.LeftStick.WasPressed)) { //  && onb
			//if using left stick: is y value greater than x?
			if (Mathf.Abs(device.LeftStickY.Value) >= Mathf.Abs(device.LeftStickX.Value))
			{
				//move up
				if ((device.DPadUp.WasPressed || device.LeftStickUp.WasPressed) && this.GetComponent<CharacterMover>().yposition > 0)
				{
					this.GetComponent<VanillaCharacter>().currentAction = "moveUp";
					rhythmRating = rating;
				}
				//move down
				else if ((device.DPadDown.WasPressed || device.LeftStickDown.WasPressed) && this.GetComponent<CharacterMover>().yposition < 6)
				{
					this.GetComponent<VanillaCharacter>().currentAction = "moveDown";
					rhythmRating = rating;
				}
			}
			//if using left stick: is x value greater than y?
			if (Mathf.Abs(device.LeftStickY.Value) <= Mathf.Abs(device.LeftStickX.Value))
			{
				//move right
				if ((device.DPadRight.WasPressed || device.LeftStickRight.WasPressed) && this.GetComponent<CharacterMover>().xposition < 6)
				{
					this.GetComponent<VanillaCharacter>().currentAction = "moveRight";
					rhythmRating = rating;
				}
				//move left
				else if ((device.DPadLeft.WasPressed || device.LeftStickLeft.WasPressed) && this.GetComponent<CharacterMover>().xposition > 0)
				{
					this.GetComponent<VanillaCharacter>().currentAction = "moveLeft";
					rhythmRating = rating;
				}
			}
		}
		//Forms
		if (device.Action3.WasPressed && meter >= 25) // && onb // (Input.GetButtonDown(leftButton)) 
		{
			currentAction = "flow";
			rhythmRating = rating;
		}
		if (device.Action2.WasPressed && meter >= 25) // && onb
		{
			currentAction = "flare";
			rhythmRating = rating;
		}
		if (device.Action4.WasPressed && meter >= 25) // && onb
		{
			currentAction = "foundation";
			rhythmRating = rating;
		}
		//Blocking
		if ((device.Action1.WasPressed || device.Action1.IsPressed) && blockMeter >= 1) // && onb
        {
            currentAction = "block";
            rhythmRating = rating;
        }
		//Six Step
		if ((device.LeftBumper.WasPressed || device.LeftTrigger.WasPressed) && (meter >= this.GetComponent<SixStep>().meterCost)) // && onb && !onCoolDown
		{
			currentAction = "sixStep";
			rhythmRating = rating;
		}
		//Head Slide
		if ((device.RightBumper.WasPressed || device.RightTrigger.WasPressed) && (meter >= this.GetComponent<HeadSlide>().meterCost)) { // && !onCoolDown
			currentAction = "headSlide";
			rhythmRating = rating;
		}
		//Basic Attack
		if (device.RightStick.WasPressed) //&& !onCoolDown
		{
			//if right stick x value is greater than y value: check horizontal
			if (Mathf.Abs(device.RightStickX.Value) >= Mathf.Abs(device.RightStickY.Value))
			{
				if (device.RightStickRight.WasPressed)
				{
					currentAction = "basicAttackRight";
					rhythmRating = rating;
				}
				else if (device.RightStickLeft.WasPressed)
				{
					currentAction = "basicAttackLeft";
					rhythmRating = rating;
				}
			}
			//if not check vertical
			else
			{
				if (device.RightStickUp.WasPressed)
				{
					currentAction = "basicAttackUp";
					rhythmRating = rating;
				}
				else if (device.RightStickDown.WasPressed)
				{
					currentAction = "basicAttackDown";
					rhythmRating = rating;
				}
			}
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
        //rhythmRatingUI.text = rhythmRating;
        if (health > 0)
        {
            this.GetComponent<RhythmRating>().DisplayRating();
        }

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
			//StartCoroutine(rhythmRatingDisplayOff());
		}
		
	}

	//Checks if player can move based on current effects (tripped, already moved, etc.)
	public bool canMove()
	{
        return !tripped;
	}

    IEnumerator rhythmRatingDisplayOff()
    {
        yield return new WaitForSeconds(.2f);
        rhythmRatingUI.text = "";
	}
		
}

