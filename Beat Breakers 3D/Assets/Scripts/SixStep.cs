using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class SixStep : MonoBehaviour
{
	private int damage;
    public GameObject grid;
    public GameObject enemy;
	public GameObject attackHitbox;
	public int player;

    //InControl input device
    private InputDevice device;

	public AudioSource soundEffect;

    //private int cooldown;
    //private bool onCoolDown = false;
    //private Text cooldownText;
    //public GameObject CooldownTimer;
    //private int cooldownCount;
    //private float bpm;
    //public GameObject HUDIcon;

    private int meterCost = 25;

    // Use this for initialization
    void Start()
    {
		//bpm = grid.GetComponent<BeatKeeper> ().getBPM ();
        damage = 10;
        //cooldown = 4;
		if (player == 1) {
            device = InputManager.Devices[0];
        } else if (player == 2) {
            device = InputManager.Devices[1];
        }
		//cooldownText = CooldownTimer.GetComponent<Text> ();
		//cooldownCount = cooldown * 2;
    }

    // Update is called once per frame
    void Update()
    {
        bool onb = grid.GetComponent<BeatKeeper>().checkifonbeat();
		bool canMove = GetComponent<VanillaCharacter> ().canMove ();

        if ((device.LeftBumper.WasPressed || device.LeftTrigger.WasPressed) && canMove && (this.GetComponent<VanillaCharacter>().meter >= meterCost)) // && onb && !onCoolDown
        {
            this.GetComponent<VanillaCharacter>().currentAction = "sixStep";
            this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper>().rhythmRating;
            //Attack();
            //StartCoroutine(CoolDown());
            //StartCoroutine(CoolDownDisplay());
            //GetComponent<VanillaCharacter>().actionTaken = true;
        }
    }

    public void Attack()
    {
		soundEffect.Play();
        // subtract meter cost
        this.GetComponent<VanillaCharacter>().meter -= meterCost;
        //get character positions
        Vector2 currentpos = GetComponent<CharacterMover>().getposition();
        Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();
        //turn attack hitbox on
        attackHitbox.SetActive(true);
        //check for hit
        if (enemypos.x == currentpos.x || enemypos.x == currentpos.x + 1 || enemypos.x == currentpos.x - 1)
        {

            if (enemypos.y == currentpos.y || enemypos.y == currentpos.y + 1 || enemypos.y == currentpos.y - 1)
            {
                enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
				enemy.GetComponent<VanillaCharacter>().Tripped(.5f);
            }

        }
        StartCoroutine(CoolDown());
        //StartCoroutine(CoolDownDisplay());
        //GetComponent<VanillaCharacter>().actionTaken = true;
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
