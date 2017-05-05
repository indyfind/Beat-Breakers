using UnityEngine;
using System.Collections;


public class CharacterSound : MonoBehaviour {

    private string character;
    private AudioClip basicAttackSound;
    private AudioClip getHitSound;
    private AudioClip meleeSound;
    private AudioClip rangedSound;
	private AudioClip Bump;
	private AudioClip Burn;
	private AudioClip Stun;
	private AudioClip FormSwitch;
	private AudioClip Block;
	private AudioClip Fall;
	private AudioClip NotEnoughMeter;

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
		} else if (character == "CosmicS") 
		{
			getHitSound = Resources.Load<AudioClip>("Sound/Eva Voice Lines/EVA-Ugh");
			meleeSound = Resources.Load<AudioClip>("Sound/SFX/Cosmic S Melee SFX");
			rangedSound = Resources.Load<AudioClip>("Sound/SFX/Cosmic S Range SFX");
		} else if (character == "Jameleon")
		{
			getHitSound = Resources.Load<AudioClip>("Sound/Eva Voice Lines/EVA-Ugh");
			meleeSound = Resources.Load<AudioClip>("Sound/SFX/Jameleon Melee SFX");
			rangedSound = Resources.Load<AudioClip>("Sound/SFX/Jameleon Range SFX");
		}
		Bump = Resources.Load<AudioClip>("Sound/SFX/Form Switching/SFX_Bump");
		Burn = Resources.Load<AudioClip>("Sound/SFX/Fire_SFX");
		Stun = Resources.Load<AudioClip>("Sound/SFX/Form Switching/SFX_Stun");
		FormSwitch = Resources.Load<AudioClip>("Sound/SFX/Form Switching/SFX_Form_Switch");
		Block = Resources.Load<AudioClip>("Sound/SFX/All Characters/SFX_Block");
		Fall = Resources.Load<AudioClip>("Sound/SFX/All Characters/SFX_Fall");
		NotEnoughMeter = Resources.Load<AudioClip>("Sound/SFX/All Characters/SFX_No_Energy");

    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void PlaySound(string sound, bool forcePlay=true)
    {
        if (!audioplayer.isPlaying || forcePlay)
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
				case "Bump":
					audioplayer.clip = Bump;
					audioplayer.Play ();
					break;
				case "Burn":
					audioplayer.clip = Burn;
					audioplayer.Play ();
					break;
				case "Stun":
					audioplayer.clip = Stun;
					audioplayer.Play ();
					break;
				case "FormSwitch":
					audioplayer.clip = FormSwitch;
					audioplayer.Play ();
					break;
				case "Block":
					audioplayer.clip = Block;
					audioplayer.Play ();
					break;
				case "Fall":
					audioplayer.clip = Fall;
					audioplayer.Play ();
					break;
				case "NotEnoughMeter":
					audioplayer.clip = NotEnoughMeter;
					audioplayer.Play ();
					break;
                default:
                    break;
            }
        }
    }
}
