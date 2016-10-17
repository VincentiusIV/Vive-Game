using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    public bool GameScene;
    public bool OfficeScene;
    public bool MenuScene;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "VrController")
        {
            Debug.Log("Collider has VrController tag.");
            if (GameScene == true)
            {
                Debug.Log("GameScene == true");
                SceneManager.LoadScene("Game");
                Debug.Log("Game scene loaded");
            }
            else if (OfficeScene == true)
            {
                Debug.Log("OfficeScene 2 == true");
                SceneManager.LoadScene("Office Scene");
                Debug.Log("office scene loaded.");
            }
            else if (MenuScene == true)
            {
                Debug.Log("scene3 == true");
                SceneManager.LoadScene("Main Menu");
                Debug.Log("Main menu scene loaded.");
            }
        }
    }



}
