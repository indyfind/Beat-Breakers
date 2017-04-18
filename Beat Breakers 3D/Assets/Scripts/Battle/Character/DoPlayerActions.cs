using UnityEngine;
using System.Collections;

public class DoPlayerActions : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public GameObject model1;
    public GameObject model2;
    public GameObject battleMaster;
	private Animator animator1;
	private Animator animator2;
	private bool gameStarted = false;
	private string char1;
	private string char2;

	// Use this for initialization
	void Start () {

        //find scene objects
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
		battleMaster = GameObject.FindGameObjectWithTag ("BattleMaster");

        //see which characters are chosen
		char1 = player1.GetComponent<VanillaCharacter>().character;
		char2 = player2.GetComponent<VanillaCharacter>().character;

        //find model
        model1 = player1.GetComponent<VanillaCharacter>().model;
        model2 = player2.GetComponent<VanillaCharacter>().model;
		animator1 = model1.GetComponent<Animator>();
		animator2 = model2.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (gameStarted == false) {
			if (this.GetComponent<BeatKeeper2>().battleStarted == true) {
				gameStarted = true;
				animator1.SetBool("gameStart", true);
				animator2.SetBool("gameStart", true);
			}
		}
	}

	public void DoCurrentAction()
	{
		string player1action = player1.GetComponent<VanillaCharacter>().currentAction;
		string player2action = player2.GetComponent<VanillaCharacter>().currentAction;

        player1.GetComponent<VanillaCharacter>().resetblocking();
        player2.GetComponent<VanillaCharacter>().resetblocking();
        player1.GetComponent<VanillaCharacter>().checkFormTimer();
        player2.GetComponent<VanillaCharacter>().checkFormTimer();

        player1.GetComponent<Flare>().FlareAttack();
        player2.GetComponent<Flare>().FlareAttack();

        player1.GetComponent<Flow>().UnTripPlayer();
        player2.GetComponent<Flow>().UnTripPlayer();

        bool player1canMove = player1.GetComponent<VanillaCharacter>().canMove();
        bool player2canMove = player2.GetComponent<VanillaCharacter>().canMove();
        if (player1canMove)
        {
            switch (player1action)
            {
                case "block":
                    player1.GetComponent<VanillaCharacter>().block();
                    break;
                case "flare":
                    player1.GetComponent<VanillaCharacter>().playerForm = "flare";
					player1.GetComponent<CharacterSound>().PlaySound("FormSwitch");
                    player1.GetComponent<VanillaCharacter>().formTimer = 0;
					player1.GetComponent<VanillaCharacter>().meter -= 25;
					player1.GetComponent<VanillaCharacter>().flareVisual.SetActive(true);
					player1.GetComponent<VanillaCharacter>().flowVisual.SetActive(false);
					player1.GetComponent<VanillaCharacter>().foundationVisual.SetActive(false);
                    //player1.GetComponent<Flare>().StartFlareAttack();
                    //Debug.Log("flare form happened for player 1");
                    break;
                case "flow":
                    player1.GetComponent<VanillaCharacter>().playerForm = "flow";
					player1.GetComponent<CharacterSound>().PlaySound("FormSwitch");
                    player1.GetComponent<VanillaCharacter>().formTimer = 0;
					player1.GetComponent<VanillaCharacter>().meter -= 25;
					player1.GetComponent<VanillaCharacter>().flowVisual.SetActive(true);
					player1.GetComponent<VanillaCharacter>().flareVisual.SetActive(false);
					player1.GetComponent<VanillaCharacter>().foundationVisual.SetActive(false);
                    //Debug.Log("flow form happened for player 1");
                    break;
                case "foundation":
                    player1.GetComponent<VanillaCharacter>().playerForm = "foundation";
					player1.GetComponent<CharacterSound>().PlaySound("FormSwitch");
                    player1.GetComponent<VanillaCharacter>().formTimer = 0;
					player1.GetComponent<VanillaCharacter>().meter -= 25;
					player1.GetComponent<VanillaCharacter>().foundationVisual.SetActive(true);
					player1.GetComponent<VanillaCharacter>().flareVisual.SetActive(false);
					player1.GetComponent<VanillaCharacter>().flowVisual.SetActive(false);
                    //Debug.Log("foundation form happened for player 1");
                    break;
                default:
                    break;
            }
        }
        if (player2canMove)
        {
            switch (player2action)
            {
                case "block":
                    player2.GetComponent<VanillaCharacter>().block();
                    break;
                case "flare":
                    player2.GetComponent<VanillaCharacter>().playerForm = "flare";
					player2.GetComponent<CharacterSound>().PlaySound("FormSwitch");
                    //Debug.Log("flare form happened for player 2");
                    player2.GetComponent<VanillaCharacter>().formTimer = 0;
					player2.GetComponent<VanillaCharacter>().meter -= 25;
					player2.GetComponent<VanillaCharacter>().flareVisual.SetActive(true);
					player2.GetComponent<VanillaCharacter>().flowVisual.SetActive(false);
					player2.GetComponent<VanillaCharacter>().foundationVisual.SetActive(false);
                    //player1.GetComponent<Flare>().StartFlareAttack();
                    break;
                case "flow":
                    player2.GetComponent<VanillaCharacter>().playerForm = "flow";
					player2.GetComponent<CharacterSound>().PlaySound("FormSwitch");
                    player2.GetComponent<VanillaCharacter>().formTimer = 0;
                    //Debug.Log("flow happened for player 2");
					player2.GetComponent<VanillaCharacter>().meter -= 25;
					player2.GetComponent<VanillaCharacter>().flowVisual.SetActive(true);
					player2.GetComponent<VanillaCharacter>().flareVisual.SetActive(false);
					player2.GetComponent<VanillaCharacter>().foundationVisual.SetActive(false);
                    break;
                case "foundation":
                    player2.GetComponent<VanillaCharacter>().playerForm = "foundation";
					player2.GetComponent<CharacterSound>().PlaySound("FormSwitch");
                    player2.GetComponent<VanillaCharacter>().formTimer = 0;
					player2.GetComponent<VanillaCharacter>().meter -= 25;
					player2.GetComponent<VanillaCharacter>().foundationVisual.SetActive(true);
					player2.GetComponent<VanillaCharacter>().flareVisual.SetActive(false);
					player2.GetComponent<VanillaCharacter>().flowVisual.SetActive(false);
                    //Debug.Log("Foundation happened for player 2");
                    break;
                default:
                    break;
            }
        }
        if (player1canMove)
        {
            switch (player1action)
            {
                case "moveUp":
                    animator1.SetTrigger("moveAnim");
                    player1.GetComponent<CharacterMover>().MoveUp();
                    break;
                case "moveDown":
                    animator1.SetTrigger("moveAnim");
                    player1.GetComponent<CharacterMover>().MoveDown();
                    break;
                case "moveLeft":
                    animator1.SetTrigger("moveAnim");
                    player1.GetComponent<CharacterMover>().MoveLeft();
                    break;
                case "moveRight":
                    animator1.SetTrigger("moveAnim");
                    player1.GetComponent<CharacterMover>().MoveRight();
                    break;
                default:
                    break;
            }
        }
        if (player2canMove)
        {
            switch (player2action)
            {
                case "moveUp":
                    animator2.SetTrigger("moveAnim");
                    player2.GetComponent<CharacterMover>().MoveUp();
                    break;
                case "moveDown":
                    animator2.SetTrigger("moveAnim");
                    player2.GetComponent<CharacterMover>().MoveDown();
                    break;
                case "moveLeft":
                    animator2.SetTrigger("moveAnim");
                    player2.GetComponent<CharacterMover>().MoveLeft();
                    break;
                case "moveRight":
                    animator2.SetTrigger("moveAnim");
                    player2.GetComponent<CharacterMover>().MoveRight();
                    break;
                default:
                    break;
            }
        }
        if (player1canMove)
        {
            switch (player1action)
            {
				case "ranged":
					animator1.SetTrigger("rangedAnim");
					if (char1 == "Eva") {
						player1.GetComponent<HeadSlide>().Attack();
					} else if (char1 == "Naz") {
						player1.GetComponent<AcidTrance>().Attack();
					}
					battleMaster.GetComponent<BattleStats>().RangedAttackP1 += 1;
					break;
                default:
                    break;
            }
        }
        if (player2canMove)
        {
            switch (player2action)
            {
				case "ranged":
					animator2.SetTrigger("rangedAnim");
					if (char2 == "Eva") {
						player2.GetComponent<HeadSlide>().Attack();
					} else if (char2 == "Naz") {
						player2.GetComponent<AcidTrance>().Attack();
					}
					battleMaster.GetComponent<BattleStats>().RangedAttackP2 += 1;
					break;	
                default:
                    break;
            }
        }
        if (player1canMove)
        {
            switch (player1action)
            {
                case "melee":
                    animator1.SetTrigger("meleeAnim");
					if (char1 == "Eva") {
						player1.GetComponent<SixStep>().Attack();
					} else if (char1 == "Naz") {
						player1.GetComponent<Shimmy>().Attack();
					} else if (char1 == "CosmicS") {
						player1.GetComponent<Xshape>().Attack();
					}
					battleMaster.GetComponent<BattleStats>().MeleeAttackP1 += 1;
                    break;
//                case "popNLock":
//                    animator1.SetTrigger("popNLockAnim");
//                    player1.GetComponent<PopNLock>().Attack();
//                    battleMaster.GetComponent<BattleStats>().PopLockP1 += 1;
//                    break;
                case "basicAttackLeft":
                    animator1.SetTrigger("basicAttackAnim");
                    player1.GetComponent<BasicAttack>().Attack("left");
                    break;
                case "basicAttackRight":
                    animator1.SetTrigger("basicAttackAnim");
                    player1.GetComponent<BasicAttack>().Attack("right");
                    break;
                case "basicAttackUp":
                    animator1.SetTrigger("basicAttackAnim");
                    player1.GetComponent<BasicAttack>().Attack("up");
                    break;
                case "basicAttackDown":
                    animator1.SetTrigger("basicAttackAnim");
                    player1.GetComponent<BasicAttack>().Attack("down");
                    break;
                default:
                    break;
            }
        }
        if (player2canMove)
        {
            switch (player2action)
            {
                case "melee":
                    animator2.SetTrigger("meleeAnim");
					if (char2 == "Eva") {
						player2.GetComponent<SixStep>().Attack();
					} else if (char2 == "Naz") {
						player2.GetComponent<Shimmy>().Attack();
					} else if (char1 == "CosmicS") {
						player1.GetComponent<Xshape>().Attack();
					}
					battleMaster.GetComponent<BattleStats>().MeleeAttackP2 += 1;
                    break;
//                case "popNLock":
//                    animator2.SetTrigger("popNLockAnim");
//                    player2.GetComponent<PopNLock>().Attack();
//                    battleMaster.GetComponent<BattleStats>().PopLockP2 += 1;
//                    break;
                case "basicAttackLeft":
                    animator2.SetTrigger("basicAttackAnim");
                    player2.GetComponent<BasicAttack>().Attack("left");
                    break;
                case "basicAttackRight":
                    animator2.SetTrigger("basicAttackAnim");
                    player2.GetComponent<BasicAttack>().Attack("right");
                    break;
                case "basicAttackUp":
                    animator2.SetTrigger("basicAttackAnim");
                    player2.GetComponent<BasicAttack>().Attack("up");
                    break;
                case "basicAttackDown":
                    animator2.SetTrigger("basicAttackAnim");
                    player2.GetComponent<BasicAttack>().Attack("down");
                    break;
                default:
                    break;
            }
        }

	}

    public void pausecharacteranimations()
    {
        animator1.speed = 0;
        animator2.speed = 0;
    }

    public void unpausecharacteranimations()
    {
        animator1.speed = 1;
        animator2.speed = 1;
    }
    IEnumerator backToIdleAnimation()
	{
		yield return new WaitForSeconds(.5f);
		animator1.SetBool("idleAnim", true);
		animator2.SetBool("idleAnim", true);
	}
}

