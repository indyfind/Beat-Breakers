using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class Shimmy : MonoBehaviour
{
	private int damage = 100;
	public int meterCost = 25;

    private GameObject grid;
    private GameObject enemy;
	public GameObject attackHitbox;

	string direction;

    // Use this for initialization
    void Start()
    {
        //take scene objects from VanillaCharacter
        grid = this.GetComponent<VanillaCharacter>().grid;
        enemy = this.GetComponent<VanillaCharacter>().enemy;
    }

    // Update is called once per frame
    void Update()
    {
        //bool onb = grid.GetComponent<BeatKeeper2>().checkifonbeat();
		//bool canMove = GetComponent<VanillaCharacter> ().canMove ();
    }

    public void Attack()
    {
		//play sound effect
        this.GetComponent<CharacterSound>().PlaySound("melee");

        // subtract meter cost
        this.GetComponent<VanillaCharacter>().meter -= meterCost;

        //get character positions
        Vector2 currentpos = GetComponent<CharacterMover>().getposition();
        Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();

        //turn attack visual on
        attackHitbox.SetActive(true);

		//check direction
		if (this.transform.localEulerAngles.y == 270) {
			direction = "up";
		} else if (this.transform.localEulerAngles.y == 90) {
			direction = "down";
		} else if (this.transform.localEulerAngles.y == 0) {
			direction = "right";
		} else { //(this.transform.localEulerAngles.y == 180) {
			direction = "left";
		}

		Debug.Log(direction);

        //check for hit
		if (direction == "up") {
        	if (enemypos.x == currentpos.x || enemypos.x == currentpos.x + 1 || enemypos.x == currentpos.x - 1)
        	{
            	if (enemypos.y >= currentpos.y - 2 && enemypos.y <= currentpos.y)
            	{
               		enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
            	}
        	}
		} else if (direction == "down") {
			if (enemypos.x == currentpos.x || enemypos.x == currentpos.x + 1 || enemypos.x == currentpos.x - 1)
			{
				if (enemypos.y <= currentpos.y + 2 && enemypos.y >= currentpos.y)
				{
					enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
				}
			}
		} else if (direction == "right") {
			if (enemypos.y == currentpos.y || enemypos.y == currentpos.y + 1 || enemypos.y == currentpos.y - 1)
			{
				if (enemypos.x <= currentpos.x + 2 && enemypos.x >= currentpos.x)
				{
					enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
				}
			}
		} else if (direction == "left") {
			if (enemypos.y == currentpos.y || enemypos.y == currentpos.y + 1 || enemypos.y == currentpos.y - 1)
			{
				if (enemypos.x >= currentpos.x - 2 && enemypos.x <= currentpos.x)
				{
					enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
				}
			}
		}

		//turn off attack visual
		StartCoroutine(hitBoxOff());
    }

    IEnumerator hitBoxOff()
    {
		yield return new WaitForSeconds (.4f);
        attackHitbox.SetActive(false);
    }
}
