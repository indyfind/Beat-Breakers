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
	private int damage;
    private int fallOffDamage = 200;
	//public KeyCode key;
	//private bool onCoolDown = false;
	public int player;
    private bool enemyfell = false;
    private bool playerfell = false;

    //InControl device
    private InputDevice device;

    //public string attacktype { get; set; }
    private Vector3 dest;
	private string joystickX;
	private string joystickY;

	//public AudioSource soundEffect;

	//private int cooldownCount;
	//private Text cooldownText;
	//public GameObject cooldownTimer;
	//private float bpm;
	//public GameObject HUDIcon;

    public int meterCost = 50;

	// Use this for initialization
	void Start()
	{
		//bpm = grid.GetComponent<BeatKeeper> ().getBPM ();
		damage = 100;
		//cooldown = 8;
		//Assign correct controller inputs based on which player it is
		if (player == 1) {
            device = InputManager.Devices[0];
        } else if (player == 2) {
            device = InputManager.Devices[1];
        }
		//cooldownText = cooldownTimer.GetComponent<Text> ();
		//cooldownCount = cooldown * 2;
	}
	
	// Update is called once per frame
	void Update()
	{
		bool onb = grid.GetComponent<BeatKeeper2>().checkifonbeat();
		bool canMove = GetComponent<VanillaCharacter> ().canMove ();
//        if (device.RightStick.WasPressed && canMove && this.GetComponent<VanillaCharacter>().meter >= meterCost) //&& !onCoolDown
//        {
//            //if right stick x value is greater than y value: check horizontal
//            if (Mathf.Abs(device.RightStickX.Value) >= Mathf.Abs(device.RightStickY.Value))
//            {
//                if (device.RightStickRight.WasPressed)
//                {
//                    this.GetComponent<VanillaCharacter>().currentAction = "headSlideRight";
//                    this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper2>().rhythmRating;
//                }
//                else if (device.RightStickLeft.WasPressed)
//                {
//                    this.GetComponent<VanillaCharacter>().currentAction = "headSlideLeft";
//                    this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper2>().rhythmRating;
//                }
//            }
//            //if not check vertical
//            else
//            {
//                if (device.RightStickUp.WasPressed)
//                {
//                    this.GetComponent<VanillaCharacter>().currentAction = "headSlideUp";
//                    this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper2>().rhythmRating;
//                }
//                else if (device.RightStickDown.WasPressed)
//                {
//                    this.GetComponent<VanillaCharacter>().currentAction = "headSlideDown";
//                    this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper2>().rhythmRating;
//                }
//            }
//        }
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
                playerfell = true;
                if (enemyfell)
                {
                    dest.y = 0;
                }
                else
                {
                    dest.y = 6;
                }
				
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
				
                playerfell = true;
                if (enemyfell)
                {
                    dest.x = 6;
                }
                else
                {
                    dest.x = 0;
                }
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
                playerfell = true;
                if (enemyfell)
                {
                    dest.x = 0;
                }
                else
                {
                    dest.x = 6;
                }
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
                playerfell = true;
                if (enemyfell)
                {
                    dest.y = 6;
                }
                else
                {
                    dest.y = 0;
                }
            }
		}
        if (playerfell)
        {
			//this.GetComponent<VanillaCharacter>().Tripped(1f);
            GetComponent<CharacterMover>().setposition((int)dest.x, (int)dest.y, 1f);
            this.GetComponent<VanillaCharacter>().TakeDamage(fallOffDamage);
            StartCoroutine(MakePlayerDisapear(playermodel));

        }
        else
        {
            GetComponent<CharacterMover>().setposition((int)dest.x, (int)dest.y, .5f);
            //return grid.GetComponent<GridMaster>().getPosition((int)dest.x, (int)dest.y);
        }
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
			enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
			if(destination.x < 0 || destination.x > 6|| destination.y > 6 || destination.y < 0){

                if(destination.x < 0)
                {
                    destination.x = 6;
                }
                else if(destination.x > 6)
                {
                    destination.x = 0;
                }
                else if(destination.y > 6)
                {
                    destination.y = 0;
                }
                else
                {
                    destination.y = 6;
                }
                enemy.GetComponent<CharacterMover>().setposition((int)destination.x, (int)destination.y, 1f);
                enemy.GetComponent<VanillaCharacter>().TakeDamage(fallOffDamage);
                enemyfell = true;
				StartCoroutine(MakePlayerDisapear(enemymodel));
            }
			else{
			
			enemy.GetComponent<CharacterMover>().setposition((int)destination.x, (int)destination.y, .5f);
			
			}
			//Push(key);
		}
		
	} 
	
	
	public void Attack(string direction)
	{
        //soundEffect.Play();
        this.GetComponent<SoundMaster>().PlaySound("headSlideSound");
        enemyfell = false;
        playerfell = false;
        //subtract meter cost
        this.GetComponent<VanillaCharacter>().meter -= meterCost;
        //show hitbox
        attackHitbox.SetActive(true);
        if (direction == "up") {
            this.transform.localEulerAngles = (new Vector3(0, -90, 0));
        } else if (direction == "down") {
            this.transform.localEulerAngles = (new Vector3(0, 90, 0));
        } else if (direction == "right") {
            this.transform.localEulerAngles = (new Vector3(0, 0, 0));
        } else if(direction == "left") {
            this.transform.localEulerAngles = (new Vector3(0, 180, 0));
        }

		Vector2 currentpos = GetComponent<CharacterMover>().getposition();
		Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();
		Vector2 temp = new Vector2(currentpos.x, currentpos.y);
        HitEnemy(direction, currentpos, enemypos);
        Slide(temp, direction);
        


        StartCoroutine(CoolDown());
		//StartCoroutine (CoolDownDisplay ());
		//StartCoroutine(MoveToPosition(.5f, destination));
		
	}
    IEnumerator MakePlayerDisapear(GameObject model)
    {
        if (enemyfell == true)
        {
			//enemy.GetComponent<VanillaCharacter>().Tripped(1f);
            enemyFallOffParticle.Play();
        }
        if (playerfell == true)
        {
			//this.GetComponent<VanillaCharacter>().Tripped(1f);
            playerFallOffParticle.Play();
        }
        model.SetActive(false);
        yield return new WaitForSeconds(1f);
        model.SetActive(true);
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
