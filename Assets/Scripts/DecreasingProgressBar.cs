using UnityEngine;
using System.Collections;

public class DecreasingProgressBar : MonoBehaviour
{
    public float progressValue;
    public float decreasePerSec;

    private bool alive;
    private int effectiveness;

    private bool increase = true;
    private bool decrease = false;
    // Use this for initialization
    void Start ()
    {
        alive = true;
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
        while (progressValue != 0 && alive)
        {
            progressValue -= decreasePerSec;
            yield return new WaitForSeconds((decreasePerSec * 2) / 10);
            Debug.Log("progress bar is at: " + progressValue + "%");

            if(progressValue <= 0)
            {
                Debug.Log("patient died! you lose");
                alive = false;
            }
        }
    }

    public void increaseForPush()
    {
        if(alive)
        {
            progressValue += effectiveness;
            Debug.Log("You pushed the chest");

            if (progressValue > 100)
            {
                Debug.Log("Patient is breathing again! you win");
                progressValue = 100;
                alive = false;
            }
        }
        else
        {
            Debug.Log("Patient no longer needs pushing");
        }
        
    }

    IEnumerator checkEffectiveness()
    {
        while(alive)
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
