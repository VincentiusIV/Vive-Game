using UnityEngine;
using System.Collections;
using Valve.VR;

public class ControllerScene : MonoBehaviour {

    SteamVR_TrackedObject obj;

    public GameObject ButtonHolder;

    public bool ButtonEnabled;


	void Awake() {
        obj = GetComponent<SteamVR_TrackedObject>();
        ButtonHolder.SetActive(false);
        ButtonEnabled = false;

	}
	
	void Update () {
        var device = SteamVR_Controller.Input((int)obj.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
            if (ButtonEnabled == false) {
                ButtonEnabled = true;
                ButtonHolder.SetActive(true);
            } else if (ButtonEnabled == true) {
                ButtonEnabled = false;
                ButtonHolder.SetActive(false);
            }
        }
	}
}
