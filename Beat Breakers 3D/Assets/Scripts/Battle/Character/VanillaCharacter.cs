using UnityEngine;
using UnityEngine.UI;
using InControl;
using System.Collections;

public class VanillaCharacter : MonoBehaviour {

	//world stuff
	private GameObject battleMaster;
	public GameObject grid;
	public GameObject enemy;
	public PlayerKeyboardActions currentPlayerActions;
	private bool keyboardControls;

	//player info
	public int player;
	public string character;
	public int health = 1000;
	public int meter = 0;
	private int meleeMeterCost;
	private int rangedMeterCost;

	//player state
	public bool tripped = false;
	public bool justTripped = false;
	public string currentAction;
	public string rhythmRating;
	private bool blocking;
	private int blockMeter;
	private int blockTimer;
	public string playerForm;
	public int formTimer;
	public bool onFire = false;

    //UI & visuals
    public GameObject model;
	private Image healthSlider;
	private Image chargeSlider;
	public GameObject blockVisual;
	public GameObject flareVisual;
	public GameObject flowVisual;
	public GameObject foundationVisual;
	private GameObject wins1;
	private GameObject wins2;
	//private GameObject blockMeter3;
	//private GameObject blockMeter4;
	public ParticleSystem blood;
	public GameObject orb;

	//InControl input device
	private InputDevice device;
    private GameObject inputMaster;
    
	//stat trackers
	private int roundWins;
	private int currentcombo;
	private bool roundOver = false;

	void Awake()
	{
        //find input master
        inputMaster = GameObject.FindGameObjectWithTag("InputMaster");
		keyboardControls = false;


        //set player number and tag based on world location
        if (this.transform.position.x == -3f)
        {
            player = 1;
            this.tag = "Player1";
        }
        else if (this.transform.position.x == 3f)
        {
            player = 2;
            this.tag = "Player2";
        }

        //set things based on player 1 or player 2
        if (player == 1)
        {

			if (inputMaster.GetComponent<InputMaster>().player1keyboard){
				currentPlayerActions = inputMaster.GetComponent<InputMaster>().getP1Actions();
				keyboardControls = true;
				device = new InputDevice();

			}
			//set input device
			else
			{
				device = inputMaster.GetComponent<InputMaster>().player1Controller;
				currentPlayerActions = new PlayerKeyboardActions();
			}

            //find scene objects
            //enemy = GameObject.FindGameObjectWithTag("Player2");
            healthSlider = GameObject.FindGameObjectWithTag("p1Health").GetComponent<Image>();
            chargeSlider = GameObject.FindGameObjectWithTag("p1Meter").GetComponent<Image>();
            wins1 = GameObject.FindGameObjectWithTag("p1BlockMeter1");
            wins2 = GameObject.FindGameObjectWithTag("p1BlockMeter2");
            //blockMeter3 = GameObject.FindGameObjectWithTag("p1BlockMeter3");
            //blockMeter4 = GameObject.FindGameObjectWithTag("p1BlockMeter4");

            //set starting position
            GetComponent<CharacterMover>().startingxPosition = 0;
            GetComponent<CharacterMover>().startingyPosition = 3;

            //set model tag
            //model.tag = "p1Model";

        }
        else if (player == 2)
        {
			if (inputMaster.GetComponent<InputMaster>().player2keyboard){
				currentPlayerActions = inputMaster.GetComponent<InputMaster>().getP2Actions();
				keyboardControls = true;
				device = new InputDevice();

			}
			//set input device
			else
			{
				device = inputMaster.GetComponent<InputMaster>().player2Controller;
				currentPlayerActions = new PlayerKeyboardActions();
			}

            //find scene objects
            //enemy = GameObject.FindGameObjectWithTag("Player1");
            healthSlider = GameObject.FindGameObjectWithTag("p2Health").GetComponent<Image>();
            chargeSlider = GameObject.FindGameObjectWithTag("p2Meter").GetComponent<Image>();
            wins1 = GameObject.FindGameObjectWithTag("p2BlockMeter1");
            wins2 = GameObject.FindGameObjectWithTag("p2BlockMeter2");
            //blockMeter3 = GameObject.FindGameObjectWithTag("p2BlockMeter3");
            //blockMeter4 = GameObject.FindGameObjectWithTag("p2BlockMeter4");

            //set starting position
            GetComponent<CharacterMover>().startingxPosition = 6;
            GetComponent<CharacterMover>().startingyPosition = 3;

            //set model tag
            //model.tag = "p2Model";

        }
    }

