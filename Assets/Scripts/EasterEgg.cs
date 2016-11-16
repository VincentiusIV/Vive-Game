using UnityEngine;
using System.Collections;

// Small script that handles easter egg behaviour
// currently it only plays a sound, but can be expanded on in the future
public class EasterEgg : MonoBehaviour
{
    private AudioSource sound;

	void Awake ()
    {
        sound = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter()
    {
        sound.Play();
    }
}
