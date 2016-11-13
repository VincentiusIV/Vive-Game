using UnityEngine;
using System.Collections;

public class MenuAnimator : MonoBehaviour
{
    public GameObject buttons;
    public float upDistance;
    public int animationSpeed;
	// Use this for initialization
	void Awake ()
    {
        StartCoroutine(moveUp());
	}
	
	IEnumerator moveUp()
    {
        for (int i = 0; i < animationSpeed; i++)
        {
            buttons.transform.Translate(new Vector3(0.0f, (upDistance / animationSpeed), 0.0f));
            yield return new WaitForSeconds(0.0f);
        }
    }
}