    // Use this for initialization
    void Start () {

        grid = GameObject.FindGameObjectWithTag("TheGrid");
        battleMaster = GameObject.FindGameObjectWithTag("BattleMaster");

        //find enemy
        if (player == 1)
        {
            enemy = GameObject.FindGameObjectWithTag("Player2");
			roundWins = battleMaster.GetComponent<EndBattle>().player1wins;
        } else if (player == 2)
        {
            enemy = GameObject.FindGameObjectWithTag("Player1");
			roundWins = battleMaster.GetComponent<EndBattle>().player2wins;
        }

        //set move costs
        if (character == "Eva") {
			//meleeMeterCost = GetComponent<SixStep>().meterCost;
			//rangedMeterCost = GetComponent<HeadSlide>().meterCost;
		} else if (character == "Naz") {
			//meleeMeterCost = GetComponent<Shimmy>().meterCost;
			//rangedMeterCost = GetComponent<AcidTrance>().meterCost;
		}

		//set form
        playerForm = "none";
        formTimer = 0;

		//set blocking
        blockMeter = 4;
        blockTimer = 0;
        blocking = false;

		//set round wins UI
		if (roundWins >= 1) {
			wins1.SetActive(true);
		} else {
			wins1.SetActive(false);
		}
		if (roundWins >= 2)
		{
			wins2.SetActive(true);
		} else {
			wins2.SetActive(false);
		}
	}
		
	// Update is called once per frame
	void Update () {

		//GOD MODE
		//meter = 100; 

        //update HUD to reflect current health/meter/round wins
		healthSlider.fillAmount = health/1000f;
        if (meter > 115) {
            meter = 115;
		} else if (meter < 0) {
			meter = 0;
		}
		chargeSlider.fillAmount = (float)meter / 100f;

		/*
		if (blockMeter >= 1) {
			blockMeter1.SetActive(true);
		} else {
			blockMeter1.SetActive(false);
		}
		if (blockMeter >= 2)
		{
			blockMeter2.SetActive(true);
		} else {
			blockMeter2.SetActive(false);
		}
		if (blockMeter >= 3)
		{
			blockMeter3.SetActive(true);
		} else {
			blockMeter3.SetActive(false);
		}
		if (blockMeter >= 4)
		{
			blockMeter4.SetActive(true);
		} else {
			blockMeter4.SetActive(false);
		}
		*/

		//check if on fire
		if (meter >= 100) {
			onFire = true;
			playerForm = "flare";
			flareVisual.SetActive(true);
		} else {
			onFire = false;
			playerForm = "none";
			flareVisual.SetActive(false);
		}

		//check for death
		if (health <= 0 && !roundOver) {
            battleMaster.GetComponent<EndBattle>().fadingsound = true;
            battleMaster.GetComponent<EndBattle>().playerLoses(player);
			roundOver = true;
			this.gameObject.SetActive (false);
		}
    }

	//Checks if player can move based on current effects (tripped, already moved, etc.)
	public bool canMove()
	{
		return !tripped;
	}

