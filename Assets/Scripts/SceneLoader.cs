using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool SwitchScene;
    public string sceneName;
    /*
     * Method used for the switching of scenes. OnTriggerEnter is called whenever collision happens
     * A check is the performed to see if the colliding object is the controller, if true it checks which scene is being tapped and then it's loaded.     * 
     */
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VrController"))
        {
            if (SwitchScene)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
