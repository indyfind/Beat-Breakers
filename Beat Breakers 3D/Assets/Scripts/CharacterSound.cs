using UnityEngine;
using System.Collections;


public class CharacterSound : MonoBehaviour {

    private string character;
    private AudioClip basicAttackSound;
    private AudioClip getHitSound;
    private AudioClip meleeSound;
    private AudioClip rangedSound;
    private AudioSource audioplayer;

    void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        audioplayer = this.GetComponent<AudioSource>();
        character = GetComponent<VanillaCharacter>().character;
        basicAttackSound = Resources.Load<AudioClip>("Sound/SFX/Zap");
        if (character == "Eva")
        {
            getHitSound = Resources.Load<AudioClip>("Sound/Eva Voice Lines/EVA-Ugh");
            meleeSound = Resources.Load<AudioClip>("Sound/SFX/Eva/SFX_Eva_Melee");
            rangedSound = Resources.Load<AudioClip>("Sound/SFX/Eva/SFX_Eva_Ranged");
        } else if (character == "Naz")
        {
            getHitSound = Resources.Load<AudioClip>("Sound/Eva Voice Lines/EVA-Ugh");
            meleeSound = Resources.Load<AudioClip>("Sound/SFX/Nazyilan/SFX_Nazyilan_Melee");
            rangedSound = Resources.Load<AudioClip>("Sound/SFX/Nazyilan/SFX_Nazyilan_Ranged");
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void PlaySound(string sound, bool forcePlay=false)
    {
        if (true)//!audioplayer.isPlaying || forcePlay)
        {
            switch (sound)
            {
                case "melee":
                    audioplayer.clip = meleeSound;
                    audioplayer.Play();
                    break;
                case "ranged":
                    audioplayer.clip = rangedSound;
                    audioplayer.Play();
                    break;
                case "basic":
                    audioplayer.clip = basicAttackSound;
                    audioplayer.Play();
                    break;
                case "getHit":
                    audioplayer.clip = getHitSound;
                    audioplayer.Play();
                    break;
                default:
                    break;
            }
        }
    }
}
