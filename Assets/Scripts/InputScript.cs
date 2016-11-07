using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class InputScript : MonoBehaviour
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

    void Update ()
    {
        
    }
    
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "VrController")
        { return; } 
        else
        {
            // runs when trigger is held down
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                col.attachedRigidbody.isKinematic = true;
                col.gameObject.transform.SetParent(this.gameObject.transform);

                /* This is for objects that have already been snapped into place by the player
                 * canSnap is reset so the player can pick up the object that has been snapped before
                 */
                if (col.gameObject.GetComponent<CompareTags>().isColSnap == true)
                {
                    col.gameObject.GetComponent<CompareTags>().canSnap = false;
                }
            }

            // runs when trigger is released
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("You have released Trigger while colliding with " + col.name);
                col.gameObject.transform.SetParent(null);
                col.attachedRigidbody.isKinematic = false;

                if (col.attachedRigidbody != null)
                {
                    tossObject(col.attachedRigidbody);
                }

                /* This basically checks if the object the controller is colliding with
                 * is colliding with a SnapPosition.
                 * This makes the object able to snap onto the SnapPosition when the trigger is released.
                 */
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
                    col.GetComponent<PatientScript>().increaseForPush();
                }
            }
        }

        // call IncreaseForPush when Jump is pressed
        if (Input.GetButtonDown("Jump"))
        {
            col.GetComponent<PatientScript>().increaseForPush();
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

 
    

