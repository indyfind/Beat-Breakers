using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopNLock : MonoBehaviour {

	public int cooldown { get; set; }
	private int damage;
	public KeyCode key;
	public KeyCode key2;
	public string attacktype { get; set; }
	public GameObject grid;
	public GameObject enemy;
	private bool onCoolDown = false;
	public GameObject attackSpriteHorizontal;
	public GameObject attackSpriteVertical;
	public int player;
	private string rightBumper;
	private string rightTrigger;

	private int cooldownCount;
	private Text cooldownText;
	public GameObject playerHUD;
	private float bpm;
	public GameObject moveButton;

	// Use this for initialization
	void Start () {
		bpm = grid.GetComponent<BeatKeeper> ().getBPM ();
		attackSpriteHorizontal.GetComponent<SpriteRenderer> ().enabled = false;
		attackSpriteVertical.GetComponent<SpriteRenderer> ().enabled = false;
		damage = 2;
		this.cooldown = 4;
		if (player == 1) {
			rightBumper = "RightBumper1";
			rightTrigger = "RightTrigger1";
		} else if (player == 2) {
			rightBumper = "RightBumper2";
			rightTrigger = "RightTrigger2";
		}
		cooldownText = playerHUD.GetComponent<Text> ();
		cooldownCount = cooldown * 2;
	}
	
	// Update is called once per frame
	void Update () {
		bool onb = grid.GetComponent<BeatKeeper> ().checkifonbeat ();
		bool canMove = GetComponent<VanillaCharacter> ().canMove ();
		if ((Input.GetKeyDown (key) || Input.GetButtonDown (rightBumper)) && onb && !onCoolDown && canMove) {
			HorizontalAttack ();
			StartCoroutine (CoolDown ());
			StartCoroutine (CoolDownDisplay());
			GetComponent<VanillaCharacter> ().actionTaken = true;
		} else if ((Input.GetKeyDown (key2) || (Input.GetAxis (rightTrigger) != 0)) && onb && !onCoolDown && canMove) {
			VerticalAttack ();
			StartCoroutine (CoolDown ());
			StartCoroutine (CoolDownDisplay());
			GetComponent<VanillaCharacter> ().actionTaken = true;
		}
	}

	void HorizontalAttack()
	{
		attackSpriteHorizontal.GetComponent<SpriteRenderer> ().enabled = true;
		Vector2 currentpos = GetComponent<CharacterMover>().getposition();
		Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();	
		if ((enemypos.x <= currentpos.x + 2 && enemypos.x >= currentpos.x - 2) && enemypos.y == currentpos.y)
		{	
			enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
		}

	}

	void VerticalAttack()
	{
		attackSpriteVertical.GetComponent<SpriteRenderer> ().enabled = true;
		Vector2 currentpos = GetComponent<CharacterMover>().getposition();
		Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();
		if ((enemypos.y <= currentpos.y + 2 && enemypos.y >= currentpos.y - 2) && enemypos.x == currentpos.x)
		{	
			enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
		}
	}

	IEnumerator CoolDown()
	{
		onCoolDown = true;
		yield return new WaitForSeconds (.2f);
		attackSpriteHorizontal.GetComponent<SpriteRenderer> ().enabled = false;
		attackSpriteVertical.GetComponent<SpriteRenderer> ().enabled = false;
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
		moveButton.GetComponent<Image> ().color = Color.white;
		cooldownText.text = "";
		cooldownCount = cooldown * 2;
	}
}
