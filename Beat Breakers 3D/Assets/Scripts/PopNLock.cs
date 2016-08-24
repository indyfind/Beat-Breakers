﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopNLock : MonoBehaviour {
	
	public GameObject grid;
	public GameObject enemy;
	public GameObject attackHitbox;
	private int cooldown;
	private int damage;
	public KeyCode key;
	private bool onCoolDown = false;
	public int player;
	private string rightBumper;

	private int cooldownCount;
	private Text cooldownText;
	public GameObject cooldownTimer;
	private float bpm;
	public GameObject HUDIcon;

	// Use this for initialization
	void Start () {
		bpm = grid.GetComponent<BeatKeeper> ().getBPM ();
		damage = 2;
		cooldown = 4;
		if (player == 1) {
			rightBumper = "RightBumper1";
		} else if (player == 2) {
			rightBumper = "RightBumper2";
		}
		cooldownText = cooldownTimer.GetComponent<Text> ();
		cooldownCount = cooldown * 2;
		attackHitbox.GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		bool onb = grid.GetComponent<BeatKeeper> ().checkifonbeat ();
		bool canMove = GetComponent<VanillaCharacter> ().canMove ();
		if ((Input.GetKeyDown (key) || Input.GetButtonDown (rightBumper)) && onb && !onCoolDown && canMove) {
			Debug.Log(this.transform.localEulerAngles.y);
			if (((Mathf.Round(this.transform.localEulerAngles.y)) == 90) || (Mathf.Round(this.transform.localEulerAngles.y) == 270)) {
				Debug.Log ("horizontal");
				HorizontalAttack ();
				StartCoroutine (CoolDown ());
				StartCoroutine (CoolDownDisplay ());
				GetComponent<VanillaCharacter> ().actionTaken = true;
			} else {
				Debug.Log ("vertical");
				VerticalAttack ();
				StartCoroutine (CoolDown ());
				StartCoroutine (CoolDownDisplay ());
				GetComponent<VanillaCharacter> ().actionTaken = true;
			}
		}
	}

	void HorizontalAttack()
	{
		attackHitbox.GetComponent<MeshRenderer> ().enabled = true;
		Vector2 currentpos = GetComponent<CharacterMover>().getposition();
		Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();	
		if ((enemypos.x <= currentpos.x + 2 && enemypos.x >= currentpos.x - 2) && enemypos.y == currentpos.y)
		{	
			enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
		}

	}

	void VerticalAttack()
	{
		attackHitbox.GetComponent<MeshRenderer> ().enabled = true;
		Vector2 currentpos = GetComponent<CharacterMover>().getposition();
		Vector2 enemypos = enemy.GetComponent<CharacterMover>().getposition();
		if ((enemypos.y <= currentpos.y + 2 && enemypos.y >= currentpos.y - 2) && enemypos.x == currentpos.x)
		{	
			enemy.GetComponent<VanillaCharacter>().TakeDamage(damage);
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
