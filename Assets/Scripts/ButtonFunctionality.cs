using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonFunctionality : MonoBehaviour
{
    public GameObject previewObject;
    public int previewID;

    public bool switchScene;
    public string sceneName;
    public bool exitGame;

    public GameObject highlight;

    void Awake()
    {
        if (highlight != null)
        {
            highlight.SetActive(false);
        }
    }
    /*
     * Method for button functionality such as Loading a new scene
     * runs when the button is colliding with the Vive Controller
     */
    void OnTriggerEnter(Collider other)
    {
        if(highlight != null)
        {
            highlight.SetActive(true);
        }
        if (switchScene)
        {
            SceneManager.LoadScene(sceneName);
        }
        if(exitGame)
        {
            Application.Quit();
        }
        if(previewObject != null)
        {
            ShowPreview();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (highlight != null)
        {
            highlight.SetActive(false);
        }
    }

    void ShowPreview()
    {
        previewObject.GetComponent<MenuPreviewer>().SwitchPreview(previewID);
    }
}
