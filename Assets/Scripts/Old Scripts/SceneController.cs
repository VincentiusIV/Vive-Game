﻿using UnityEngine;
using System.Collections;
using Valve.VR;

public class SceneController : MonoBehaviour { 
  /*  this script is old and has some link issues therefor this is commented
   *  
   *  SteamVR_TrackedObject obj;

    public GameObject ButtonHolder;
    public bool ButtonEnabled;

	void Awake() {
        obj = GetComponent<SteamVR_TrackedObject>();
        ButtonHolder.SetActive(false);
        ButtonEnabled = false;       
	}
	
    /*
     * Handles the visibility of the scene loading cubes. 
     * Opens or closes the loading cubes when the applicationbutton is pressed 
     *
	void Update () {

        var device = SteamVR_Controller.Input((int)obj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
            if (ButtonEnabled == false) {
                ButtonEnabled = true;
                ButtonHolder.SetActive(true);
            }
            else if (ButtonEnabled == true) {
                ButtonEnabled = false;
                ButtonHolder.SetActive(false);
            }
        }
	}*/
}
