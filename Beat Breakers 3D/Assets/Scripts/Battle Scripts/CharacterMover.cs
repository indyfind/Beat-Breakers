using UnityEngine;
using System.Collections;

public class CharacterMover : MonoBehaviour {
    public int startingxPosition;
    public int startingyPosition;
    public int xposition = 0;
    public int yposition = 0;
	private int destinationx;
	private int destinationy;
	private Vector3 destination;
    //private float moverange = 1;
	public GameObject playerModel;
	public ParticleSystem fallOffParticle;
	private GameObject grid;
	private GameObject enemy;
    private bool pushed;
	private bool fellOff = false;
	private int fallOffDamage = 200;

    // Use this for initialization
    void Start () {
        //take scene objects from VanillaCharacter
        grid = this.GetComponent<VanillaCharacter>().grid;
        enemy = this.GetComponent<VanillaCharacter>().enemy;

        xposition = startingxPosition;
        yposition = startingyPosition;
		destinationx = startingxPosition;
		destinationy = startingyPosition;
	}
	
	// Update is called once per frame
	void Update () {
//        Debug.Log(player + "  destinationx = " + destinationx );
//        Debug.Log(player + "  destinationy = " + destinationy);
//        Debug.Log(player + " xposition =" + xposition);
//        Debug.Log(player + " yposition  =" + yposition);
    }
    public void MoveUp()
    {
        this.transform.localEulerAngles = (new Vector3(0, -90, 0));
        StartCoroutine(MoveDirection(.2f, "up"));
    }
    public void MoveDown()
    {
        this.transform.localEulerAngles = (new Vector3(0, 90, 0));
        StartCoroutine(MoveDirection(.2f, "down"));
    }
    public void MoveLeft()
    {
        this.transform.localEulerAngles = (new Vector3(0, 180, 0));
        StartCoroutine(MoveDirection(.2f, "left"));
    }
    public void MoveRight()
    {
        this.transform.localEulerAngles = (new Vector3(0, 0, 0));
        StartCoroutine(MoveDirection(.2f, "right"));
    }

    public Vector2 getposition()
    {
		return new Vector2((float)destinationx, (float)destinationy);
    }

    public void setposition(int x, int y)
    {
		float timeToMove = .5f;
		fellOff = false;
		// If player knocked off, set them on opposite side
		if (x > 6) {
			fellOff = true;
			xposition = 0;
		} else if (x < 0) {
			fellOff = true;
			xposition = 6;
		} else { 
			xposition = x;
		}
		if (y > 6) {
			fellOff = true;
			yposition = 0;
		} else if (y < 0) {
			fellOff = true;
			yposition = 6;
		} else {
			yposition = y;
		}
		if (fellOff) {
			timeToMove = 1f;
		}
		destination = new Vector3(grid.GetComponent<GridMaster>().getPosition(xposition, yposition).x, 
			1f, 
			grid.GetComponent<GridMaster>().getPosition(xposition, yposition).y);
		StartCoroutine(MoveToPosition(timeToMove));
		if (fellOff) {
			this.GetComponent<VanillaCharacter>().health -= fallOffDamage;
			StartCoroutine(FallOff());
		}
    }

	public IEnumerator MoveToPosition(float timeToMove)
	{
		pushed = true;
		Vector3 currentPos = gameObject.transform.position;
		float t = 0f;
		while (t < 1)
		{
			t += Time.deltaTime / timeToMove;
			transform.position = Vector3.Lerp(currentPos, destination, t);
			yield return null;
		}
		destinationx = xposition;
		destinationy = yposition;
		pushed = false;
	}
    
	public IEnumerator MoveDirection(float timeToMove, string direction)
	{
		switch (direction)
		{
			case "up":
				destinationx = xposition;
                if (yposition - 1 < 0)
                {
                    destinationy = yposition;
                }
                else
                {
                    destinationy = yposition - 1;
                }
				break;
			case "down":
				destinationx = xposition;
                if (yposition + 1 > 6)
                {
                    destinationy = yposition;
                }
                else
                {
                    destinationy = yposition + 1;
                }
				break;
			case "left":
				destinationy = yposition;
                if (xposition - 1 < 0)
                {
                    destinationx = xposition;
                }
                else
                {
                    destinationx = xposition - 1;
                }
				break;
			case "right":
				destinationy = yposition;
                if (xposition + 1 > 6)
                {
                    destinationx = xposition;
                }
                else
                {
                    destinationx = xposition + 1;
                }
				break;
			default:
				break;
		}
		destination = new Vector3(grid.GetComponent<GridMaster>().getPosition(destinationx, destinationy).x, 1f, grid.GetComponent<GridMaster>().getPosition(destinationx, destinationy).y);
		Vector3 currentPos = gameObject.transform.position;
		bool sameSpace = false;
		float t = 0f;
		// move halfway to destination
        while (t <= 0.5) {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, destination, t);
			//this.transform.localEulerAngles = new Vector3 (0, 30, 0);
            yield return null;
        }
		// if trying to enter same space as enemy player, send player back to original position
		// by reversing current position and destination
		if (destinationx == enemy.GetComponent<CharacterMover>().destinationx && destinationy == enemy.GetComponent<CharacterMover>().destinationy) {
			sameSpace = true; // (both players trying to enter same space)
			destination = currentPos;
			currentPos = gameObject.transform.position;
		}
		// move rest of way to destination
		while (t >= 0.5 && t < 1) {
			t += Time.deltaTime / timeToMove;
			transform.position = Vector3.Lerp(currentPos, destination, t);
			//this.transform.localEulerAngles = new Vector3 (0, 30, 0);
			yield return null;
		}
		if (sameSpace == false) {
			if (pushed != true) {
				xposition = destinationx;
				yposition = destinationy;
			}
		} else {
			destinationx = xposition;
			destinationy = yposition;
		}

    }

	IEnumerator FallOff()
	{
		fallOffParticle.Play();
		playerModel.SetActive(false);
		yield return new WaitForSeconds(1f);
		playerModel.SetActive(true);
	}
}
