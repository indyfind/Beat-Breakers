using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VanillaCharacter : MonoBehaviour {

    private int health = 5;
    public string character;
    public GameObject grid;
    public GameObject enemy;
	private bool tripped = false;
	public string color;
	public Slider healthSlider;
	public bool actionTaken = false;
	private Vector3 scale;
	public Text playerWins;
    public string currentAction;

    // Use this for initialization
    void Start () {
		scale = gameObject.GetComponent<Transform>().localScale;
	}
	
	// Update is called once per frame
	void Update () {
		//update HUD to reflect current health
		healthSlider.value = health;
		if (health <= 0) {
			if (color == "red") {
				playerWins.text = "Player 2 Wins!";
			} else {
				playerWins.text = "Player 1 Wins!";
			}
			this.GetComponent<MeshRenderer>().enabled = false;
			StartCoroutine(End());
		}
    }
	
    public void TakeDamage(int dam)
    {
        health -= dam;
        //Debug.Log(character + "Took this much damage");
        //Debug.Log(character + " Has " + health + " health remaining");
    }
    public void DoCurrentAction()
    {
        // Move direction / HeadSlide  different directions/  sixstep /  PopLock

        switch (currentAction)
        {
            case "moveUp":
                this.GetComponent<CharacterMover>().MoveUp();
                break;
            case "moveDown":
                this.GetComponent<CharacterMover>().MoveDown();
                break;
            case "moveLeft":
                this.GetComponent<CharacterMover>().MoveLeft();
                break;
            case "moveRight":
                this.GetComponent<CharacterMover>().MoveRight();
                break;
        }
        currentAction = "";
    }
	public void Tripped(float time) 
	{
		tripped = true;
		//change color to black to show player is tripped
		//gameObject.GetComponent<MeshRenderer> ().color = Color.black;
		//use Coroutine to count down the time tripped
		StartCoroutine (TimeTripped (time));
	}

	IEnumerator TimeTripped(float time)
	{
		yield return new WaitForSeconds (time);
		tripped = false;
		//change color back to original
//		if (color == "red") {
//			gameObject.GetComponent<MeshRenderer> ().color = Color.red;
//		} else if (color == "white") {
//			gameObject.GetComponent<MeshRenderer> ().color = Color.white;
//		}
	}

	//Checks if player can move based on current effects (tripped, already moved, etc.)
	public bool canMove()
	{
		if (!tripped && !actionTaken) {
			return true;
		} else {
			return false;
		}
	}

	IEnumerator End()
	{
		yield return new WaitForSeconds (3f);
		Application.LoadLevel (0);
	}

//	public void beatAnimation()
//	{
//		this.transform.localScale = new Vector3 (scale.x / 2, scale.y / 2, scale.z / 2);
//	}
}

