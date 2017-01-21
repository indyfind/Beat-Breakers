using UnityEngine;
using System.Collections;
using InControl;

public class CharacterMover : MonoBehaviour {
    public int startingxPosition;
    public int startingyPosition;
    public int xposition = 0;
    public int yposition = 0;
	private int destinationx;
	private int destinationy;
    private float moverange = 1;
    public GameObject grid;
    private Vector3 destination;
    private bool pushed;
	public int player; // player 1 or player 2

	public GameObject enemy;

    //InControl device
    private InputDevice device;

    // Use this for initialization
    void Start () {
        xposition = startingxPosition;
        yposition = startingyPosition;
		destinationx = startingxPosition;
		destinationy = startingyPosition;
		if (player == 1) {
            device = InputManager.Devices[0];
        } else if (player == 2) {
            device = InputManager.Devices[1];
        }
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
        //yposition -= 1;
        this.transform.localEulerAngles = (new Vector3(0, -90, 0));
        StartCoroutine(MoveDirection(.2f, "up"));
        //GetComponent<VanillaCharacter>().actionTaken = true; //action has been taken, so no more moves/attacks for this beat
    }
    public void MoveDown()
    {
        //yposition += 1;
        this.transform.localEulerAngles = (new Vector3(0, 90, 0));
        StartCoroutine(MoveDirection(.2f, "down"));
        //GetComponent<VanillaCharacter>().actionTaken = true;
    }
    public void MoveLeft()
    {
        //xposition -= 1;
        this.transform.localEulerAngles = (new Vector3(0, 180, 0));
        StartCoroutine(MoveDirection(.2f, "left"));
        //GetComponent<VanillaCharacter>().actionTaken = true;
    }
    public void MoveRight()
    {
        //xposition += 1;
        this.transform.localEulerAngles = (new Vector3(0, 0, 0));
        StartCoroutine(MoveDirection(.2f, "right"));
        //GetComponent<VanillaCharacter>().actionTaken = true;
    }

    public Vector2 getposition()
    {
		return new Vector2((float)destinationx, (float)destinationy);
    }

    public void setposition(int x, int y, float timetomove)
    {
        xposition = x;
        yposition = y;
		destination = new Vector3(grid.GetComponent<GridMaster>().getPosition(xposition, yposition).x, 1f, grid.GetComponent<GridMaster>().getPosition(xposition, yposition).y);
        StartCoroutine(MoveToPosition(timetomove));
    }
    //public void startMovement(float timetomove)
    //{
       // StartCoroutine(MoveToPosition(timetomove));
    //} 
    //public void moveenemy(float t ,Vector3 pos)
    //{
      //  destination = pos;
        //StartCoroutine(MoveToPosition(t));
    //}

	public IEnumerator MoveToPosition(float timeToMove)
	{
		pushed = true;
		Vector3 currentPos = gameObject.transform.position;
		float t = 0f;
		while (t < 1)
		{

			t += Time.deltaTime / timeToMove;
			transform.position = Vector3.Lerp(currentPos, destination, t);
			//this.transform.localEulerAngles = new Vector3 (0, 30, 0);
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
}
