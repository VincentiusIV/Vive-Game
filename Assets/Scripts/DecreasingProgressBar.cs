using UnityEngine;
using System.Collections;

public class DecreasingProgressBar : MonoBehaviour
{
    public float progressValue;
    public float decreasePerMs;

    private bool runBar;
    private int effectiveness;

    private bool increase = true;
    private bool decrease = false;
    // Use this for initialization
    void Start ()
    {
        runBar = true;

        Debug.Log("runBar in Start() = " + runBar);
        StartCoroutine(DecreasePerSecond());
        StartCoroutine(checkEffectiveness());
	}

	IEnumerator DecreasePerSecond()
    {
        // runs continously while runBar == true
        while (progressValue > 0 || progressValue < 100)
        {
            progressValue -= decreasePerMs;
            Debug.Log("progress bar is at: " + progressValue + "%");

            if (progressValue <= 0)
            {
                Debug.Log("patient died! you lose");
                //runBar = false;
            }
            if (progressValue >= 100)
            {
                Debug.Log("patient is alive! you win");
                //runBar = false;
            }
            yield return new WaitForSeconds(1.0f);
        }
        
    }

    public void increaseForPush()
    {
        if (progressValue > 0 || progressValue < 100)
        {
            progressValue += effectiveness;
            Debug.Log("You pushed the chest and improved health by: "+effectiveness);

            effectiveness = -5;
            increase = true;
            decrease = false;
        }
    }

    IEnumerator checkEffectiveness()
    {
        while(progressValue > 0 || progressValue < 100)
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
                if (effectiveness <= -6)
                {
                    increase = true;
                    decrease = false;
                }
            }
            yield return new WaitForSeconds(1/15);
        } 
    }
}
