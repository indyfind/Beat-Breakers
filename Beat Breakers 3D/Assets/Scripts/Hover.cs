using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {
    private bool floatup = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (floatup)
        {
            StartCoroutine(FloatingUp());
        } else
        {
            StartCoroutine(FloatingDown());
        }
	}

    IEnumerator FloatingUp()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y + 1f * Time.deltaTime,
            transform.position.z);
        yield return new WaitForSeconds(1f);
        floatup = false;
    }

    IEnumerator FloatingDown()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y - 1f * Time.deltaTime,
            transform.position.z);
        yield return new WaitForSeconds(1f);
        floatup = true;
    }

}
