using UnityEngine;
using System.Collections;

public class DecreasingProgressBar : MonoBehaviour
{
    public float progressValue;
    public float decreasePerSec;

    private bool patientAlive;
    private int effectiveness;

    private bool increase = true;
    private bool decrease = false;
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
        StartCoroutine(checkEffectiveness());
	}
	
	IEnumerator DecreasePerSecond()
    {
        while (progressValue != 0 && patientAlive)
        {
            progressValue -= decreasePerSec;
            yield return new WaitForSeconds((decreasePerSec * 2) / 10);
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
                if (effectiveness <= -4)
                {
                    increase = true;
                    decrease = false;
                }
            }

            yield return new WaitForSeconds(1/15);
            Debug.Log("effectiveness = "+ effectiveness);
        } 
    }
}
