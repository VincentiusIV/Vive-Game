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

    /*
     * Method for button functionality such as Loading a new scene
     * runs when the button is colliding with the Vive Controller
     */
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("VrController"))
        {
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
                StartCoroutine(move(false));
            }
        }
    }
    void OnMouseEnter()
    {
        Debug.Log("mouse is over");
        if (showSliders)
        {
            if(transform.parent.transform.position.y >= upDistance)
            {
                StartCoroutine(move(true));
            }
            else if(transform.parent.transform.position.y <= 0)
            {
                StartCoroutine(move(false));
            }
            
        }
    }

    IEnumerator move(bool down)
    {
        for (int i = 0; i < animationSpeed; i++)
        {
            float yValue = (upDistance / animationSpeed);

            if (down)
            {
                yValue *= -1;
            }

            transform.parent.transform.Translate(new Vector3(0.0f, yValue, 0.0f));
            yield return new WaitForSeconds(0.0f);
        }
    }
}
