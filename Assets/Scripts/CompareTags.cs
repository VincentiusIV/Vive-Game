using UnityEngine;
using System.Collections;

public class CompareTags : MonoBehaviour {

    SteamVR_Controller.Device device;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("SnapPosition") && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Other tag is SnapPosition");
            col.GetComponent<SnapScript>().SnapToPosition(this.gameObject);
        }
    }
}
