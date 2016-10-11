using UnityEngine;
using System.Collections;
using InControl;

public class CharacterMover : MonoBehaviour {
    public int startingxPosition;
    public int startingyPosition;
    private int xposition = 0;
    private int yposition = 0;
    private float moverange = 1;
    public GameObject grid;
    private Vector3 destination;
    private bool pushed;
	public int player; // player 1 or player 2

    //InControl device
    private InputDevice device;

    // Use this for initialization
    void Start () {
        xposition = startingxPosition;
        yposition = startingyPosition;
		if (player == 1) {
            device = InputManager.Devices[0];
        } else if (player == 2) {
            device = InputManager.Devices[1];
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        bool onb = grid.GetComponent<BeatKeeper>().checkifonbeat();
		bool canMove = GetComponent<VanillaCharacter> ().canMove (); //check if player is tripped/already moved
        if ((device.DPad.WasPressed || device.LeftStick.WasPressed) && canMove) { //  && onb
            //if using left stick: is y value greater than x?
            if (Mathf.Abs(device.LeftStickY.Value) >= Mathf.Abs(device.LeftStickX.Value))
            {
                //move up
                if ((device.DPadUp.WasPressed || device.LeftStickUp.WasPressed) && yposition > 0)
                {
                    this.GetComponent<VanillaCharacter>().currentAction = "moveUp";
                    this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper>().rhythmRating;
                }
                //move down
                else if ((device.DPadDown.WasPressed || device.LeftStickDown.WasPressed) && yposition < 6)
                {
                    this.GetComponent<VanillaCharacter>().currentAction = "moveDown";
                    this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper>().rhythmRating;
                }
            }
            //if using left stick: is x value greater than y?
            if (Mathf.Abs(device.LeftStickY.Value) <= Mathf.Abs(device.LeftStickX.Value))
            {
                //move right
                if ((device.DPadRight.WasPressed || device.LeftStickRight.WasPressed) && xposition < 6)
                {
                    this.GetComponent<VanillaCharacter>().currentAction = "moveRight";
                    this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper>().rhythmRating;
                }
                //move left
                else if ((device.DPadLeft.WasPressed || device.LeftStickLeft.WasPressed) && xposition > 0)
                {
                    this.GetComponent<VanillaCharacter>().currentAction = "moveLeft";
                    this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper>().rhythmRating;
                }
            }
        }
    }
    public void MoveUp()
    {
        yposition -= 1;
        this.transform.localEulerAngles = (new Vector3(0, -90, 0));
        destination = new Vector3(grid.GetComponent<GridMaster>().getPosition(xposition, yposition).x, 1f, grid.GetComponent<GridMaster>().getPosition(xposition, yposition).y);
        StartCoroutine(MoveToPosition(.2f));
        //GetComponent<VanillaCharacter>().actionTaken = true; //action has been taken, so no more moves/attacks for this beat
    }
    public void MoveDown()
    {
        yposition += 1;
        this.transform.localEulerAngles = (new Vector3(0, 90, 0));
        destination = new Vector3(grid.GetComponent<GridMaster>().getPosition(xposition, yposition).x, 1f, grid.GetComponent<GridMaster>().getPosition(xposition, yposition).y);
        StartCoroutine(MoveToPosition(.2f));
        //GetComponent<VanillaCharacter>().actionTaken = true;
    }
    public void MoveLeft()
    {
        xposition -= 1;
        this.transform.localEulerAngles = (new Vector3(0, 180, 0));
        destination = new Vector3(grid.GetComponent<GridMaster>().getPosition(xposition, yposition).x, 1f, grid.GetComponent<GridMaster>().getPosition(xposition, yposition).y);
        StartCoroutine(MoveToPosition(.2f));
        //GetComponent<VanillaCharacter>().actionTaken = true;
    }
    public void MoveRight()
    {
        xposition += 1;
        this.transform.localEulerAngles = (new Vector3(0, 0, 0));
        destination = new Vector3(grid.GetComponent<GridMaster>().getPosition(xposition, yposition).x, 1f, grid.GetComponent<GridMaster>().getPosition(xposition, yposition).y);
        StartCoroutine(MoveToPosition(.2f));
        //GetComponent<VanillaCharacter>().actionTaken = true;
    }

    public Vector2 getposition()
    {
        return new Vector2((float)xposition, (float)yposition);
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
