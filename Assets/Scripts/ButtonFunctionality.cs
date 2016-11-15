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
    public bool returnToMenuButtons;

    private Vector3 pos;
    private Vector3 startPos;

    void Awake()
    {
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
            StartCoroutine(ScaleAnimation(transform.parent.gameObject, false));
        }
    }

   /* void OnMouseEnter()
    {
        if (switchScene)
        {
            SceneManager.LoadScene(sceneName);
        }
        if (showSliders)
        {
            StartCoroutine(ScaleAnimation(transform.parent.gameObject, false));
        }
    }
    */
    IEnumerator ScaleAnimation(GameObject obj, bool scaleSetting)
    {
        float scaleValue = transform.parent.localScale.x;

        for (float i = 10; i == 0; i--)
        {
            Debug.Log("setting new scale");
            
            scaleValue -= 1 / animationSpeed;
            
            obj.transform.localScale = new Vector3(scaleValue, 1f, 1f);
            yield return new WaitForEndOfFrame();
        }

        obj.transform.localScale = Vector3.zero;
    }

    IEnumerator MoveAnimation(GameObject obj, bool moveSetting)
    {
        float posValue;

        for (int i = 0; i < animationSpeed; i++)
        {
            obj.transform.Translate(Vector3.up * (upDistance/animationSpeed));
            yield return new WaitForEndOfFrame();
        }
    }
}
