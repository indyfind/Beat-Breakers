using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

    public AudioClip[] AnnouncerRound2;
    private AudioSource audioplayer;
	// Use this for initialization
	void Start () {
        AnnouncerRound2 = Resources.LoadAll<AudioClip>("Sound/AnnouncerVoiceLines/Round2");
        audioplayer = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaySound(string sound)
    {
        switch (sound)
        {

            case "Round2Sound":
                audioplayer.clip = AnnouncerRound2[Random.Range(0, AnnouncerRound2.Length - 1)];
                audioplayer.Play();
                break;
            default:
                break;
        }
    }
}
