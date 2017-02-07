using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class SixStep : MonoBehaviour
{
	private int damage;
	public int meterCost = 25;

    public GameObject grid;
    public GameObject enemy;
	public GameObject attackHitbox;

    // Use this for initialization
    void Start()
    {
        damage = 100;
    }

    // Update is called once per frame
    void Update()
    {
        bool onb = grid.GetComponent<BeatKeeper2>().checkifonbeat();
		bool canMove = GetComponent<VanillaCharacter> ().canMove ();
    }

    public void Attack()
    {
		//play sound effect
        this.GetComponent<SoundMaster>().PlaySound("sixStepSound");

        // subtract meter cost
        this.GetComponent<VanillaCharacter>().meter -= meterCost;

        //get character positions
        Vector2 currentpos = GetComponent<CharacterMover>().getposition();
        Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();

        //turn attack visual on
        attackHitbox.SetActive(true);

        //check for hit
        if (enemypos.x == currentpos.x || enemypos.x == currentpos.x + 1 || enemypos.x == currentpos.x - 1)
        {

            if (enemypos.y == currentpos.y || enemypos.y == currentpos.y + 1 || enemypos.y == currentpos.y - 1)
            {
                enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
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
