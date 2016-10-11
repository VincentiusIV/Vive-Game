using UnityEngine;
using System.Collections;

public class WandController : MonoBehaviour
{
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private bool gripButtonDown = false;
    private bool gripButtonUp = false;
    public bool gripButtonPressed = false;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private bool triggerButtonDown = false;
    private bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;

    private Valve.VR.EVRButtonId dPad_Up = Valve.VR.EVRButtonId.k_EButton_DPad_Up;
    private bool dPad_UpDown = false;
    private bool dPad_UpUp = false;
    public bool dPad_UpPressed = false;

    private Valve.VR.EVRButtonId dPad_Down = Valve.VR.EVRButtonId.k_EButton_DPad_Down;
    private bool dPad_DownDown = false;
    private bool dPad_DownUp = false;
    public bool dPad_DownPressed = false;

    private Valve.VR.EVRButtonId dPad_Right = Valve.VR.EVRButtonId.k_EButton_DPad_Right;
    private bool dPad_RightDown = false;
    private bool dPad_RightUp = false;
    public bool dPad_RightPressed = false;

    private Valve.VR.EVRButtonId dPad_Left = Valve.VR.EVRButtonId.k_EButton_DPad_Left;
    private bool dPad_LeftDown = false;
    private bool dPad_LeftUp = false;
    public bool dPad_LeftPressed = false;

    private SteamVR_Controller.Device controller {  get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;
	// Use this for initialization
	void Start ()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);

        dPad_UpDown = controller.GetPressDown(dPad_Up);
        dPad_UpUp = controller.GetPressUp(dPad_Up);
        dPad_UpPressed = controller.GetPress(dPad_Up);

        dPad_DownDown = controller.GetPressDown(dPad_Down);
        dPad_DownUp = controller.GetPressUp(dPad_Down);
        dPad_DownPressed = controller.GetPress(dPad_Down);

        dPad_RightDown = controller.GetPressDown(dPad_Right);
        dPad_RightUp = controller.GetPressUp(dPad_Right);
        dPad_RightPressed = controller.GetPress(dPad_Right);

        dPad_LeftDown = controller.GetPressDown(dPad_Left);
        dPad_LeftUp = controller.GetPressUp(dPad_Left);
        dPad_LeftPressed = controller.GetPress(dPad_Left);

        if (gripButtonDown)
        {
            Debug.Log("Grip Button was pressed");
        }

        if (gripButtonUp)
        {
            Debug.Log("Grip Button was released");
        }

        if (triggerButtonDown)
        {
            Debug.Log("trigger Button was pressed");
        }

        if (triggerButtonUp)
        {
            Debug.Log("trigger Button was released");
        }

        if (dPad_UpDown || dPad_DownDown || dPad_RightDown || dPad_LeftDown)
        {
            Debug.Log("the dpad was pressed was pressed");
        }

        if (dPad_UpUp || dPad_DownUp || dPad_RightUp || dPad_LeftUp)
        {
            Debug.Log("the dpad was released");
        }
    }
}
