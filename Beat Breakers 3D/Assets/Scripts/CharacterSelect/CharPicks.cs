using UnityEngine;
using System.Collections;

public class CharPicks : MonoBehaviour {
    public string p1char;
    public string p2char;
	public string song;
    
    // Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);

        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
