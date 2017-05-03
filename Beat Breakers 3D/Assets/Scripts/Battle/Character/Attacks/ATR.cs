using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ATR : MonoBehaviour
{
	private int meterCost = 25;
	public int damage = 100;
	private int lowDamage = 25;
	private Object projectilePrefab;
	private GameObject projectileLeft;
	private GameObject projectileDown;
	private GameObject projectileRight;
	private GameObject projectileUp;

    // Use this for initialization
    void Start()
    {
		projectilePrefab = Resources.Load("Prefabs/Projectile");
		/*
		if (GetComponent<VanillaCharacter>().player == 1){
			projectileLeft = Resources.Load("Prefabs/ProjectileLeft1") as GameObject;
			projectileDown = Resources.Load("Prefabs/ProjectileDown1") as GameObject;
			projectileRight = Resources.Load("Prefabs/ProjectileRight1") as GameObject;
			projectileUp = Resources.Load("Prefabs/ProjectileUp1") as GameObject;
		} else {
			projectileLeft = Resources.Load("Prefabs/ProjectileLeft2") as GameObject;
			projectileDown = Resources.Load("Prefabs/ProjectileDown2") as GameObject;
			projectileRight = Resources.Load("Prefabs/ProjectileRight2") as GameObject;
			projectileUp = Resources.Load("Prefabs/ProjectileUp2") as GameObject;
		} */
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
		//play sound effect
        this.GetComponent<CharacterSound>().PlaySound("ranged");

		if (GetComponent<VanillaCharacter>().meter >= meterCost) {
			SpawnProjectiles(damage);
			// subtract meter cost
        	this.GetComponent<VanillaCharacter>().meter -= meterCost;
		} else {
			SpawnProjectiles(lowDamage);
		}
    }

	private void SpawnProjectiles(int dam)
	{
		int xpos = (int)GetComponent<CharacterMover>().getposition().x;
		int ypos = (int)GetComponent<CharacterMover>().getposition().y;
		Debug.Log(xpos);
		Debug.Log(ypos);

		if (xpos-1 >= 0) {
		GameObject projectileLeft = Instantiate(projectilePrefab, 
			GetComponent<VanillaCharacter>().grid.GetComponent<GridMaster>().getPosition(xpos-1, ypos) , 
			Quaternion.Euler(Vector3.zero)) as GameObject;
		projectileLeft.GetComponent<Projectile>().damage = dam;
		projectileLeft.GetComponent<Projectile>().dir = Projectile.direction.Left;
		projectileLeft.GetComponent<Projectile>().player = GetComponent<VanillaCharacter>().player;
		projectileLeft.GetComponent<Projectile>().SetPosition(xpos, ypos);
		}
		if (ypos+1 <= 6) {
		GameObject projectileDown = Instantiate(projectilePrefab, 
			GetComponent<VanillaCharacter>().grid.GetComponent<GridMaster>().getPosition(xpos, ypos+1) , 
			Quaternion.Euler(Vector3.zero)) as GameObject;
		projectileDown.GetComponent<Projectile>().damage = dam;
		projectileDown.GetComponent<Projectile>().dir = Projectile.direction.Down;
		projectileDown.GetComponent<Projectile>().player = GetComponent<VanillaCharacter>().player;
		projectileDown.GetComponent<Projectile>().SetPosition(xpos, ypos);
		}
		if (xpos+1 <= 6) {
		GameObject projectileRight = Instantiate(projectilePrefab,
			GetComponent<VanillaCharacter>().grid.GetComponent<GridMaster>().getPosition(xpos+1, ypos) , 
			Quaternion.Euler(Vector3.zero)) as GameObject;
		projectileRight.GetComponent<Projectile>().damage = dam;
		projectileRight.GetComponent<Projectile>().dir = Projectile.direction.Right;
		projectileRight.GetComponent<Projectile>().player = GetComponent<VanillaCharacter>().player;
		projectileRight.GetComponent<Projectile>().SetPosition(xpos, ypos);
		}
		if (ypos-1 >= 0) {
		GameObject projectileUp = Instantiate(projectilePrefab,
			GetComponent<VanillaCharacter>().grid.GetComponent<GridMaster>().getPosition(xpos, ypos-1) , 
			Quaternion.Euler(Vector3.zero)) as GameObject;
		projectileUp.GetComponent<Projectile>().damage = dam;
		projectileUp.GetComponent<Projectile>().dir = Projectile.direction.Up;
		projectileUp.GetComponent<Projectile>().player = GetComponent<VanillaCharacter>().player;
		projectileUp.GetComponent<Projectile>().SetPosition(xpos, ypos);
		}
	}
}
