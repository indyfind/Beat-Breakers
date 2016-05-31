using UnityEngine;
using System.Collections;

public class CharacterMover : MonoBehaviour {
    public int startingxPosition;
    public int startingyPosition;
    private int xposition = 0;
    private int yposition = 0;
    private float moverange = 1;
    public GameObject grid;
    private Vector3 destination;
    public KeyCode up;
    public KeyCode left;
    public KeyCode right;
    public KeyCode down;
    private bool pushed;
	public int player; // player 1 or player 2
	//move buttons
	private string joystickX;
	private string joystickY;

    // Use this for initialization
    void Start () {
        xposition = startingxPosition;
        yposition = startingyPosition;

		//Assign correct controller inputs based on which player it is
		if (player == 1) {
			joystickX = "JoystickX1";
			joystickY = "JoystickY1";
		} else {
			joystickX = "JoystickX2";
			joystickY = "JoystickY2";
		}
	}
	
	// Update is called once per frame
	void Update () {
        bool onb = grid.GetComponent<BeatKeeper>().checkifonbeat();
		bool canMove = GetComponent<VanillaCharacter> ().canMove (); //check if player is tripped/already moved
		//move up
		if ((Input.GetKeyDown(up) || Input.GetAxisRaw (joystickY) < 0f) && yposition > 0 && onb && canMove)
        {
            yposition -= 1;
			this.transform.localEulerAngles = (new Vector3 (0, -90, 0));
			destination = new Vector3 (grid.GetComponent<GridMaster>().getPosition(xposition, yposition).x, 1f, grid.GetComponent<GridMaster>().getPosition(xposition, yposition).y);
            StartCoroutine(MoveToPosition(.2f));
			GetComponent<VanillaCharacter>().actionTaken = true; //action has been taken, so no more moves/attacks for this beat
        }
        //move down
		else if ((Input.GetKeyDown(down) || Input.GetAxisRaw (joystickY) > 0f) && yposition < 6 && onb && canMove) 
        {
            yposition += 1;
			this.transform.localEulerAngles = (new Vector3 (0, 90, 0));
			destination = new Vector3 (grid.GetComponent<GridMaster>().getPosition(xposition, yposition).x, 1f, grid.GetComponent<GridMaster>().getPosition(xposition, yposition).y);
            StartCoroutine(MoveToPosition(.2f));
			GetComponent<VanillaCharacter>().actionTaken = true;
        }
        //move right
		else if ((Input.GetKeyDown(right) || Input.GetAxisRaw (joystickX) > 0f) && xposition < 6 && onb && canMove)
        {
            xposition += 1;
			this.transform.localEulerAngles = (new Vector3 (0, 0, 0));
			destination = new Vector3 (grid.GetComponent<GridMaster>().getPosition(xposition, yposition).x, 1f, grid.GetComponent<GridMaster>().getPosition(xposition, yposition).y);
            StartCoroutine(MoveToPosition(.2f));
			GetComponent<VanillaCharacter>().actionTaken = true;
        }
        //move left
		else if ((Input.GetKeyDown(left) || Input.GetAxisRaw (joystickX) < 0f) && xposition > 0 && onb && canMove)
        {
            xposition -= 1;
			this.transform.localEulerAngles = (new Vector3 (0, 180, 0));
			destination = new Vector3 (grid.GetComponent<GridMaster>().getPosition(xposition, yposition).x, 1f, grid.GetComponent<GridMaster>().getPosition(xposition, yposition).y);
            StartCoroutine(MoveToPosition(.2f));
			GetComponent<VanillaCharacter>().actionTaken = true;
        }
    }
    public Vector2 getposition()
    {
        return new Vector2((float)xposition, (float)yposition);
    }

    public void setposition(int x, int y)
    {
        xposition = x;
        yposition = y;
    }
    
    public void moveenemy(float t ,Vector3 pos)
    {
        destination = pos;
        StartCoroutine(MoveToPosition(t));
    }
    
    public IEnumerator MoveToPosition(float timeToMove)
    {
        Vector3 currentPos = gameObject.transform.position;
        float t = 0f;
        while (t < 1)
        {
             
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, destination, t);
			//this.transform.localEulerAngles = new Vector3 (0, 30, 0);
            yield return null;
        }
    }
}
