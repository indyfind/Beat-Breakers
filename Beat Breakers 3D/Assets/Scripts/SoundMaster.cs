using UnityEngine;
using System.Collections;


public class SoundMaster : MonoBehaviour {

    public AudioSource getHitSound;
    public AudioSource popNLockSound;
    public AudioSource headSlideSound;
    public AudioSource sixStepSound;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void PlaySound(string sound)
    {
        switch (sound)
        {

            case "getHitSound":
                getHitSound.Play();
                break;
            case "popNLockSound":
                popNLockSound.Play();
                break;
            case "headSlideSound":
                headSlideSound.Play();
                break;
            case "sixStepSound":
                sixStepSound.Play();
                break;
            default:
                break;



        }
    }
}
