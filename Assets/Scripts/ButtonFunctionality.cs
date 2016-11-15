using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonFunctionality : MonoBehaviour
{
    
    public float upDistance;
    public int animationSpeed;

    public bool switchScene;
    public string sceneName;
    public bool exitGame;
    public bool showSliders;

    private Vector3 pos;
    private Vector3 startPos;
    private bool isUp;

    void Awake()
    {
        isUp = false;
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
        Debug.Log("Touched by: " + other.gameObject.name);
        if (switchScene)
        {
            SceneManager.LoadScene(sceneName);
        }
        if(exitGame)
        {
            Application.Quit();
        }
        if(showSliders)
        {
            StartCoroutine(ScaleAnimation(false));
        }
    }

    void OnMouseEnter()
    {
        if (switchScene)
        {
            SceneManager.LoadScene(sceneName);
        }
        if (showSliders)
        {
            StartCoroutine(ScaleAnimation(false));
        }
    }

    IEnumerator ScaleAnimation(bool scaleSetting)
    {
        float scaleValue = transform.parent.localScale.x;

        for (int i = 0; i < transform.parent.localScale.x; i = 0)
        {
            Debug.Log("setting new scale");
            if(scaleSetting)
            {
                scaleValue += (transform.parent.localScale.x / animationSpeed);
            }
            else
            {
                scaleValue -= (transform.parent.localScale.x / animationSpeed);
            }
            
            transform.parent.transform.localScale = new Vector3(scaleValue, 1f, 1f);
            yield return new WaitForEndOfFrame();
        }
    }
}
