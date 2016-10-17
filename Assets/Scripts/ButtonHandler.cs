using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {

    SteamVR_TrackedObject obj;

    public bool scene1;
    public bool scene2;
    public bool scene3;

    void Awake() {
        obj = GetComponent<SteamVR_TrackedObject>();
    }


    void OnTriggerEnter(Collider other) {
        if (other.tag == "VrController") {
            var device = SteamVR_Controller.Input((int)obj.index);
            Debug.Log("Collider has VrController tag.");
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
                Debug.Log("Colliding and trigger is down."); 
                if (scene1 == true) {
                    Debug.Log("Scene1 == true");
                    SceneManager.LoadScene("Game");
                    Debug.Log("Game scene loaded");
                } else if (scene2 == true) {
                    Debug.Log("Scene 2 == true");
                    SceneManager.LoadScene("Office Scene");
                    Debug.Log("office scene loaded.");
                } else if (scene3 == true) {
                    Debug.Log("scene3 == true");
                    SceneManager.LoadScene("Main Menu");
                    Debug.Log("Main menu scene loaded.");
                }
            }
        }

    }


}
