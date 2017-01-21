using UnityEngine;
using System.Collections;

public class Flow : MonoBehaviour
{

    private int counter;
    // Use this for initialization
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TripPlayer()
    {
        this.GetComponent<VanillaCharacter>().tripped = true;
        this.GetComponent<VanillaCharacter>().justTripped = true;
        counter = 4;
    }

    public void UnTripPlayer()
    {
        Debug.Log("Tripped counter");
        Debug.Log(counter);
        if (counter == 3)
        {
            Debug.Log("justTripped = true");
            this.GetComponent<VanillaCharacter>().tripped = false;
            this.GetComponent<VanillaCharacter>().justTripped = true;
        }
        if (counter == 1)
        {
            Debug.Log("justTripped = false");
            this.GetComponent<VanillaCharacter>().justTripped = false;
        }
        if (counter > 0)
        {
            counter--;

        }
    }


}
