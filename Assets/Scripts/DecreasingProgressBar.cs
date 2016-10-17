using UnityEngine;
using System.Collections;

public class DecreasingProgressBar : MonoBehaviour
{
    public int progressValue;
    public int decreasePerSec;

    private bool patientAlive;
    private int effectiveness;

	// Use this for initialization
	void Start ()
    {
        patientAlive = true;
        effectiveness = 0;

	    if(progressValue == 0)
        {
            progressValue = 100;
        }
        StartCoroutine(DecreasePerSecond());
	}
	
	IEnumerator DecreasePerSecond()
    {
        while (progressValue != 0 || patientAlive == false)
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
            progressValue += effectiveness;
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

    IEnumerator checkEffectiveness()
    {
        while(true)
        {
            bool increase = true;
            bool decrease = false;

            if (increase)
            {
                effectiveness += 1;
                if(effectiveness >= 10)
                {
                    increase = false;
                    decrease = true;
                }
            }

            if (decrease)
            {
                effectiveness -= 1;
                if (effectiveness <= -5)
                {
                    increase = true;
                    decrease = false;
                }
            }

            yield return new WaitForSeconds(1/15);
            Debug.Log("effectiveness = ": effectiveness);
        } 
    }
}
