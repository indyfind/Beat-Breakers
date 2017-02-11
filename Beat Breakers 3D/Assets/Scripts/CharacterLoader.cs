﻿using UnityEngine;
using System.Collections;

public class CharacterLoader : MonoBehaviour {

    //character prefabs
    private GameObject eva;
    private GameObject evaAlt;
    private GameObject naz;

    //positions/rotations
    private Vector3 startP1 = new Vector3(-3f, 1f, 0f);
    private Vector3 startP2 = new Vector3(3f, 1f, 0f);
    private Vector3 right = new Vector3(0f, 0f, 0f);
    private Vector3 left = new Vector3(0f, 180f, 0f);

    //character choices
    private string p1char = "Eva";
    private string p2char = "Eva";

    //final character prefabs
    private GameObject p1object;
    private GameObject p2object;

    void Awake ()
    {
        eva = Resources.Load("CharacterPrefabs/Eva") as GameObject;
        evaAlt = Resources.Load("CharacterPrefabs/EvaAlt") as GameObject;
        naz = Resources.Load("CharacterPrefabs/Naz") as GameObject;

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