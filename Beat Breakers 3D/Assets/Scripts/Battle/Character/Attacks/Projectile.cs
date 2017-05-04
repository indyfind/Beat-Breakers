using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public int player;
	private GameObject grid;
	private GameObject enemy;
	public int xposition;
	public int yposition;
	public int damage = 100;
	public enum direction
	{
		None,
		Up,
		Down,
		Left,
		Right,
	};
	public direction dir;
	public int lifespan = 3;

	// Use this for initialization
	void Start () {
		//dir = direction.Up;
		grid = GameObject.FindGameObjectWithTag("TheGrid");
		if (player == 1){
			enemy = GameObject.FindGameObjectWithTag("Player2");
		} else {
			enemy = GameObject.FindGameObjectWithTag("Player1");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetPosition(int x, int y){
		xposition = x;
		yposition = y;
	}

	public void MoveProjectile() {
		//Debug.Log("projectile moved");
		if (lifespan <= 0) {
			Destroy(this.gameObject);
		} else {
			StartCoroutine(MoveDirection(.2f, dir));
			lifespan -= 1;
		}
	}

	IEnumerator MoveDirection(float timeToMove, direction dir)
	{
		grid = GameObject.FindGameObjectWithTag("TheGrid");
		if (player == 1){
			enemy = GameObject.FindGameObjectWithTag("Player2");
		} else {
			enemy = GameObject.FindGameObjectWithTag("Player1");
		}
		int destinationx = xposition;
		int destinationy = yposition;

		switch (dir)
		{
		case direction.Up:
			if (yposition - 1 < 0)
			{
				Destroy(this.gameObject);
			}
			else
			{
				destinationy = yposition - 1;
			}
			break;
		case direction.Down:
			if (yposition + 1 > 6)
			{
				Destroy(this.gameObject);
			}
			else
			{
				destinationy = yposition + 1;
			}
			break;
		case direction.Left:
			if (xposition - 1 < 0)
			{
				Destroy(this.gameObject);
			}
			else
			{
				destinationx = xposition - 1;
			}
			break;
		case direction.Right:
			if (xposition + 1 > 6)
			{
				Destroy(this.gameObject);
			}
			else
			{
				destinationx = xposition + 1;
			}
			break;
		default:
			break;
		}

		Vector3 currentPos = gameObject.transform.position;
		Vector3 destination = new Vector3(grid.GetComponent<GridMaster>().getPosition(destinationx, destinationy).x, 1f, grid.GetComponent<GridMaster>().getPosition(destinationx, destinationy).y);

		// check for hit
		if (destinationx == enemy.GetComponent<CharacterMover>().getposition().x && destinationy == enemy.GetComponent<CharacterMover>().getposition().y) {
			enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
		}

		//set new position
		xposition = destinationx;
		yposition = destinationy;

		// move to destination
		float t = 0f;
		while (t <= 1) {
			t += Time.deltaTime / timeToMove;
			transform.position = Vector3.Lerp(currentPos, destination, t);
			//this.transform.localEulerAngles = new Vector3 (0, 30, 0);
			yield return null;
		}
	}

}
