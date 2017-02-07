using UnityEngine;
using System.Collections;

public class Flare : MonoBehaviour {

     private int flareCounter = 0;
     private int flaredamage = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void  FlareAttack()
    {
        if (flareCounter > 0)
        {
           this.GetComponent<VanillaCharacter>().TakeDamage(flaredamage, true);
            flareCounter -= 1;
        }
    }

    public void StartFlareAttack()
    {
        flareCounter = 4;
    }


}
