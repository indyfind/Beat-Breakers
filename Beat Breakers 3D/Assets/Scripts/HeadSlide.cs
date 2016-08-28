using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeadSlide : MonoBehaviour
{
	public GameObject grid;
	public GameObject enemy;
	public GameObject attackHitbox;
	private int cooldown;
	private int damage;
	//public KeyCode key;
	private bool onCoolDown = false;
	public int player;
	private string rightBumper;

	//public string attacktype { get; set; }
	private Vector3 dest;
	private string direction;
	private string joystickX;
	private string joystickY;

	private int cooldownCount;
	private Text cooldownText;
	public GameObject cooldownTimer;
	private float bpm;
	public GameObject HUDIcon;

	// Use this for initialization
	void Start()
	{
		bpm = grid.GetComponent<BeatKeeper> ().getBPM ();
		this.damage = 1;
		this.cooldown = 8;
		//Assign correct controller inputs based on which player it is
		if (player == 1) {
			joystickX = "RightJoystickX1";
			joystickY = "RightJoystickY1";
		} else {
			joystickX = "RightJoystickX2";
			joystickY = "RightJoystickY2";
		}
		cooldownText = cooldownTimer.GetComponent<Text> ();
		cooldownCount = cooldown * 2;
	}
	
	// Update is called once per frame
	void Update()
	{
		bool onb = grid.GetComponent<BeatKeeper>().checkifonbeat();
		bool canMove = GetComponent<VanillaCharacter> ().canMove ();
		if ((Input.GetAxisRaw (joystickX) > 0f) && onb && canMove && onCoolDown == false) {
			direction = "right";
			onCoolDown = true;
			this.transform.localEulerAngles = (new Vector3 (0, 0, 0));
			attackHitbox.GetComponent<MeshRenderer> ().enabled = true;
			Attack (direction);
			StartCoroutine (CoolDown ());
		} 
		if ((Input.GetAxisRaw (joystickX) < 0f) && onb && canMove && onCoolDown == false) {
			direction = "left";
			onCoolDown = true;
			this.transform.localEulerAngles = (new Vector3 (0, 180, 0));
			attackHitbox.GetComponent<MeshRenderer> ().enabled = true;
			Attack (direction);
			StartCoroutine (CoolDown ());
		}
		if ((Input.GetAxisRaw (joystickY) < 0f) && onb && canMove && onCoolDown == false) {
			direction = "up";
			onCoolDown = true;
			this.transform.localEulerAngles = (new Vector3 (0, -90, 0));
			attackHitbox.GetComponent<MeshRenderer> ().enabled = true;
			Attack (direction);
			StartCoroutine (CoolDown ());
		}
		if ((Input.GetAxisRaw (joystickY) > 0f) && onb && canMove && onCoolDown == false) {
			direction = "down";
			onCoolDown = true;
			this.transform.localEulerAngles = (new Vector3 (0, 90, 0));
			attackHitbox.GetComponent<MeshRenderer> ().enabled = true;
			Attack (direction);
			StartCoroutine (CoolDown ());
		}
	}
	
	void Slide(Vector2 current, string dir)
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
		GetComponent<CharacterMover>().setposition((int)dest.x, (int)dest.y, .5f);
		//return grid.GetComponent<GridMaster>().getPosition((int)dest.x, (int)dest.y);
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
			
			enemy.GetComponent<CharacterMover>().setposition((int)destination.x, (int)destination.y, .5f);
			
			}
			//Push(key);
		}
		
	} 
	
	
	void Attack(string direction)
	{
		Vector2 currentpos = GetComponent<CharacterMover>().getposition();
		Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();
		Vector2 temp = new Vector2(currentpos.x, currentpos.y);
		Slide(temp, direction);
		HitEnemy (direction, currentpos, enemypos);
		
		
		
		StartCoroutine(CoolDown());
		StartCoroutine (CoolDownDisplay ());
		//StartCoroutine(MoveToPosition(.5f, destination));

		onCoolDown = true;
		
	}
	
	IEnumerator CoolDown()
	{
		yield return new WaitForSeconds (.2f);
		attackHitbox.GetComponent<MeshRenderer> ().enabled = false;
		yield return new WaitForSeconds(cooldown - .2f);
		onCoolDown = false;
	}

	IEnumerator CoolDownDisplay()
	{
		HUDIcon.GetComponent<Image> ().color = Color.grey;
		while (cooldownCount > 0) {
			cooldownText.text = cooldownCount.ToString();
			yield return new WaitForSeconds (60f / bpm);
			cooldownCount -= 1;
		}
		cooldownText.text = "";
		cooldownCount = cooldown * 2;
		HUDIcon.GetComponent<Image> ().color = Color.white;
	}
	
	/*public IEnumerator MoveToPosition(float timeToMove , Vector3 destination )
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
    */
}
