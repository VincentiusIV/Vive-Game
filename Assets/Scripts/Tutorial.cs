using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

    public AudioClip wellDoneSound;
    public AudioClip[] voice;
    private AudioSource sound;
    public int phaseCounter;
	// Use this for initialization
	void Awake ()
    {
        sound = GetComponent<AudioSource>();
        phaseCounter = 0;
	}

    void Update()
    {
        if(sound.isPlaying == false && phaseCounter == 0)
        {
            sound.clip = voice[1];
            sound.Play();
            phaseCounter++;
        }
    }
   
	public void StartTutorial()
    {
        if(phaseCounter == 1)
        {
            StartCoroutine(GrabTutorial());
            phaseCounter++;
        }
    }

    IEnumerator GrabTutorial()
    {
        sound.clip = wellDoneSound;
        sound.Play();
        yield return new WaitForSeconds(sound.clip.length);
        sound.clip = voice[2];
        sound.Play();
        yield return new WaitForSeconds(sound.clip.length);
        sound.clip = voice[3];
        sound.Play();
        yield return new WaitForSeconds(sound.clip.length);
    }

    public void StartPinchTutorial()
    {
        if(phaseCounter == 2)
        {
            StartCoroutine(PinchTutorial());
            phaseCounter++;
        }
    }

    IEnumerator PinchTutorial()
    {
        sound.clip = wellDoneSound;
        sound.Play();
        yield return new WaitForSeconds(sound.clip.length);
        sound.clip = voice[4];
        sound.Play();
        yield return new WaitForSeconds(sound.clip.length);
        sound.clip = voice[5];
        sound.Play();
        yield return new WaitForSeconds(sound.clip.length);
    }

    public void EndTutorial()
    {
        sound.clip = voice[6];
        sound.Play();
    }
}
