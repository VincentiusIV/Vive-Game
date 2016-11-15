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
    public bool preview;

    public bool showHighlight;
    public GameObject highlight;

    void Awake()
    {
        if (showHighlight)
        {
            highlight.SetActive(false);
        }
        if(highlight == null)
        {
            highlight = new GameObject();
        }
        if(previewObject == null)
        {
            previewObject = new GameObject();
        }
    }

    void Update()
    {

    }
    /*
     * Method for button functionality such as Loading a new scene
     * runs when the button is colliding with the Vive Controller
     */
    void OnTriggerEnter(Collider other)
    {
        if(showHighlight)
        {
            highlight.SetActive(true);
        }
        

        Debug.Log("Touched by: " + other.gameObject.name);
        if (switchScene)
        {
            SceneManager.LoadScene(sceneName);
        }
        if(exitGame)
        {
            Application.Quit();
        }
        if(preview)
        {
            ShowPreview();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (showHighlight)
        {
            highlight.SetActive(false);
        }
    }

    void ShowPreview()
    {
        Debug.Log("switching preview");
        previewObject.GetComponent<MenuPreviewer>().SwitchPreview(previewID);
    }
}
