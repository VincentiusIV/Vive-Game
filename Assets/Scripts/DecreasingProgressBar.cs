using UnityEngine;
using System.Collections;

public class DecreasingProgressBar : MonoBehaviour
{
    public int progressValue;
    public int decreasePerSec;
    
	// Use this for initialization
	void Start ()
    {
	    if(progressValue == 0)
        {
            progressValue = 100;
        }
        StartCoroutine(DecreasePerSecond());
	}
	
	IEnumerator DecreasePerSecond()
    {
        while (progressValue != 0)
        {
            progressValue -= decreasePerSec;
            yield return new WaitForSeconds(1.0f);
            Debug.Log("progress bar is at: " + progressValue + "%");
        }
    }

    public void increaseForPush()
    {
        progressValue += 10;
        if (progressValue >= 100)
        {
            Debug.Log("patient is breathing again! you win");

        }
    }
}
