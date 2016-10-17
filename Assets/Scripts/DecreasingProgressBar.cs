using UnityEngine;
using System.Collections;

public class DecreasingProgressBar : MonoBehaviour
{
    public int progressValue;
    public int decreasePerSec;

    private bool patientAlive;
	// Use this for initialization
	void Start ()
    {
        patientAlive = true;
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
            if(progressValue <= 0)
            {
                Debug.Log("patient died! you lose");
                patientAlive = false;
            }
        }
    }

    public void increaseForPush()
    {
        if(patientAlive)
        {
            progressValue += 10;
            Debug.Log("You pushed the chest");

            if (progressValue >= 100)
            {
                Debug.Log("Patient is breathing again! you win");
                progressValue = 100;
            }
        }
        else
        {
            Debug.Log("Patient is dead");
        }
    }
}
