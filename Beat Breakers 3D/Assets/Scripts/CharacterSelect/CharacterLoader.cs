using UnityEngine;
using System.Collections;

public class CharacterLoader : MonoBehaviour {

    //character prefabs
    private GameObject eva;
    private GameObject evaAlt;
    private GameObject naz;
	private GameObject cosmic;

    //positions/rotations
    private Vector3 startP1 = new Vector3(-3f, 1f, 0f);
    private Vector3 startP2 = new Vector3(3f, 1f, 0f);
    private Vector3 right = new Vector3(0f, 0f, 0f);
    private Vector3 left = new Vector3(0f, 180f, 0f);

    //character choices
    private GameObject charPicks;
    public string p1char;
    public string p2char;

    //final character prefabs
    private GameObject p1object;
    private GameObject p2object;

    void Awake ()
    {
        charPicks = GameObject.FindGameObjectWithTag("CharPicks");
        p1char = charPicks.GetComponent<CharPicks>().p1char;
        p2char = charPicks.GetComponent<CharPicks>().p2char;

        eva = Resources.Load("CharacterPrefabs/Eva") as GameObject;
        evaAlt = Resources.Load("CharacterPrefabs/EvaAlt") as GameObject;
        naz = Resources.Load("CharacterPrefabs/Naz") as GameObject;
		cosmic = Resources.Load("CharacterPrefabs/CosmicS") as GameObject;

        //check player 1 character choice
        if (p1char == "Eva")
        {
            p1object = eva;
        }
        else if (p1char == "EvaAlt")
        {
            p1object = evaAlt;
        }
        else if (p1char == "Naz")
        {
            p1object = naz;
        }
		else if (p1char == "CosmicS")
		{
			p1object = cosmic;
		}

        //check player 2 character choice
        if (p2char == "Eva")
        {
            p2object = eva;
        }
        else if (p2char == "EvaAlt")
        {
            p2object = evaAlt;
        }
        else if (p2char == "Naz")
        {
            p2object = naz;
        }
		else if (p2char == "CosmicS")
		{
			p2object = cosmic;
		}

        //load character prefabs
        Instantiate(p1object, startP1, Quaternion.Euler(right));
        Instantiate(p2object, startP2, Quaternion.Euler(left));
    }

	// Use this for initialization
	void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
