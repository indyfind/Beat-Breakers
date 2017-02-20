﻿using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour
{
    //battle
    public AudioClip Countdown;
    public AudioClip Round1;
    public AudioClip Round2;
    public AudioClip Round3;
    public AudioClip[] RoundOver;
    public AudioClip[] AnnouncerRound2;
    public AudioClip[] AnnouncerRound3;
	public AudioClip[] AnyRound;

    //menus
    public AudioClip ChooseYourDancer;
    public AudioClip Naz;
    public AudioClip Eva;

    private AudioSource audioplayer;
    // Use this for initialization
    void Start()
    {
        Countdown = Resources.Load<AudioClip>("Sound/AnnouncerVoiceLines/UILines/321Dance");
        Round1 = Resources.Load<AudioClip>("Sound/AnnouncerVoiceLines/UILines/Round1");
        Round2 = Resources.Load<AudioClip>("Sound/AnnouncerVoiceLines/UILines/Round2");
        Round3 = Resources.Load<AudioClip>("Sound/AnnouncerVoiceLines/UILines/FinalRound");
        RoundOver = Resources.LoadAll<AudioClip>("Sound/AnnouncerVoiceLines/UILines/RoundOver");
        AnnouncerRound2 = Resources.LoadAll<AudioClip>("Sound/AnnouncerVoiceLines/Round2");
        AnnouncerRound3 = Resources.LoadAll<AudioClip>("Sound/AnnouncerVoiceLines/Round3");
		AnyRound = Resources.LoadAll<AudioClip>("Sound/AnnouncerVoiceLines/AnyRound");
        Eva = Resources.Load<AudioClip>("Sound/AnnouncerVoiceLines/UILines/EVA");
        Naz = Resources.Load<AudioClip>("Sound/AnnouncerVoiceLines/UILines/NAZ");
        ChooseYourDancer = Resources.Load<AudioClip>("Sound/AnnouncerVoiceLines/UILines/ChooseYourDancer");
        audioplayer = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

	public void PlaySound(string sound, bool forcePlay=false)
    {
		if (!audioplayer.isPlaying || forcePlay) {
			switch (sound) {
			case "Countdown":
				audioplayer.clip = Countdown;
				audioplayer.Play ();
				break;
			case "Round1":
				audioplayer.clip = Round1;
				audioplayer.Play ();
				break;
			case "Round2":
				audioplayer.clip = Round2;
				audioplayer.Play ();
				break;
			case "Round3":
				audioplayer.clip = Round3;
				audioplayer.Play ();
				break;
			case "RoundOver":
				audioplayer.clip = RoundOver [Random.Range (0, RoundOver.Length - 1)];
				audioplayer.Play ();
				break;
			case "Round2Sound":
				audioplayer.clip = AnnouncerRound2 [Random.Range (0, AnnouncerRound2.Length - 1)];
				audioplayer.Play ();
				break;
			case "Round3Sound":
				audioplayer.clip = AnnouncerRound3 [Random.Range (0, AnnouncerRound3.Length - 1)];
				audioplayer.Play ();
				break;
			case "AnyRound":
				audioplayer.clip = AnyRound [Random.Range (0, AnyRound.Length - 1)];
				audioplayer.Play ();
				break;
            case "ChooseYourCharacter":
                audioplayer.clip = ChooseYourDancer;
                audioplayer.Play();
                break;
            case "Eva":
                audioplayer.clip = Eva;
                audioplayer.Play();
                break;
            case "Naz":
                audioplayer.clip = Naz;
                audioplayer.Play();
                break;
            default:
				break;
			}
		}
    }
}
