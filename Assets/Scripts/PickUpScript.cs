using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickUpScript : MonoBehaviour
{
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    // Use this for initialization
    void Awake ()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }
    
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "VrController")
        { return; } 
        else
        {
            Debug.Log("You have collided with " + col.name + " and activated OnTriggerStay");
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("You have collided with " + col.name + " while holding down the trigger");
                col.attachedRigidbody.isKinematic = true;
                col.gameObject.transform.SetParent(this.gameObject.transform);

                if (col.gameObject.GetComponent<CompareTags>().isColSnap == true)
                {
                    col.gameObject.GetComponent<CompareTags>().canSnap = false;
                }
            }

            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("You have released Trigger while colliding with " + col.name);
                col.gameObject.transform.SetParent(null);
                col.attachedRigidbody.isKinematic = false;

                if (col.attachedRigidbody != null)
                {
                    tossObject(col.attachedRigidbody);
                }
                if(col.gameObject.GetComponent<CompareTags>().isColSnap == true)
                {
                    col.gameObject.GetComponent<CompareTags>().canSnap = true;
                }
            }

            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                Debug.Log("Grip was pressed while colliding with " + col.name);

                if (col.CompareTag("Patient"))
                {
                    col.GetComponent<DecreasingProgressBar>().increaseForPush();
                }
            }
        }

        if(Input.GetButtonDown("Fire2"))
        {
            col.GetComponent<DecreasingProgressBar>().increaseForPush();
        }
    }

    public void tossObject(Rigidbody rigidbody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
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

 
    

