﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class BasicAttack : MonoBehaviour
{
    private int damage;
    private GameObject grid;
    private GameObject enemy;
    public GameObject attackHitbox;
    private int player;

    //InControl device
    private InputDevice device;

    // Use this for initialization
    void Start()
    {
        player = this.GetComponent<VanillaCharacter>().player;

        //take scene objects from VanillaCharacter
        grid = this.GetComponent<VanillaCharacter>().grid;
        enemy = this.GetComponent<VanillaCharacter>().enemy;

        damage = 50;
        if (player == 1) {
            device = InputManager.Devices[0];
        } else if (player == 2) {
            device = InputManager.Devices[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //bool onb = grid.GetComponent<BeatKeeper2>().checkifonbeat();
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

                if (enemypos.y == currentpos.y)

                    enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
            }
        }
        if (direction == "right")
        {
            this.transform.localEulerAngles = (new Vector3(0, 0, 0));
            if (enemypos.x == currentpos.x + 1)
            {

                if (enemypos.y == currentpos.y)

                    enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
            }
        }
        if (direction == "up")
        {
            this.transform.localEulerAngles = (new Vector3(0, -90, 0));
            if (enemypos.y == currentpos.y - 1)
            {

                if (enemypos.x == currentpos.x)

                    enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);  
            }
        }
        if (direction == "down")
        {
            this.transform.localEulerAngles = (new Vector3(0, 90, 0));
            if (enemypos.y == currentpos.y + 1)
            {

                if (enemypos.x == currentpos.x)

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
