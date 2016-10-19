using UnityEngine;
using System.Collections;

public class CompareTags : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("SnapPosition"))
        {
            col.GetComponent<SnapScript>().SnapToPosition(this.gameObject);
        }
    }
}
