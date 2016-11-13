using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonFunctionality : MonoBehaviour
{
    public bool SwitchScene;
    public string sceneName;
    public bool exitGame;
    /*
     * Method for button functionality such as Loading a new scene
     * runs when the button is colliding with the Vive Controller
     */
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VrController"))
        {
            if (SwitchScene)
            {
                SceneManager.LoadScene(sceneName);
            }
            if(exitGame)
            {
                Application.Quit();
            }
        }
    }
}
