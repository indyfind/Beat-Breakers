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
					this.GetComponent<BeatKeeper2>().beatsLeft = 228;
			}
			else if (songname == "pyramid Planet"){
					index = 1;
					this.GetComponent<BeatKeeper2>().beatsLeft = 404;
			}
			else if (songname == "Neo Nebula"){
					index = 2;
					this.GetComponent<BeatKeeper2>().beatsLeft = 332;
			}
			else if (songname == "Milky Way Wishes"){
					index = 3;
					this.GetComponent<BeatKeeper2>().beatsLeft = 356;
			}
			else if (songname == "MegaMix"){
					index = 4;
					this.GetComponent<BeatKeeper2>().beatsLeft = 512;
			}
			else if (songname == "Groovin Galaxy"){
					index = 5;
					this.GetComponent<BeatKeeper2>().beatsLeft = 300;
			}
			else if (songname == "Dark Matter"){
					index = 6;
					this.GetComponent<BeatKeeper2>().beatsLeft = 480;
			}
			else if (songname == "Celestial Catwalk"){
					index = 7;
					this.GetComponent<BeatKeeper2>().beatsLeft = 316;
			}
			else if (songname ==  "BoomBox Theory"){
					index = 8;
					this.GetComponent<BeatKeeper2>().beatsLeft = 318;
			}
			else if (songname == "Baroque Breakers"){
					index = 9;
					this.GetComponent<BeatKeeper2>().beatsLeft = 346;
			}

		this.GetComponent<Koreographer>().EventDelayInSeconds = delays[index];
		GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().clip = Songs[index];
		//this.GetComponent<Koreographer>().LoadKoreography(koreos[index]);
		//this.GetComponent<Koreographer>().GetKoreographyAtIndex(index);
	}
}
