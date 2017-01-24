using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class HeadSlide : MonoBehaviour
{
	public GameObject grid;
	public GameObject enemy;
	public GameObject attackHitbox;
    public GameObject playermodel;
    public GameObject enemymodel;
    public ParticleSystem playerFallOffParticle;
    public ParticleSystem enemyFallOffParticle;
	//private int cooldown;
	private int damage = 100;
    private int fallOffDamage = 200;
	//public KeyCode key;
	//private bool onCoolDown = false;
	public int player;
    private bool enemyfell = false;
	private bool playerfell = false;
    //public string attacktype { get; set; }
    private Vector3 dest;
	private string joystickX;
	private string joystickY;

    public int meterCost = 25;

	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		bool onb = grid.GetComponent<BeatKeeper2>().checkifonbeat();
		bool canMove = GetComponent<VanillaCharacter> ().canMove ();
	}
	
	void Slide (string dir)
	{
		if (dir == "up") {
			this.GetComponent<CharacterMover>().MoveUp();
		} else if (dir == "down") {
			this.GetComponent<CharacterMover>().MoveDown();
		} else if (dir == "right") {
			this.GetComponent<CharacterMover>().MoveRight();
		} else { //(this.transform.localEulerAngles.y == 180) {
			this.GetComponent<CharacterMover>().MoveLeft();
		}
	}
	

	void HitEnemy(string direction, Vector2 pos, Vector2 enemypos)
	{
		bool dam = false; //whether or not attack hits
		Vector2 enemyDest = new Vector2(enemypos.x, enemypos.y);
		switch (direction) {
		case "up":
			//If enemy is in same x column
			if (enemypos.x == pos.x) {
				//and 3 or less spaces above, attack hits
				if (enemypos.y < pos.y && enemypos.y >= pos.y - 3) {
					dam = true;
					enemyDest.y -= 1;
				}
			//If enemy is 1 to the left or 1 to the right of player x column
			} else if (enemypos.x >= pos.x - 1 && enemypos.x <= pos.x + 1) {
				//and 2 or 3 spaces above, attack hits
				if (enemypos.y <= pos.y - 2 && enemypos.y >= pos.y -3) {
					dam = true;
					enemyDest.y -= 1;
				}
			}
			break;
		case "down":
			//If enemy is in same x column
			if (enemypos.x == pos.x) {
				//and 3 or less spaces below, attack hits
				if (enemypos.y > pos.y && enemypos.y <= pos.y + 3) {
					dam = true;
					enemyDest.y += 1;
				}
				//If enemy is 1 to the left or 1 to the right of player x column
			} else if (enemypos.x >= pos.x - 1 && enemypos.x <= pos.x + 1) {
				//and 2 or 3 spaces below, attack hits
				if (enemypos.y >= pos.y + 2 && enemypos.y <= pos.y + 3) {
					dam = true;
					enemyDest.y += 1;
				}
			}
			break;
		case "right":
			//If enemy is in same y column
			if (enemypos.y == pos.y) {
				//and 3 or less spaces to the right, attack hits
				if (enemypos.x > pos.x && enemypos.x <= pos.x + 3) {
					dam = true;
					enemyDest.x += 1;
				}
				//If enemy is 1 above or 1 below of player y column
			} else if (enemypos.y >= pos.y - 1 && enemypos.y <= pos.y + 1) {
				//and 2 or 3 spaces to the right, attack hits
				if (enemypos.x >= pos.x + 2 && enemypos.x <= pos.x + 3) {
					dam = true;
					enemyDest.x += 1;
				}
			}
			break;
		case "left":
			//If enemy is in same y column
			if (enemypos.y == pos.y) {
				//and 3 or less spaces to the left, attack hits
				if (enemypos.x < pos.x && enemypos.x >= pos.x - 3) {
					dam = true;
					enemyDest.x -= 1;
				}
				//If enemy is 1 above or 1 below of player y column
			} else if (enemypos.y >= pos.y - 1 && enemypos.y <= pos.y + 1) {
				//and 2 or 3 spaces to the left, attack hits
				if (enemypos.x <= pos.x - 2 && enemypos.x >= pos.x - 3) {
					dam = true;
					enemyDest.x -= 1;
				}
			}
			break;
		default:
			break;
		}
		if (dam)
		{
			enemy.GetComponent<VanillaCharacter>().TakeDamage(damage); //, false, 1);
			//enemy.GetComponent<CharacterMover>().setposition((int)enemyDest.x, (int)enemyDest.y);
		}
	} 
	
	
	public void Attack()
	{
        this.GetComponent<SoundMaster>().PlaySound("headSlideSound");
		string direction;
        //subtract meter cost
        this.GetComponent<VanillaCharacter>().meter -= meterCost;
        //show hitbox
        attackHitbox.SetActive(true);
		//check direction
		if (this.transform.localEulerAngles.y == 270) {
			direction = "up";
		} else if (this.transform.localEulerAngles.y == 90) {
			direction = "down";
		} else if (this.transform.localEulerAngles.y == 0) {
			direction = "right";
		} else { //(this.transform.localEulerAngles.y == 180) {
			direction = "left";
        }
		Vector2 currentpos = GetComponent<CharacterMover>().getposition();
		Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();
		//Vector2 temp = new Vector2(currentpos.x, currentpos.y);
        HitEnemy(direction, currentpos, enemypos);
        Slide(direction);
        StartCoroutine(CoolDown());
	}

    IEnumerator CoolDown()
	{
        //onCoolDown = true;
        yield return new WaitForSeconds (.4f);
		attackHitbox.SetActive (false);
        //reset current action to nothing to prevent accidental double headSlide
        this.GetComponent<VanillaCharacter>().currentAction = "";
		//attackHitbox.GetComponent<MeshRenderer> ().enabled = false;
		//yield return new WaitForSeconds(cooldown - .2f);
		//onCoolDown = false;
	}

    /*
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
    */
}
