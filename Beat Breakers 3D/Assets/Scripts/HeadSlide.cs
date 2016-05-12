using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeadSlide : MonoBehaviour
{
	
	public int cooldown { get; set; }
	public int damage { get; set; }
	//public KeyCode key;
	public string attacktype { get; set; }
	public GameObject grid;
	public GameObject enemy;
	private bool onCoolDown = false;
	//public GameObject attackSprite;
	private Vector3 dest;
	public int player;
	private string direction;
	private string aButton;
	private string bButton;
	private string xButton;
	private string yButton;

	private int cooldownCount;
	private Text cooldownText;
	public GameObject playerHUD;
	private float bpm;
	public GameObject moveButton;

	public GameObject attackSpriteUp;
	public GameObject attackSpriteDown;
	public GameObject attackSpriteLeft;
	public GameObject attackSpriteRight;

	// Use this for initialization
	void Start()
	{
		bpm = grid.GetComponent<BeatKeeper> ().getBPM ();
		this.damage = 1;
		this.cooldown = 8;
		if (player == 1) {
			aButton = "A1";
			bButton = "B1";
			xButton = "X1";
			yButton = "Y1";
		} else if (player == 2) {
			aButton = "A2";
			bButton = "B2";
			xButton = "X2";
			yButton = "Y2";
		}
		cooldownText = playerHUD.GetComponent<Text> ();
		cooldownCount = cooldown * 2;
	}
	
	// Update is called once per frame
	void Update()
	{
		bool onb = grid.GetComponent<BeatKeeper>().checkifonbeat();
		bool canMove = GetComponent<VanillaCharacter> ().canMove ();
		if (Input.GetButtonDown(bButton) && onb && canMove && onCoolDown == false) {
			direction = "right";
			onCoolDown = true;
			attackSpriteRight.GetComponent<SpriteRenderer> ().enabled = true;
			Attack (direction);
			StartCoroutine (CoolDown ());
		} 
		if (Input.GetButtonDown(xButton) && onb && canMove && onCoolDown == false) {
			direction = "left";
			onCoolDown = true;
			attackSpriteLeft.GetComponent<SpriteRenderer> ().enabled = true;
			Attack (direction);
			StartCoroutine (CoolDown ());
		}
		if (Input.GetButtonDown(yButton) && onb && canMove && onCoolDown == false) {
			direction = "up";
			onCoolDown = true;
			attackSpriteUp.GetComponent<SpriteRenderer> ().enabled = true;
			Attack (direction);
			StartCoroutine (CoolDown ());
		}
		if (Input.GetButtonDown(aButton) && onb && canMove && onCoolDown == false) {
			direction = "down";
			onCoolDown = true;
			attackSpriteDown.GetComponent<SpriteRenderer> ().enabled = true;
			Attack (direction);
			StartCoroutine (CoolDown ());
		}
	}
	
	Vector3 Slide(Vector2 current, string dir)
	{
		Vector2 dest = new Vector2(current.x, current.y);
		if (dir == "up")
		{
			if (current.y - 2 >= 0)
			{
				dest.y -= 2;
			}
			else
			{
				dest.y = 0;
			}
		}
		else if (dir == "right")
		{
			if (current.x + 2 <= 6)
			{
				dest.x += 2;
			}
			else
			{
				dest.x = 6;
			}
		}
		else if (dir == "left")
		{
			if (current.x - 2 >= 0)
			{
				dest.x -= 2;
			}
			else
			{
				dest.x = 0;
			}
		}
		else
		{
			if (current.y + 2 <= 6)
			{
				dest.y += 2;
			}
			else
			{
				dest.y = 6;
			}
		}
		GetComponent<CharacterMover>().setposition((int)dest.x, (int)dest.y);
		return grid.GetComponent<GridMaster>().getPosition((int)dest.x, (int)dest.y);
	}
	

	void HitEnemy(string direction, Vector2 pos, Vector2 enemypos)
	{
		bool dam = false;
		Vector2 destination = new Vector2(enemypos.x, enemypos.y);
		if (direction == "down"){
			if(pos.x == enemypos.x && pos.y + 1 == enemypos.y)
			{
				dam = true;
				destination.y = pos.y + 4 ;
			}
			else if(pos.y + 2 == enemypos.y || pos.y + 3 == enemypos.y)
			{
				if(pos.x == enemypos.x || enemypos.x == pos.x + 1 || enemypos.x == pos.x - 1)
				{
					dam = true;
					if (pos.x == enemypos.x){
						//Debug.Log ("in front of me");
						destination.y = pos.y + 4 ;

					}
					else if(pos.x + 1 == enemypos.x){
						destination.x = enemypos.x + 1;
					}
					else if (pos.x - 1 == enemypos.x){
						destination.x = enemypos.x - 1;
					}
				}
			}
		}
		else if (direction == "right")
		{
			if (pos.x + 1 == enemypos.x && pos.y == enemypos.y)
			{
				dam = true;
				destination.x = pos.x + 4 ;
			}
			else if (pos.x + 2 == enemypos.x || pos.x + 3 == enemypos.y)
			{
				if (pos.y == enemypos.y || enemypos.y == pos.y + 1 || enemypos.y == pos.y - 1)
				{
					dam = true;
					if (pos.y == enemypos.y){
						//Debug.Log ("in front of me");
						destination.x = pos.x + 4 ;
						
					}
					else if(pos.y + 1 == enemypos.y){
						destination.y = enemypos.y + 1;
					}
					else if (pos.y - 1 == enemypos.y){
						destination.y = enemypos.y - 1;
					}
				}
			}
		} else if (direction == "left")
		{
			if (pos.x - 1 == enemypos.x && pos.y == enemypos.y)
			{
				dam = true;
				destination.x = pos.x - 4 ;
			}
			else if (pos.x - 2 == enemypos.x || pos.x - 3 == enemypos.x)
			{
				if (pos.y == enemypos.y || enemypos.y == pos.y + 1 || enemypos.y == pos.y - 1)
				{
					dam = true;
					if (pos.y == enemypos.y){
						//Debug.Log ("in front of me");
						destination.x = pos.x - 4 ;
						
					}
					else if(pos.y + 1 == enemypos.y){
						destination.y = enemypos.y + 1;
					}
					else if (pos.y - 1 == enemypos.y){
						destination.y = enemypos.y - 1;
					}
				}
			}
		} else // up
		{
			if (pos.x == enemypos.x && pos.y - 1 == enemypos.y)
			{
				dam = true;
				destination.y = pos.y - 4 ;
			}
			else if (pos.y - 2 == enemypos.y || pos.y - 3 == enemypos.y)
			{
				if (pos.x == enemypos.x || enemypos.x == pos.x + 1 || enemypos.x == pos.x - 1)
				{
					dam = true;
					if (pos.x == enemypos.x){
						//Debug.Log ("in front of me");
						destination.y = pos.y - 4 ;
						
					}
					else if(pos.x + 1 == enemypos.x){
						destination.x = enemypos.x + 1;
					}
					else if (pos.x - 1 == enemypos.x){
						destination.x = enemypos.x - 1;
					}
				}
			}
		}
		if (dam)
		{
			enemy.GetComponent<VanillaCharacter>().TakeDamage(1);
			if(destination.x < 0 || destination.x > 6|| destination.y > 6 || destination.y < 0){
				enemy.GetComponent<VanillaCharacter>().TakeDamage(1000);
			}
			else{
			Vector3 finaldestination = grid.GetComponent<GridMaster>().getPosition((int)destination.x, (int)destination.y);
			enemy.GetComponent<CharacterMover>().setposition((int)destination.x, (int)destination.y);
			enemy.GetComponent<CharacterMover>().moveenemy(.5f, finaldestination);
			}
			//Push(key);
		}
		
	} 
	
	
	void Attack(string direction)
	{
		Vector2 currentpos = GetComponent<CharacterMover>().getposition();
		Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();
		Vector2 temp = new Vector2(currentpos.x, currentpos.y);
		Vector3 destination = Slide(temp, direction);
		HitEnemy (direction, currentpos, enemypos);
		
		
		
		StartCoroutine(CoolDown());
		StartCoroutine (CoolDownDisplay ());
		StartCoroutine(MoveToPosition(.5f, destination));
		onCoolDown = true;
		
	}
	
	IEnumerator CoolDown()
	{
		yield return new WaitForSeconds (.2f);
		attackSpriteUp.GetComponent<SpriteRenderer> ().enabled = false;
		attackSpriteDown.GetComponent<SpriteRenderer> ().enabled = false;
		attackSpriteLeft.GetComponent<SpriteRenderer> ().enabled = false;
		attackSpriteRight.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds(cooldown - .2f);
		onCoolDown = false;
	}

	IEnumerator CoolDownDisplay()
	{
		moveButton.GetComponent<Image> ().color = Color.grey;
		while (cooldownCount > 0) {
			cooldownText.text = cooldownCount.ToString();
			yield return new WaitForSeconds (60f / bpm);
			cooldownCount -= 1;
		}
		cooldownText.text = "";
		cooldownCount = cooldown * 2;
		moveButton.GetComponent<Image> ().color = Color.white;
	}
	
	public IEnumerator MoveToPosition(float timeToMove , Vector3 destination )
	{
		Vector3 start = gameObject.transform.position;
		float t = 0f;
		while (t < 1)
		{
			t += Time.deltaTime / timeToMove;
			gameObject.transform.position = Vector3.Lerp(start, destination, t);
			yield return null;
		}
	}
}
