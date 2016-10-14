using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickUpScript : MonoBehaviour {
   
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    private GameObject colObj;
    private Transform resPosColObj;

    // Use this for initialization
    void Awake ()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding touch on the trigger");
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("You are holding touch on the grip button");
            colObj.transform.position = 
            colObj.GetComponent<Rigidbody>().velocity =
            colObj.GetComponent<Rigidbody>().angularVelocity = resPosColObj.position;

            resPosColObj = null;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(resPosColObj == null && colObj == null)
        {
            resPosColObj = col.transform;
            colObj = col.gameObject;
        }

        Debug.Log("You have collided with " + col.name + " and activated OnTriggerStay");
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have collided with " + col.name + " while holding down the trigger");
            col.attachedRigidbody.isKinematic = true;
            col.gameObject.transform.SetParent(this.gameObject.transform);
        }

        if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have released Touch while colliding with " + col.name);
            col.gameObject.transform.SetParent(null);
            col.attachedRigidbody.isKinematic = false;

            if(col.attachedRigidbody != null)
            {
                tossObject(col.attachedRigidbody);
            }
            
            //col.GetComponent<Rigidbody>()
        }
    }

    private void tossObject(Rigidbody rigidbody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if(origin != null)
        {
            rigidbody.velocity = origin.TransformVector(device.velocity);
            rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
        }
        else
        {
            rigidbody.velocity = device.velocity;
            rigidbody.angularVelocity = device.angularVelocity;
        }

    }
}
