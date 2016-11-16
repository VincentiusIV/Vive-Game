using UnityEngine;
using System.Collections;

// Small script that determines when the chest of the patient is compressed or not
public class AddForce : MonoBehaviour {

    private Rigidbody rb;
    private Transform tf;

    public GameObject downTrigger;

    public bool uncompressed;
    
	// Use this for initialization
	void Awake ()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>(); 
    }
	
	// Permanently fixes the rotation of the object
	void Update ()
    {
        tf.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        // Makes it able to test CPR without Vive
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.down * 10.0f, ForceMode.Force);
        }
	}

    // when collider enters downtrigger, chest is compressed
    void OnTriggerEnter(Collider col)
    {
        if (col.name == downTrigger.name)
        {
            uncompressed = false;
        }
    }

    // when collider leaves the downtrigger, chest is uncompressed
    void OnTriggerExit(Collider col)
    {
        if (col.name == downTrigger.name)
        {
            uncompressed = true;
        }
    }
}
