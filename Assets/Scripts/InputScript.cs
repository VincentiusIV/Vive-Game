using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class InputScript : MonoBehaviour
{
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    private Animator animator;
    // Use this for initialization
    void Awake ()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    void Update()
    {
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            SetAnimation("Grab", false, true);
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            SetAnimation("Grab", true, false);
        }
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            SetAnimation("Pinch", false, true);
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            SetAnimation("Pinch", true, false);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "VrController")
        { return; }
        else if(col.CompareTag("InteractableArea"))
        {
            Debug.Log("collider tag is " + col.tag);
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                Debug.Log("Grip is held down");
                col.transform.position = new Vector3(col.transform.position.x, col.transform.position.y, this.transform.position.z );
            }
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {

            }
        }
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
                if (col.gameObject.GetComponent<PatientScript>().isColSnap == true && col.tag != "Patient")
                {
                    col.gameObject.GetComponent<PatientScript>().canSnap = false;
                }
            }

            // runs when trigger is released
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
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
                if (col.gameObject.GetComponent<PatientScript>().isColSnap == true)
                {
                    col.gameObject.GetComponent<PatientScript>().canSnap = true;
                }
            }

            
        }
    }

    void SetAnimation(String varName, bool whenIts, bool ItShouldBe)
    {
        if (animator.GetBool(varName) == whenIts)
        {
            animator.SetBool(varName, ItShouldBe);
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

 
    

