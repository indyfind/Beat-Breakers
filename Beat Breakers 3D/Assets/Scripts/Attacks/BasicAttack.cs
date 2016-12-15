using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class BasicAttack : MonoBehaviour
{
    private int damage;
    public GameObject grid;
    public GameObject enemy;
    public GameObject attackHitbox;
    public int player;

    //InControl device
    private InputDevice device;

    // Use this for initialization
    void Start()
    {
        damage = 5;
        if (player == 1) {
            device = InputManager.Devices[0];
        } else if (player == 2) {
            device = InputManager.Devices[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool onb = grid.GetComponent<BeatKeeper2>().checkifonbeat();
        bool canMove = GetComponent<VanillaCharacter>().canMove();

//        if (device.Action3.WasPressed && canMove) // && onb // (Input.GetButtonDown(leftButton)) 
//        {
//            this.GetComponent<VanillaCharacter>().currentAction = "basicAttackLeft";
//            this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper2>().rhythmRating;
//        }
//        if (device.Action2.WasPressed && canMove) // && onb
//        {
//            this.GetComponent<VanillaCharacter>().currentAction = "basicAttackRight";
//            this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper2>().rhythmRating;
//        }
//        if (device.Action4.WasPressed && canMove) // && onb
//        {
//            this.GetComponent<VanillaCharacter>().currentAction = "basicAttackUp";
//            this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper2>().rhythmRating;
//        }
//        if (device.Action1.WasPressed && canMove) // && onb
//        {
//            this.GetComponent<VanillaCharacter>().currentAction = "basicAttackDown";
//            this.GetComponent<VanillaCharacter>().rhythmRating = grid.GetComponent<BeatKeeper2>().rhythmRating;
//        }
    }

    public void Attack(string direction)
    {

        Vector2 currentpos = GetComponent<CharacterMover>().getposition();
        Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();

        attackHitbox.SetActive(true);
        if (direction == "left")
        {
            this.transform.localEulerAngles = (new Vector3(0, 180, 0));
            if (enemypos.x == currentpos.x - 1)
            {

                if (enemypos.y == currentpos.y || enemypos.y == currentpos.y + 1 || enemypos.y == currentpos.y - 1)

                    enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
            }
        }
        if (direction == "right")
        {
            this.transform.localEulerAngles = (new Vector3(0, 0, 0));
            if (enemypos.x == currentpos.x + 1)
            {

                if (enemypos.y == currentpos.y || enemypos.y == currentpos.y + 1 || enemypos.y == currentpos.y - 1)

                    enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
            }
        }
        if (direction == "up")
        {
            this.transform.localEulerAngles = (new Vector3(0, -90, 0));
            if (enemypos.y == currentpos.y - 1)
            {

                if (enemypos.x == currentpos.x || enemypos.x == currentpos.x + 1 || enemypos.x == currentpos.x - 1)

                    enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
                    
                    
                    
            }
        }
        if (direction == "down")
        {
            this.transform.localEulerAngles = (new Vector3(0, 90, 0));
            if (enemypos.y == currentpos.y + 1)
            {

                if (enemypos.x == currentpos.x || enemypos.x == currentpos.x + 1 || enemypos.x == currentpos.x - 1)

                    enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
            }
        }
        StartCoroutine(CoolDown());
    }
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(.4f);
        attackHitbox.SetActive(false);
    }
}
