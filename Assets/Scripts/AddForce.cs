using UnityEngine;
using System.Collections;

public class AddForce : MonoBehaviour {

    private Rigidbody rb;
    private Transform tf;

    public GameObject upTrigger;
    public GameObject downTrigger;

    public AudioClip[] audioClips;
    public AudioSource breathingSound;

    public bool uncompressed;
    public bool compressed;
    
	// Use this for initialization
	void Awake ()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        tf.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        if (Input.GetButtonDown("Jump"))
        {
            //rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * 10.0f, ForceMode.Force);
            Debug.Log("added force");
        }
        if (Input.GetButtonUp("Jump"))
        {
            // tf.transform.position = Vector3.zero;
            //rb.velocity.Normalize();
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.name == upTrigger.name)
        {
            breathingSound.clip = audioClips[0];
            breathingSound.Play();
            uncompressed = true;
            compressed = false;
        }
        if(col.name == downTrigger.name)
        {
            compressed = true;
            uncompressed = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.name == upTrigger.name)
        {
            breathingSound.clip = audioClips[1];
            breathingSound.Play();
        }
    }
}
