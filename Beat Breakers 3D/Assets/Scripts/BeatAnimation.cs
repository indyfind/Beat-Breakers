using UnityEngine;
using System.Collections;

public class BeatAnimation : MonoBehaviour {

    private AudioClip testAudio;
    private Vector3 originalScale;
	private Vector3 newScale;
    private AudioSource audio;
    private float bpm = 140f;
    private float loopTime;
    // Use this for initialization
    void Start () {
        /*
        testAudio = Resources.Load<AudioClip>("New Main Theme Song");
        audio = this.GetComponent<AudioSource>();
        originalScale = this.GetComponent<Transform> ().localScale;
        newScale = new Vector3 (originalScale.x * 1.025f, originalScale.y * 1.025f, originalScale.z * 1.025f);
        audio.Play();
        float nextBeatSample = (float)AudioSettings.dspTime * testAudio.frequency;
        float loopTime = (30f / 1000f);
        float samplePeriod = (60f / bpm) * testAudio.frequency;
        StartCoroutine(ToBeat(samplePeriod, nextBeatSample));
        */
        originalScale = this.GetComponent<Transform>().localScale;
        newScale = new Vector3(originalScale.x * 1.025f, originalScale.y * 1.025f, originalScale.z * 1.025f);
        StartCoroutine(BeatPulse());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Delay()
	{
		yield return new WaitForSeconds (.1f);
		this.transform.localScale = originalScale;
	}

    IEnumerator BeatPulse()
    {
        yield return new WaitForSeconds(.15f);
        while (true)
        {
            yield return new WaitForSeconds(3.5f);
            this.transform.localScale = newScale;
            yield return new WaitForSeconds(.1f);
            this.transform.localScale = originalScale;
        }
    }
    /*
    IEnumerator ToBeat(float samplePeriod, float nextBeatSample)
    {
        //yield return new WaitForSeconds(.05f);
        while (audio.isPlaying)
        {
            float currentSample = (float)AudioSettings.dspTime * testAudio.frequency;
            //			Debug.Log(currentSample + "current");
            //			Debug.Log(nextBeatSample + "nextBeat");

            //Beat happens
            if (currentSample >= nextBeatSample)
            {
                nextBeatSample += samplePeriod;
                //GetComponent<Renderer>().material.color = Color.green;
                //beatHappened = true; //beatHappened is directly when beat happens
                //make title pulse to beat
                //this.transform.localScale = newScale;
			}
            if (currentSample >= nextBeatSample - (.34f * testAudio.frequency))
            {
                this.transform.localScale = originalScale;
            }
            if (currentSample >= nextBeatSample - (.214f * testAudio.frequency))
            {
                this.transform.localScale = newScale;
            }
            if (currentSample >= nextBeatSample - (.114f * testAudio.frequency))
            {
                this.transform.localScale = originalScale;
            }
            if (currentSample >= nextBeatSample - (.018f * testAudio.frequency))
            {
                this.transform.localScale = newScale;
            }
            yield return new WaitForSeconds(loopTime);
        }
    }
    */
}
