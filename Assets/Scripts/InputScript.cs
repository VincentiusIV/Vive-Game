﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class InputScript : MonoBehaviour
{
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    public GameObject pointer;
    private Animator animator;
    // Use this for initialization
    void Awake ()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        animator = GetComponent<Animator>();
        if(pointer != null)
        {
            pointer.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    //Runs every frame and handles the animation for the controllers when certain buttons are pressed.

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
            if (pointer != null)
            {
                pointer.SetActive(true);
            }
        }
        if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (pointer != null)
            {
                pointer.SetActive(false);
            }
        }
    }

    /*
     * OnTriggerStay is called whenever an object with a trigger collider collides with something else (the parameter col)
     * It handles determination of the colliding object, va the means of tags attached to objects.
     * Based on said determination, it will perform actions corresponding with the object it's colliding with.
     */
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "VrController")
        {
            return;
        }
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

        if (col.tag != "InteractableArea" && col.tag != "HMD" && col.tag != "FixedObject")
        {
            if(col.tag == "Patient" && col.GetComponent<PatientScript>().isOnStretcher == true)
            {
                return;
            }
            // runs when trigger is held down
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                col.attachedRigidbody.isKinematic = true;
                col.gameObject.transform.SetParent(this.gameObject.transform);

                if(col.CompareTag("TutorialObject"))
                {
                    col.GetComponent<Tutorial>().StartPinchTutorial();
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
            }

            if(device.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && col.CompareTag("TutorialObject"))
            {
                col.GetComponent<Tutorial>().EndTutorial();
            }
        }
    }


    //Method to determine which animation should be performed.
    void SetAnimation(String varName, bool whenIts, bool ItShouldBe)
    {
        if (animator.GetBool(varName) == whenIts)
        {
            animator.SetBool(varName, ItShouldBe);
        }
    }

    //Method used for throwing away objects, it uses the velocity of the controller to determine the velocity of the object that gets thrown away.
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