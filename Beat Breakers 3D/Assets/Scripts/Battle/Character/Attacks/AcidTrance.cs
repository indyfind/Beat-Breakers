using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AcidTrance : MonoBehaviour
{
	public int damage = 100;
	private int meterCost = 25;
	private int tempDamage;
	private int knockback;

    private GameObject grid;
    private GameObject enemy;
	public GameObject attackHitbox;

	private string direction;

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
        this.GetComponent<CharacterSound>().PlaySound("ranged");

		if (GetComponent<VanillaCharacter>().meter >= meterCost) {
			tempDamage = damage;
			knockback = 1;
			// subtract meter cost
        	this.GetComponent<VanillaCharacter>().meter -= meterCost;
		} else {
			tempDamage = 25;
			knockback = 0;
		}

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

		//Debug.Log(direction);

        //check for hit
		if (direction == "up") {
			if (enemypos.y == currentpos.y - 4)
        	{
				if (enemypos.x >= currentpos.x - 1 && enemypos.x <= currentpos.x + 1)
            	{
					enemy.GetComponent<VanillaCharacter>().TakeDamage(tempDamage, false, knockback);
				} else if (enemypos.x == currentpos.x - 2 || enemypos.x == currentpos.x + 2) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage((int)(tempDamage * 0.8f));
				}
			} else if (enemypos.y == currentpos.y - 3) {
				if (enemypos.x == currentpos.x) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage(tempDamage, false, knockback);
				} else if (enemypos.x == currentpos.x - 1 || enemypos.x == currentpos.x + 1) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage((int)(tempDamage * 0.8f));
				}
			} else if (enemypos.y == currentpos.y - 2) {
				if (enemypos.x == currentpos.x) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage((int)(tempDamage * 0.8f));
				}
			}
		} else if (direction == "down") {
			if (enemypos.y == currentpos.y + 4)
			{
				if (enemypos.x >= currentpos.x - 1 && enemypos.x <= currentpos.x + 1)
				{
					enemy.GetComponent<VanillaCharacter>().TakeDamage(tempDamage, false, knockback);
				} else if (enemypos.x == currentpos.x - 2 || enemypos.x == currentpos.x + 2) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage((int)(tempDamage * 0.8f));
				}
			} else if (enemypos.y == currentpos.y + 3) {
				if (enemypos.x == currentpos.x) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage(damage, false, knockback);
				} else if (enemypos.x == currentpos.x - 1 || enemypos.x == currentpos.x + 1) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage((int)(tempDamage * 0.8f));
				}
			} else if (enemypos.y == currentpos.y + 2) {
				if (enemypos.x == currentpos.x) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage((int)(tempDamage * 0.8f));
				}
			}
		} else if (direction == "right") {
			if (enemypos.x == currentpos.x + 4)
			{
				if (enemypos.y >= currentpos.y - 1 && enemypos.y <= currentpos.y + 1)
				{
					enemy.GetComponent<VanillaCharacter>().TakeDamage(tempDamage, false, knockback);
				} else if (enemypos.y == currentpos.y - 2 || enemypos.y == currentpos.y + 2) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage((int)(tempDamage * 0.8f));
				}
			} else if (enemypos.x == currentpos.x + 3) {
				if (enemypos.y == currentpos.y) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage(tempDamage, false, knockback);
				} else if (enemypos.y == currentpos.y - 1 || enemypos.y == currentpos.y + 1) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage((int)(tempDamage * 0.8f));
				}
			} else if (enemypos.x == currentpos.x + 2) {
				if (enemypos.y == currentpos.y) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage((int)(tempDamage * 0.8f));
				}
			}
		} else if (direction == "left") {
			if (enemypos.x == currentpos.x - 4)
			{
				if (enemypos.y >= currentpos.y - 1 && enemypos.y <= currentpos.y + 1)
				{
					enemy.GetComponent<VanillaCharacter>().TakeDamage(tempDamage, false, knockback);
				} else if (enemypos.y == currentpos.y - 2 || enemypos.y == currentpos.y + 2) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage((int)(tempDamage * 0.8f));
				}
			} else if (enemypos.x == currentpos.x - 3) {
				if (enemypos.y == currentpos.y) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage(tempDamage, false, knockback);
				} else if (enemypos.y == currentpos.y - 1 || enemypos.y == currentpos.y + 1) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage((int)(tempDamage * 0.8f));
				}
			} else if (enemypos.x == currentpos.x - 2) {
				if (enemypos.y == currentpos.y) {
					enemy.GetComponent<VanillaCharacter>().TakeDamage((int)(tempDamage * 0.8f));
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