	//Take damage & handle forms, etc.
	public void TakeDamage(int dam, bool DOT = false, int knockbackDistance = 0)
    {
		//gain meter for getting hit
		meter += 2;

		//set enemyform
        string enemyform = enemy.GetComponent<VanillaCharacter>().playerForm;
        if (DOT) {
            enemyform = "flare";
			this.GetComponent<CharacterSound> ().PlaySound ("Burn");
        } else if (enemyform == "flare" && !blocking) {
            this.GetComponent<Flare>().StartFlareAttack();
			this.GetComponent<CharacterSound> ().PlaySound ("Burn");
        } else if (enemyform == "flow" && !(justTripped) && !blocking) {
            this.GetComponent<Flow>().TripPlayer();
			this.GetComponent<CharacterSound> ().PlaySound ("Stun");
        } else if (enemyform == "foundation") {
			knockbackDistance += 1;
			this.GetComponent<CharacterSound> ().PlaySound ("Bump");
        }

		//if not blocking, get knocked back
		if (!blocking) {
			this.GetComponent<Knockback>().GetKnockedBack(knockbackDistance);
		}

		//Debug.Log("player " + player + " had this much health  " + health );
        //Debug.Log("player " + player + " " + playerForm);

		//chance to play get hit sound
		int chance = (int)Random.Range(1f, 3f);
        if (chance == 1 && !blocking)
        {
            //getHitSound.Play();
            //this.GetComponent<CharacterSound>().PlaySound("getHit");
        }

		//if blocking, take no damage
        if (blocking)
        {
			//unless you have form weakness, then take some
            if (formWeakness(playerForm, enemyform)) {
                health -= (dam / 4);
            }
        }
 		else  //if not, take full damage
        {
			if (formWeakness(playerForm, enemyform)) // if weaker, take more damage
            {
                health -= (int)(dam * 1.5);
            }
			else if (formStrength(playerForm, enemyform)) // if stronger, take less damage
            {
                health -= (int)(dam * .75);
            }
			else // else, take normal damage
            {
                health -= dam;
            }

			//give opponent meter
            enemy.GetComponent<VanillaCharacter>().meter += 3;

			//play bleed animation
            blood.Play();

			//Chance to to play announcer line
			chance = (int)Random.Range (1f, 15f);
			if (health > 200 && chance == 1) {
				battleMaster.GetComponent<SoundPlayer> ().PlaySound ("AnyRound");
			}
		}

        //Debug.Log("player "+ player + " Has " + health + " health remaining");
    }

