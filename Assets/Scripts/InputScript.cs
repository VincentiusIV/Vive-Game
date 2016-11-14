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

        if(device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("you touched the touchpad");
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "VrController")
        { return; }
        else if(col.CompareTag("InteractableArea"))
        {
            if (col.name == "PinchArea")
            {
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
                {
                    col.transform.parent.GetComponent<PatientScript>().pinchNose(true);
                }
                if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
                {
                    col.transform.parent.GetComponent<PatientScript>().pinchNose(false);
                }
            }
            else
            {
                col.transform.position = new Vector3(col.transform.position.x, this.transform.position.y, col.transform.position.z);
            }
        }
        else if(col.tag != "InteractableArea" && col.tag != "HMD")
        {
            if(col.tag == "Patient" && col.GetComponent<PatientScript>().isOnStretcher == true)
            {
                return;
            }
            // runs when trigger is held down
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                col.attachedRigidbody.isKinematic = true;
                col.gameObject.transform.SetParent(this.gameObject.transform);
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