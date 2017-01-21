using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour {

    public GameObject enemy;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void GetKnockedBack()
    {
        Vector2 currentpos = GetComponent<CharacterMover>().getposition();
        Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();
        // If enemy is vertically adjacent
        if (enemypos.x == currentpos.x)
        {
            // If enemy is below, push up
            if (enemypos.y > currentpos.y)
            {
                this.GetComponent<CharacterMover>().setposition((int)currentpos.x, (int)currentpos.y - 1, .5f);
            } else // If enemy is above, push down
            {
                this.GetComponent<CharacterMover>().setposition((int)currentpos.x, (int)currentpos.y + 1, .5f);
            }
        // If enemy is horizontally adjacent
        } else if (enemypos.y == currentpos.y)
        {
            // If enemy is to the right, push left
            if (enemypos.x > currentpos.x)
            {
                this.GetComponent<CharacterMover>().setposition((int)currentpos.x-1, (int)currentpos.y, .5f);
            } else // If enemy is to the left, push right
            {
                this.GetComponent<CharacterMover>().setposition((int)currentpos.x + 1, (int)currentpos.y, .5f);
            }
        } else // If enemy is diagonally adjacent
        {
            // If enemy is to the right
            if (enemypos.x > currentpos.x)
            {
                // and below, push up-left
                if (enemypos.y > currentpos.y)
                {
                    this.GetComponent<CharacterMover>().setposition((int)currentpos.x - 1, (int)currentpos.y - 1, .5f);
                }
                else // and above, push down-left
                {
                    this.GetComponent<CharacterMover>().setposition((int)currentpos.x - 1, (int)currentpos.y + 1, .5f);
                }
            }
            else // If enemy is to the left 
            {
                // and below, push up-right
                if (enemypos.y > currentpos.y)
                {
                    this.GetComponent<CharacterMover>().setposition((int)currentpos.x + 1, (int)currentpos.y - 1, .5f);
                }
                else // and above, push down-right
                {
                    this.GetComponent<CharacterMover>().setposition((int)currentpos.x + 1, (int)currentpos.y + 1, .5f);
                }
            }
        }
    }
}
