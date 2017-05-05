using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class SongLoader : MonoBehaviour {
	 
	public GameObject selectedSong;
	public string songname;
	public AudioClip[] Songs;
	public Koreography[] koreos;
	public float[] delays;
	private int index;
	// Use this for initialization
	void Start () {
		selectedSong = GameObject.FindGameObjectWithTag("CharPicks");
		songname = selectedSong.GetComponent<CharPicks>().song;



		LoadSong();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadSong(){

			if(songname == "Spook House Theme"){
					index = 0;
			}
			else if (songname == "pyramid Planet"){
					index = 1;

			}
			else if (songname == "Neo Nebula"){
					index = 2;
			}
			else if (songname == "Milky Way Wishes"){
					index = 3;
			}
			else if (songname == "MegaMix"){
					index = 4;
			}
			else if (songname == "Groovin Galaxy"){
					index = 5;
			}
			else if (songname == "Dark Matter"){
					index = 6;
			}
			else if (songname == "Celestial Catwalk"){
					index = 7;
			}
			else if (songname ==  "BoomBox Theory"){
					index = 8;
			}
			else if (songname == "Baroque Breakers"){
					index = 9;
			}

		this.GetComponent<Koreographer>().EventDelayInSeconds = delays[index];
		GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().clip = Songs[index];
		//this.GetComponent<Koreographer>().LoadKoreography(koreos[index]);
		//this.GetComponent<Koreographer>().GetKoreographyAtIndex(index);
	}
}
