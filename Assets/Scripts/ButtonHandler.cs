using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {

    public bool scene1;
    public bool scene2;
    public bool scene3;


    void OnTriggerEnter(Collider other) {
        if (other.tag == "VrController") {
            if (scene1 == true) {
                SceneManager.LoadScene("Game");
            } else if (scene2 == true) {
                SceneManager.LoadScene("Office Scene");
            } else if (scene3 == true) {
                SceneManager.LoadScene("Main Menu");
            }
        }

    }


}