	//returns true if you are in weaker form
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
		else if (string.Equals(playerf, "none") && !string.Equals(enemyf, "none"))
		{
			return true;
		}
        else
        {
            return false;
        }

    }

	//returns true if you are in stronger form
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

	//keeps timer for how long form lasts
    public void checkFormTimer()
    {
        if (!(string.Equals(playerForm, "none")))
        {
            if (formTimer >= 12)
            {
                //Debug.Log("Form Has Been Reset");
				flareVisual.SetActive(false);
				flowVisual.SetActive(false);
				foundationVisual.SetActive(false);
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
  
	public void ReadInput(string rating, float beatTime){
		
		//Movement
		if ((device.DPad.WasPressed || device.LeftStick.WasPressed || keyboardControls)) { //  && onb
			Debug.Log(Time.time - beatTime);
			//if using left stick: is y value greater than x?
			if ((Mathf.Abs(device.LeftStickY.Value) >= Mathf.Abs(device.LeftStickX.Value)) || keyboardControls)
			{
				//move up
				if ((device.DPadUp.WasPressed || device.LeftStickUp.WasPressed || currentPlayerActions.Up) && this.GetComponent<CharacterMover>().yposition > 0)
				{
					this.GetComponent<VanillaCharacter>().currentAction = "moveUp";
					rhythmRating = rating;
				}
				//move down
				else if ((device.DPadDown.WasPressed || device.LeftStickDown.WasPressed || currentPlayerActions.Down) && this.GetComponent<CharacterMover>().yposition < 6)
				{
					this.GetComponent<VanillaCharacter>().currentAction = "moveDown";
					rhythmRating = rating;
				}
			}
			//if using left stick: is x value greater than y?
			if ((Mathf.Abs(device.LeftStickY.Value) <= Mathf.Abs(device.LeftStickX.Value))|| keyboardControls)
			{
				//move right
				if ((device.DPadRight.WasPressed || device.LeftStickRight.WasPressed || currentPlayerActions.Right) && this.GetComponent<CharacterMover>().xposition < 6)
				{
					this.GetComponent<VanillaCharacter>().currentAction = "moveRight";
					rhythmRating = rating;
				}
				//move left
				else if ((device.DPadLeft.WasPressed || device.LeftStickLeft.WasPressed || currentPlayerActions.Left) && this.GetComponent<CharacterMover>().xposition > 0)
				{
					this.GetComponent<VanillaCharacter>().currentAction = "moveLeft";
					rhythmRating = rating;
				}
			}
		}

		//Forms
		/*
		if (device.Action3.WasPressed) // && meter >= 25) // && onb // (Input.GetButtonDown(leftButton)) 
		{
			if (meter >= 0) {
				currentAction = "flow";
				rhythmRating = rating;
			} else {
				this.GetComponent<CharacterSound>().PlaySound("NotEnoughMeter");
			}
		}
		if (device.Action2.WasPressed) // && meter >= 25) // && onb
		{
			if (meter >= 0) {
				currentAction = "flare";
				rhythmRating = rating;
			} else {
				this.GetComponent<CharacterSound>().PlaySound("NotEnoughMeter");
			}
		}
		if (device.Action4.WasPressed) // && meter >= 25) // && onb
		{
			if (meter >= 0) {
				currentAction = "foundation";
				rhythmRating = rating;
			} else {
				this.GetComponent<CharacterSound>().PlaySound("NotEnoughMeter");
			}
		}
		*/

		//Blocking
		/*
		if ((device.Action1.WasPressed || device.Action1.IsPressed) && blockMeter >= 1) // && onb
        {
            currentAction = "block";
            rhythmRating = rating;
        }
        */

		//Melee attack
		if ((device.LeftBumper.WasPressed) || currentPlayerActions.ShortAtk) // || device.LeftTrigger.WasPressed)) // && (meter >= meleeMeterCost)) // && onb && !onCoolDown
		{
			if (true) { //meter >= meleeMeterCost) {
				currentAction = "melee";
				rhythmRating = rating;
			} else {
				this.GetComponent<CharacterSound>().PlaySound("NotEnoughMeter");
			}
		}

		//Ranged attack
		if ((device.RightBumper.WasPressed) || currentPlayerActions.LongAtk) // || device.RightTrigger.WasPressed)) // && (meter >= rangedMeterCost)) { // && !onCoolDown
		{
			if (true) { //meter >= rangedMeterCost) {
				currentAction = "ranged";
				rhythmRating = rating;
			} else {
				this.GetComponent<CharacterSound>().PlaySound("NotEnoughMeter");
			}
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
		else if (currentPlayerActions.BasicAttack)
		{
			if (this.transform.localEulerAngles.y == 270)
			{
				currentAction = "basicAttackUp";
				rhythmRating = rating;
			}
			else if (this.transform.localEulerAngles.y == 90)
			{
				currentAction = "basicAttackDown";
				rhythmRating = rating;
			}
			else if (this.transform.localEulerAngles.y == 0)
			{
				currentAction = "basicAttackRight";
				rhythmRating = rating;
			}
			else
			{ //(this.transform.localEulerAngles.y == 180) {
				currentAction = "basicAttackLeft";
				rhythmRating = rating;
			}
		}
	}

	public void DoRhythmRating()
	{
		//update combos
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
		if (health > 0)
		{
			this.GetComponent<RhythmRating>().DisplayRating();
		}
			
		//Track stats and increase meter
		if (rhythmRating == "Good!")
		{
			//add meter
			meter += 1;

			//track stats
			currentcombo = 0;
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
		}
		if (rhythmRating == "Great!")
		{
			//add meter
			meter += 2;

			//track stats
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
			//add meter
			meter += 4;

			//track stats
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
	}

}

