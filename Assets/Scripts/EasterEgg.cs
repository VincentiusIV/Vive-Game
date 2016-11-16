using UnityEngine;
using System.Collections;

public class EasterEgg : MonoBehaviour
{
    private AudioSource sound;
	// Use this for initialization
	void Awake ()
    {
        sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter()
    {
        sound.Play();
    }
}
