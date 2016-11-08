using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    public bool GameScene;
    public bool OfficeScene;
    public bool MenuScene;
    
    /*
     * Method used for the switching of scenes. OnTriggerEnter is called whenever collision happens
     * A check is the performed to see if the colliding object is the controller, if true it checks which scene is being tapped and then it's loaded.     * 
     */
    void OnTriggerEnter(Collider other) {
        if (other.tag == "VrController") {
            if (GameScene == true) {
                SceneManager.LoadScene("Game Test Scene");
            }
            else if (OfficeScene == true) {
                SceneManager.LoadScene("Office Scene");
            }
            else if (MenuScene == true) {
                SceneManager.LoadScene("Main Menu");
            }
        }
    }
}
