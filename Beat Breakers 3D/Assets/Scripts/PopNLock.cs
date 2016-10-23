using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class PopNLock : MonoBehaviour {
	
	public GameObject grid;
	public GameObject enemy;
	public GameObject attackHitbox;
	//private int cooldown;
	private int damage;
	public KeyCode key;
	//private bool onCoolDown = false;
	public int player;

    //InControl device
    private InputDevice device;

	public AudioSource soundEffect;

    //private int cooldownCount;
    //private Text cooldownText;
    //public GameObject cooldownTimer;
	//public GameObject HUDIcon;

    private int meterCost = 25;

	// Use this for initialization
	void Start () {
		damage = 2;
		//cooldown = 4;
		if (player == 1) {
            device = InputManager.Devices[0];
        } else if (player == 2) {
            device = InputManager.Devices[1];
        }
		//cooldownText = cooldownTimer.GetComponent<Text> ();
		//cooldownCount = cooldown * 2;
    }
	
	// Update is called once per frame
	void Update () {
		bool onb = grid.GetComponent<BeatKeeper> ().checkifonbeat ();
		bool canMove = GetComponent<VanillaCharacter> ().canMove ();
		if (device.RightBumper.WasPressed && canMove && (this.GetComponent<VanillaCharacter>().meter >= meterCost)) { // && !onCoolDown
            this.GetComponent<VanillaCharacter>().currentAction = "popNLock";
            this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper>().rhythmRating;
        }
	}
    public void Attack()
    {
		soundEffect.Play();
        //subtract meter cost
        this.GetComponent<VanillaCharacter>().meter -= meterCost;
        //Debug.Log(this.transform.localEulerAngles.y);
        if (((Mathf.Round(this.transform.localEulerAngles.y)) == 90) || (Mathf.Round(this.transform.localEulerAngles.y) == 270)) {
            //Debug.Log ("horizontal");
            HorizontalAttack();
        } else {
            //Debug.Log ("vertical");
            VerticalAttack();
        }
        StartCoroutine(CoolDown());
        //StartCoroutine(CoolDownDisplay());
        //GetComponent<VanillaCharacter>().actionTaken = true;
    }
    void HorizontalAttack()
	{
		attackHitbox.SetActive(true);
		Vector2 currentpos = GetComponent<CharacterMover>().getposition();
		Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();	
		if ((enemypos.x <= currentpos.x + 2 && enemypos.x >= currentpos.x - 2) && enemypos.y == currentpos.y)
		{	
			enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
		}

	}

	void VerticalAttack()
	{
        attackHitbox.SetActive(true);
        Vector2 currentpos = GetComponent<CharacterMover>().getposition();
		Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();
		if ((enemypos.y <= currentpos.y + 2 && enemypos.y >= currentpos.y - 2) && enemypos.x == currentpos.x)
		{	
			enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
		}
	}

	IEnumerator CoolDown()
	{
		//onCoolDown = true;
		yield return new WaitForSeconds (.4f);
        attackHitbox.SetActive(false);
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
		HUDIcon.GetComponent<Image> ().color = Color.white;
		cooldownText.text = "";
		cooldownCount = cooldown * 2;
	}
    */
}
