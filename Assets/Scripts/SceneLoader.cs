using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public bool GameScene;
    public bool OfficeScene;
    public bool MenuScene;
    public bool AnimationTest;
    public bool Field;
    
    /*
     * Method used for the switching of scenes. OnTriggerEnter is called whenever collision happens
     * A check is the performed to see if the colliding object is the controller, if true it checks which scene is being tapped and then it's loaded.     * 
     */
    void OnTriggerEnter(Collider other) {
        if (other.tag == "VrController") {
            if (GameScene) {
                SceneManager.LoadScene("Game Test Scene");
            }
            else if (OfficeScene) {
                SceneManager.LoadScene("Office Scene");
            }
            else if (MenuScene) {
                SceneManager.LoadScene("Main Menu");
            }
            else if (AnimationTest)
            {
                SceneManager.LoadScene("AnimationTest");
            }
            else if (Field)
            {
                SceneManager.LoadScene("Field");
            }
        }
    }
}
