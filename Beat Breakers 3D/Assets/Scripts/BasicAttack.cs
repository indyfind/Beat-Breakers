using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BasicAttack : MonoBehaviour
{
    private int damage;
    public GameObject grid;
    public GameObject enemy;
    public GameObject attackHitbox;
    public int player;
    private string leftButton;
    private string rightButton;
    private string downButton;
    private string upButton;
    private float bpm;

    // Use this for initialization
    void Start()
    {
        bpm = grid.GetComponent<BeatKeeper>().getBPM();
        attackHitbox.GetComponent<MeshRenderer>().enabled = false;
        damage = 1;
        if (player == 1)
        {
            leftButton = "X1";
            rightButton = "B1";
            downButton = "A1";
            upButton = "Y1";
        }
        else if (player == 2)
        {
            leftButton = "X2";
            rightButton = "B2";
            downButton = "A2";
            upButton = "Y2";
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool onb = grid.GetComponent<BeatKeeper>().checkifonbeat();
        bool canMove = GetComponent<VanillaCharacter>().canMove();

        if ((Input.GetButtonDown(leftButton)) && canMove) // && onb
        {
            this.GetComponent<VanillaCharacter>().currentAction = "basicAttackLeft";
            //Attack();
            //StartCoroutine(CoolDown());
            //StartCoroutine(CoolDownDisplay());
            //GetComponent<VanillaCharacter>().actionTaken = true;
        }
    }

    public void Attack(string direction)
    {

        Vector2 currentpos = GetComponent<CharacterMover>().getposition();
        Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();

        attackHitbox.GetComponent<MeshRenderer>().enabled = true;
        if (enemypos.x == currentpos.x || enemypos.x == currentpos.x + 1 || enemypos.x == currentpos.x - 1)
        {

            if (enemypos.y == currentpos.y || enemypos.y == currentpos.y + 1 || enemypos.y == currentpos.y - 1)
            {
                enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
                enemy.GetComponent<VanillaCharacter>().Tripped(.5f);
            }

        }
        //GetComponent<VanillaCharacter>().actionTaken = true;
    }
}
