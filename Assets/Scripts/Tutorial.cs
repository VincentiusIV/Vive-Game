using UnityEngine;
using System.Collections;

/* Script that switches between voices of the tutorial
 * a new phase is called when the player does the right action
 * for example if the pink ball is grabbed using trigger button, next phase is started
 * coroutines are used to combine sound clips and wait for each sound to play out
 * */ 
public class Tutorial : MonoBehaviour
{
    public AudioClip wellDoneSound;
    public AudioClip[] voice;
    private AudioSource sound;
    public int phaseCounter;

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
        phaseCounter++;
    }
}
