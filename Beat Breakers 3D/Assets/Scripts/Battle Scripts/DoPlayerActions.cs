using UnityEngine;
using System.Collections;

public class DoPlayerActions : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public GameObject model1;
    public GameObject battleMaster;
	private Animator animator1;
	public GameObject model2;
	private Animator animator2;
	private bool gameStarted = false;
	// Use this for initialization
	void Start () {
		battleMaster = GameObject.FindGameObjectWithTag ("BattleMaster");
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


        switch (player1action)
        {
            case "block":
                player1.GetComponent<VanillaCharacter>().block();
                break;
            case "flare":
                player1.GetComponent<VanillaCharacter>().playerForm = "flare";
                player1.GetComponent<VanillaCharacter>().formTimer = 0;
                Debug.Log("flare form happened for player 1");
                break;
            case "flow":
                player1.GetComponent<VanillaCharacter>().playerForm = "flow";
                player1.GetComponent<VanillaCharacter>().formTimer = 0;
                Debug.Log("flow form happened for player 1");
                break;
            case "foundation":
                player1.GetComponent<VanillaCharacter>().playerForm = "foundation";
                player1.GetComponent<VanillaCharacter>().formTimer = 0;
                Debug.Log("foundation form happened for player 1");
                break;
            default:
                break;
        }

        switch (player2action)
        {
            case "block":
                player2.GetComponent<VanillaCharacter>().block();
                break;
            case "flare":
                player2.GetComponent<VanillaCharacter>().playerForm = "flare";
                Debug.Log("flare form happened for player 2");
                player2.GetComponent<VanillaCharacter>().formTimer = 0;
                break;
            case "flow":
                player2.GetComponent<VanillaCharacter>().playerForm = "flow";
                player2.GetComponent<VanillaCharacter>().formTimer = 0;
                Debug.Log("flow happened for player 2");
                break;
            case "foundation":
                player2.GetComponent<VanillaCharacter>().playerForm = "foundation";
                player2.GetComponent<VanillaCharacter>().formTimer = 0;
                Debug.Log("Foundation happened for player 2");
                break;
            default:
                break;
        }

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
		switch (player1action)
		{
		case "headSlideUp":
			animator1.SetTrigger("headSlideAnim");
			player1.GetComponent<HeadSlide>().Attack("up");
            battleMaster.GetComponent<BattleStats>().HeadSlideP1 += 1;
			break;
		case "headSlideDown":
			animator1.SetTrigger("headSlideAnim");
			player1.GetComponent<HeadSlide>().Attack("down");
                battleMaster.GetComponent<BattleStats>().HeadSlideP1 += 1;
			break;
		case "headSlideLeft":
			animator1.SetTrigger("headSlideAnim");
			player1.GetComponent<HeadSlide>().Attack("left");
            battleMaster.GetComponent<BattleStats>().HeadSlideP1 += 1;
			break;
		case "headSlideRight":
			animator1.SetTrigger("headSlideAnim");
			player1.GetComponent<HeadSlide>().Attack("right");
            battleMaster.GetComponent<BattleStats>().HeadSlideP1 += 1;
			break;
		default:
			break;
		}
		switch (player2action)
		{
		case "headSlideUp":
			animator2.SetTrigger("headSlideAnim");
			player2.GetComponent<HeadSlide>().Attack("up");
            battleMaster.GetComponent<BattleStats>().HeadSlideP2 += 1;
			break;
		case "headSlideDown":
			animator2.SetTrigger("headSlideAnim");
			player2.GetComponent<HeadSlide>().Attack("down");
			battleMaster.GetComponent<BattleStats>().HeadSlideP2 += 1;
			break;
		case "headSlideLeft":
			animator2.SetTrigger("headSlideAnim");
			player2.GetComponent<HeadSlide>().Attack("left");
			battleMaster.GetComponent<BattleStats>().HeadSlideP2 += 1;
			break;
		case "headSlideRight":
			animator2.SetTrigger("headSlideAnim");
			player2.GetComponent<HeadSlide>().Attack("right");
			battleMaster.GetComponent<BattleStats>().HeadSlideP2 += 1;
			break;
		default:
			break;
		}

		switch (player1action)
		{
		case "sixStep":
			animator1.SetTrigger("sixStepAnim");
			player1.GetComponent<SixStep>().Attack();
            battleMaster.GetComponent<BattleStats>().SixStepP1 += 1;
			break;
		case "popNLock":
			animator1.SetTrigger("popNLockAnim");
			player1.GetComponent<PopNLock>().Attack();
            battleMaster.GetComponent<BattleStats>().PopLockP1 += 1;
			break;
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
		switch (player2action)
		{
		case "sixStep":
			animator2.SetTrigger("sixStepAnim");
			player2.GetComponent<SixStep>().Attack();
            battleMaster.GetComponent<BattleStats>().SixStepP2 += 1;
			break;
		case "popNLock":
			animator2.SetTrigger("popNLockAnim");
			player2.GetComponent<PopNLock>().Attack();
            battleMaster.GetComponent<BattleStats>().PopLockP2 += 1;
			break;
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
	IEnumerator backToIdleAnimation()
	{
		yield return new WaitForSeconds(.5f);
		animator1.SetBool("idleAnim", true);
		animator2.SetBool("idleAnim", true);
	}
}

