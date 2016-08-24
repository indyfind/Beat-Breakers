﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SixStep : MonoBehaviour
{

	private int cooldown;
	private int damage;
    public KeyCode key;
    public GameObject grid;
    public GameObject enemy;
    private bool onCoolDown = false;
	public GameObject attackHitbox;
	public int player;
	private string leftBumper;

	private Text cooldownText;
	public GameObject CooldownTimer;
	private int cooldownCount;
	private float bpm;
	public GameObject HUDIcon;

    // Use this for initialization
    void Start()
    {
		bpm = grid.GetComponent<BeatKeeper> ().getBPM ();
		attackHitbox.GetComponent<MeshRenderer> ().enabled = false;
        damage = 1;
        cooldown = 4;
		if (player == 1) {
			leftBumper = "LeftBumper1";
		} else if (player == 2) {
			leftBumper = "LeftBumper2";
		}
		cooldownText = CooldownTimer.GetComponent<Text> ();
		cooldownCount = cooldown * 2;
    }

    // Update is called once per frame
    void Update()
    {
        bool onb = grid.GetComponent<BeatKeeper>().checkifonbeat();
		bool canMove = GetComponent<VanillaCharacter> ().canMove ();

        if ((Input.GetKeyDown(key) || Input.GetButtonDown(leftBumper)) && onb && !onCoolDown && canMove)
        {
            Attack();
            StartCoroutine(CoolDown());
			StartCoroutine(CoolDownDisplay());
			GetComponent<VanillaCharacter>().actionTaken = true;
        }
    }

    void Attack()
    {
        Vector2 currentpos = GetComponent<CharacterMover>().getposition();
        Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();
		attackHitbox.GetComponent<MeshRenderer> ().enabled = true;
        if (enemypos.x == currentpos.x || enemypos.x == currentpos.x + 1 || enemypos.x == currentpos.x - 1)
        {

            if (enemypos.y == currentpos.y || enemypos.y == currentpos.y + 1 || enemypos.y == currentpos.y - 1)
            {
                enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
				enemy.GetComponent<VanillaCharacter>().Tripped(.5f);
            }

        }
    }

    IEnumerator CoolDown()
    {
        onCoolDown = true;
		yield return new WaitForSeconds (.2f);
		attackHitbox.GetComponent<MeshRenderer> ().enabled = false;
        yield return new WaitForSeconds(cooldown - .2f);
        onCoolDown = false;
    }

	IEnumerator CoolDownDisplay()
	{
		HUDIcon.GetComponent<Image> ().color = Color.grey;
		while (cooldownCount > 0) {
			cooldownText.text = cooldownCount.ToString();
			yield return new WaitForSeconds (60f / bpm);
			cooldownCount -= 1;
		}
		HUDIcon.GetComponent<Image> ().color = Color.white;
		cooldownText.text = "";
		cooldownCount = cooldown * 2;
	}
}
